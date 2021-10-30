using PrinterSolution.Common.Utils.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterSolution.Common.Entities
{
    public class Configuration
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code{ get; set; }
        public string Value { get; set; }
        public ConfigurationType Type { get; set; }
    }
}
