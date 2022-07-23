namespace PrinterSolution.Repository.Entities
{
    public class Configuration : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public ConfigurationType Type { get; set; }
    }
}
