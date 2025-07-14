using MassTransit;
using Microsoft.Extensions.Logging;
using QuartzMassTransitPOC.Messages;

public class SchedulOrderConsumer : IConsumer<SchedulOrder>
{
    private readonly ILogger<SchedulOrderConsumer> _logger;

    public SchedulOrderConsumer(ILogger<SchedulOrderConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<SchedulOrder> context)
    {
        _logger.LogInformation("Received SchedulOrder: {OrderId} at {Time}",
            context.Message.OrderId, context.Message.Timestamp);
        return Task.CompletedTask;
    }
}
