namespace PrinterSolution.Repository.Entities
{
    public class OrderHistory : BaseEntity
    {
        public string Description { get; set; }
        public HistoryType Type { get; set; }
        public long OrderId { get; set; }
        public Order Order { get; set; }
    }
}
