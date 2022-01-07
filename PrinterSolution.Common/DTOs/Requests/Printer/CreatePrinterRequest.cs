using PrinterSolution.Common.Utils.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterSolution.Common.DTOs.Requests
{
    public class CreatePrinterRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public PrinterType Type { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Depth { get; set; }
        public bool HeatBed { get; set; }
    }
}
