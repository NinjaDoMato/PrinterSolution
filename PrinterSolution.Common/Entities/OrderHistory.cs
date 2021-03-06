using PrinterSolution.Common.Utils.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterSolution.Common.Entities
{
    public class OrderHistory : BaseEntity
    {
        public string Description { get; set; }
        public HistoryType Type { get; set; }
        public long OrderId { get; set; }
        public Order Order { get; set; }
    }
}
