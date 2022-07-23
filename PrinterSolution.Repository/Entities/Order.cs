namespace PrinterSolution.Repository.Entities
{
    public class Order : BaseEntity
    {
        public OrderStatus Status { get; set; }

        public List<OrderHistory> History { get; set; }
    }
}
