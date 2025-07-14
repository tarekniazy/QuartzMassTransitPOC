using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using QuartzMassTransitPOC.Messages;
using Quartz.MongoDB;


var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((hostContext, services) =>
{

    services.AddQuartz(q =>
    {
        q.SchedulerName = "MassTransit-Scheduler";
        q.SchedulerId = "AUTO";

        q.UseDefaultThreadPool(tp =>
        {
            tp.MaxConcurrency = 10;
        });

        q.UseInMemoryStore();

    });

    services.AddMassTransit(x =>
    {
        x.AddPublishMessageScheduler();

        //x.AddQuartzConsumers();

        x.AddConsumer<SchedulOrderConsumer>();

        x.AddConsumer<SubmitOrderConsumer>();

        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.UsePublishMessageScheduler();
            cfg.ReceiveEndpoint("schedule-order-queue", e =>
            {
                e.ConfigureConsumer<SchedulOrderConsumer>(context);
            });
            cfg.ConfigureEndpoints(context);

        });
    }); 
    services.Configure<MassTransitHostOptions>(options =>
    {
        options.WaitUntilStarted = true;
    });

    //services.AddQuartzHostedService(options =>
    //{
    //    //options.StartDelay = TimeSpan.FromSeconds(1);
    //    //options.WaitForJobsToComplete = true;
    //});
});


var host = builder.Build();

await ScheduleRecurringMessage(host.Services);
await host.RunAsync();


static async Task ScheduleRecurringMessage(IServiceProvider provider)
{
    using var scope = provider.CreateScope();

    var messageScheduler = scope.ServiceProvider.GetRequiredService<IRecurringMessageScheduler>();

    // Schedule the recurring message
    var schedule = new SubmitOrderSchedule("123", nameof(SchedulOrder), "0/1 * * * * ?", "");
    await messageScheduler.ScheduleRecurringSend(
        new Uri("queue:schedule-order-queue"),
        schedule,
        new SchedulOrder());

    Console.WriteLine("Scheduled recurring message.");

    // Wait for a while before canceling (e.g., 10 seconds)
    //await Task.Delay(TimeSpan.FromSeconds(20));

    // Cancel the scheduled recurring message
    await messageScheduler.CancelScheduledRecurringSend("123", nameof(SchedulOrder));

    Console.WriteLine("Cancelled recurring message.");
}
