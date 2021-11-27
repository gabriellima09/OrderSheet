namespace OrderSheet.Core.Domain.Events
{
    public class OrderSheetClosedEvent : BaseDomainEvent
    {
        public Aggregates.OrderSheet Order { get; }

        public OrderSheetClosedEvent(Aggregates.OrderSheet order)
        {
            Order = order;
        }
    }
}
