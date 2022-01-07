using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrinterSolution.PrinterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrinterController : ControllerBase
    {
        private readonly ILogger<PrinterController> _logger;

        public PrinterController(ILogger<PrinterController> logger)
        {
            _logger = logger;
        }

    }
}
