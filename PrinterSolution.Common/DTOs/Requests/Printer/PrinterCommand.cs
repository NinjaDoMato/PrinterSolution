using PrinterSolution.Common.Utils.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterSolution.Common.DTOs.Requests.Printer
{
    public class PrinterCommand
    {
        public long PrinterId { get; set; }
        public CommandType Type { get; set; }
        public string Value { get; set; }
    }
}
