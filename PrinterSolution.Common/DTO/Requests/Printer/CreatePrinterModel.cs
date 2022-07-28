using PrinterSolution.Common.Utils.Enum;
using System.ComponentModel.DataAnnotations;

namespace PrinterSolution.Common.DTOs.Requests
{
    public class CreatePrinterModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Address { get; set; }
        public PrinterType Type { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage ="The printer height must be greater than zero.")]
        public int Height { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage ="The printer width must be greater than zero.")]
        public int Width { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage ="The printer depth must be greater than zero.")]
        public int Depth { get; set; }
        public bool HeatBed { get; set; }
    }
}
