using PrinterSolution.Common.Utils.Enum;

namespace PrinterSolution.Common.DTOs.Requests.Printer
{
    public class PrinterCommand
    {
        public long PrinterId { get; set; }
        public CommandType Type { get; set; }
        public string Value { get; set; }
    }
}
