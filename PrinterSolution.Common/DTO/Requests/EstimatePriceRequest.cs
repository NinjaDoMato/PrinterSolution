using System.ComponentModel.DataAnnotations;

namespace PrinterSolution.PriceAPI.Models.Requests
{
    public class EstimatePriceRequest
    {
        [Required]
        public string MaterialCode { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "The Weight of the part must be greater than zero.")]
        public decimal Weight { get; set; }

        [Range(0.01f, double.MaxValue, ErrorMessage = "The print time must be greater than zero.")]
        public decimal HoursPrinting { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "The preparation time must be equal or greater than zero.")]
        public decimal PreparationTime { get; set; }
    }
}
