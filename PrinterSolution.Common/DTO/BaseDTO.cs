using System;

namespace PrinterSolution.Common.DTO
{
    public abstract class BaseDTO
    {
        public long Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
