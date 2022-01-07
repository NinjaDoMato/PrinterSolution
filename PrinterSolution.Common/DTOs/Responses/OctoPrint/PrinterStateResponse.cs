using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterSolution.Common.DTOs.Responses.OctoPrint
{
    public class PrinterStateResponse
    {
        public TemperatureData Temperature { get; set; }
        public SdData Sd { get; set; }
        public StateData State { get; set; }

        public class TemperatureData
        {
            public ToolData Tool0 { get; set; }
            public ToolData Tool1 { get; set; }
            public ToolData Bed { get; set; }
            public List<History> History { get; set; }
        }

        public class ToolData
        {
            public decimal Actual { get; set; }
            public decimal? Target { get; set; }
            public int Offset { get; set; }
        }

        public class History
        {
            public int Time { get; set; }
            public ToolData Tool0 { get; set; }
            public ToolData Tool1 { get; set; }
            public ToolData Bed { get; set; }
        }
        public class SdData
        {
            public bool Ready { get; set; }
        }

        public class StateData
        {
            public string Text { get; set; }
            public Flags Flags { get; set; }
        }

        public class Flags
        {
            public bool Operational { get; set; }
            public bool Paused { get; set; }
            public bool Printing { get; set; }
            public bool Cancelling { get; set; }
            public bool Pausing { get; set; }
            public bool SdReady { get; set; }
            public bool Error { get; set; }
            public bool Ready { get; set; }
            public bool ClosedOrError { get; set; }
        }

    }
}
