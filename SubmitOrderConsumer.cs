using MassTransit;
using Microsoft.Extensions.Logging;
using QuartzMassTransitPOC.Messages;

public class SubmitOrderConsumer : IConsumer<SubmitOrder>
{
    private readonly ILogger<SubmitOrderConsumer> _logger;

    public SubmitOrderConsumer(ILogger<SubmitOrderConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<SubmitOrder> context)
    {
        _logger.LogInformation("Received SubmitOrder: {OrderId} at {Time}",
            context.Message.OrderId, context.Message.Timestamp);
        return Task.CompletedTask;
    }
}
