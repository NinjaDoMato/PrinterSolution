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
        public OrderHystoryType Type { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
