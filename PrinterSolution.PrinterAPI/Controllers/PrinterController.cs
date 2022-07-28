using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrinterSolution.Common.DTOs.Requests;
using PrinterSolution.Repository.Entities;
using PrinterSolution.Service.Interfaces;
using System;
using System.Collections.Generic;

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
        public ActionResult<Printer> Create([FromBody] CreatePrinterModel request)
        {
            try
            {
                var result = _printerService.CreatePrinter(request);

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
        public IActionResult Delete([FromQuery] long id)
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

        [HttpGet]
        [Route("Details/{printerId}")]
        public ActionResult<Printer> GetPrinterDetails(long printerId)
        {
            try
            {
                var printer = _printerService.GetPrinterById(printerId);

                return Ok(printer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpGet]
        [Route("Status/{printerId}")]
        public IActionResult GetPrinterStatus(long printerId)
        {
            throw new NotImplementedException();
        }
    }
}
