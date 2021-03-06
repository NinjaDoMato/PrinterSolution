using PrinterSolution.Common.Utils.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterSolution.Common.Entities
{
    public class Job : BaseEntity
    {
        public int MaterialId { get; set; }
        public JobStatus Status { get; set; }
        public string File { get; set; }

        public decimal ManualWorkTime { get; set; }
        public decimal EstimatedPrintTime { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? FinishTime { get; set; }

        public decimal EstimatedMaterialUsage { get; set; }
        public decimal EstimatedMaterialCost { get; set; }
        public decimal EstimatedWorkCost { get; set; }


        public Material Material { get; set; }
        public List<JobHistory> JobHistory { get; set; }
    }
}
