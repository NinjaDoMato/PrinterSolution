using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterSolution.Common.Utils.Enum
{
    public enum PrinterStatus
    {
        Offline,
        Operational,
        Printing,
        WaitingOperator
    }

    public enum PrinterType
    {
        FDM,
        SLS
    }
}
