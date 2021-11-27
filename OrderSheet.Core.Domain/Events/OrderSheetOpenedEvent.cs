namespace OrderSheet.Core.Domain.Events
{
    public class OrderSheetOpenedEvent : BaseDomainEvent
    {
        public Aggregates.OrderSheet Order { get; }

        public OrderSheetOpenedEvent(Aggregates.OrderSheet order)
        {
            Order = order;
        }
    }
}
