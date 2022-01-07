using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterSolution.Common.DTOs.Responses.OctoPrint
{
    public class CurrentJobResponse
    {
        public FileData File { get; set; }
        public int EstimatedPrintTime { get; set; }
        public FilamentData Filament { get; set; }
        public ProgressData Progress { get; set; }
        public string State { get; set; }
        public string Error { get; set; }

        public class FileData
        {
            public string Name { get; set; }
            public string Origin { get; set; }
            public int Size { get; set; }
            public int Date { get; set; }
        }

        public class FilamentData
        {
            public class ToolData
            {
                public int Length { get; set; }
                public decimal Volume { get; set; }
            }
        }

        public class ProgressData
        {
            public decimal Completion { get; set; }
            public int Filepos { get; set; }
            public int PrintTime { get; set; }
            public int PrintTimeLeft { get; set; }
        }
    }
}
