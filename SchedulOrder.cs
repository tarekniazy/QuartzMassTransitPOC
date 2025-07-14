namespace QuartzMassTransitPOC.Messages
{
    public record SchedulOrder
    {
        public Guid OrderId { get; init; } = Guid.NewGuid();
        public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    }
}