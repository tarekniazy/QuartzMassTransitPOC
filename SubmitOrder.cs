namespace QuartzMassTransitPOC.Messages
{
    public record SubmitOrder
    {
        public Guid OrderId { get; init; } = Guid.NewGuid();
        public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    }
}