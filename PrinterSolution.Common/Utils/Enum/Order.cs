using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterSolution.Common.Utils.Enum
{
    public enum OrderStatus
    {
        [Description("Created")]
        Created,
        [Description("Waiting for payment")]
        WaitingPayment,
        [Description("Paid")]
        Paid,
        [Description("Processing")]
        Processing,
        [Description("Shipped")]
        Shipped,
        [Description("Finished")]
        Finished,
        [Description("Cancelled")]
        Cancelled,
        [Description("Error")]
        Error,
    }
}
