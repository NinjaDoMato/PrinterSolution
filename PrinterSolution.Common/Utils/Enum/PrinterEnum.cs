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

    public enum CommandType
    {
        Printer,
        Bed,
        Tool,
        File
    }
}
