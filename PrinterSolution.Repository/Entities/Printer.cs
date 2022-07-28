using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrinterSolution.Repository.Entities
{
    public class Printer : BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;
        public PrinterStatus Status { get; set; }
        public PrinterType Type { get; set; }
        public DateTime? LastMaintenance { get; set; }
        public long? CoupledMaterialId { get; set; }

        public int Height { get; set; }
        public int Width { get; set; }
        public int Depth { get; set; }
        public bool HasHeatedBed { get; set; }

        [ForeignKey("CoupledMaterialId")]
        public Material? CoupledMaterial { get; set; } = new();
    }
}
