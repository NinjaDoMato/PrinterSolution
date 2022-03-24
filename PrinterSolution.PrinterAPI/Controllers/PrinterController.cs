using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrinterSolution.Common.DTOs.Requests;
using PrinterSolution.Common.Entities;
using PrinterSolution.Common.Services;
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
        private readonly IPrinterService _printerService;

        public PrinterController(IPrinterService printerService, ILogger<PrinterController> logger)
        {
            _logger = logger;
            _printerService = printerService;
        }

        #region CRUD
        [HttpGet]
        [Route("Get")]
        public ActionResult<List<Printer>> Get()
        {
            try
            {
                var result = _printerService.GetPrinters();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult<Printer> Create([FromBody] CreatePrinterRequest request)
        {
            try
            {
                var result = _printerService.CreatePrinter(request.Name, request.Address, request.Type, request.Height, request.Width, request.Depth, request.HeatBed);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpPost]
        [Route("Update")]
        public ActionResult<Printer> Update([FromBody] Printer request)
        {
            try
            {
                return Ok(_printerService.UpdatePrinter(request));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete([FromQuery] int id)
        {
            try
            {
                var result = _printerService.DeletePrinter(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }
        #endregion

        #region Commands
        [HttpGet]
        [Route("Info/{printerId}")]
        public ActionResult<Printer> GetPrinterInfo(long printerId)
        {
            try
            {
                var printer = _printerService.GetPrinterById(printerId);

                return printer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }
        #endregion
    }
}
