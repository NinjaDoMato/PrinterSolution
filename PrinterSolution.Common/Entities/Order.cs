using PrinterSolution.Common.Utils.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterSolution.Common.Entities
{
    public class Order : BaseEntity
    {
        public OrderStatus Status { get; set; }

        public List<OrderHistory> History { get; set; }
    }
}
