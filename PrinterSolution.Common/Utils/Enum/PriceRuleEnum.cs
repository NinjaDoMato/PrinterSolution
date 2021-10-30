using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterSolution.Common.Utils.Enum
{
    public enum PriceRuleTarget
    {
        MaterialCost,
        EnergyCost,
        Preparation,
        FinalPrice
    }

    public enum PriceRuleOperation
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }
}
