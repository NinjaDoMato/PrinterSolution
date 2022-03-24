using PrinterSolution.Common.Utils.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterSolution.Common.Entities
{
    public class JobHistory :BaseEntity
    {
        public string Description { get; set; }
        public HistoryType Type { get; set; }
        public long JobId { get; set; }
        public Job Job { get; set; }
    }
}
