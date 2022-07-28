namespace PrinterSolution.Repository.Entities
{
    public class Configuration : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public ConfigurationType Type { get; set; }
    }
}
