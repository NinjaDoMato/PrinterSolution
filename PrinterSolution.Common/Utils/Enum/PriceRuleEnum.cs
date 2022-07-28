using System.ComponentModel;

namespace PrinterSolution.Common.Utils.Enum
{
    public enum PriceRuleTarget
    {
        [Description("Material Cost")]
        MaterialCost,
        [Description("Energy Cost")]
        EnergyCost,
        [Description("Preparation Cost")]
        Preparation,
        [Description("Final Price")]
        FinalPrice
    }

    public enum PriceRuleOperation
    {
        Add,
        Subtract,
        Multiply,
        Divide,
        AddPercentage
    }
}
