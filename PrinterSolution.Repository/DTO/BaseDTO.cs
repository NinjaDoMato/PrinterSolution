using System.ComponentModel.DataAnnotations;

namespace PrinterSolution.Repository.DTO
{
    public abstract class BaseDTO
    {
        [Key]
        public long Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
