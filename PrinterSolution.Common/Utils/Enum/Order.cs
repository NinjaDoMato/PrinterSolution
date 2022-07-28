using System.ComponentModel;

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
