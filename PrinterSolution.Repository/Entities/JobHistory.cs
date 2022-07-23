namespace PrinterSolution.Repository.Entities
{
    public class JobHistory : BaseEntity
    {
        public string Description { get; set; }
        public HistoryType Type { get; set; }
        public long JobId { get; set; }
        public Job Job { get; set; }
    }
}
