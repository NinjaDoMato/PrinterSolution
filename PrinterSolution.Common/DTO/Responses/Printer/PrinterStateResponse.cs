using PrinterSolution.Common.Utils.Enum;

namespace PrinterSolution.Common.DTOs.Responses.Printer
{
    public class PrinterStateResponse
    {
        public long PrinterId { get; set; }
        public string PrinterName { get; set; }
        public PrinterStatus Status { get; set; }
        public TempStatus Tool { get; set; }
        public TempStatus Bed { get; set; }
        public decimal CPUTemp { get; set; }
    }

    public class TempStatus
    {
        public decimal ActualTemp { get; set; }
        public decimal TargetTemp { get; set; }
    }
}
