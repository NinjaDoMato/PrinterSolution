using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrinterSolution.PriceAPI.Models.Requests;
using PrinterSolution.PriceAPI.Models.Responses;
using PrinterSolution.Service.Interfaces;
using System;

namespace PrinterSolution.PriceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PriceController : ControllerBase
    {
        private readonly ILogger<PriceController> _logger;
        private readonly IPriceService _priceService;

        public PriceController(IPriceService priceService, ILogger<PriceController> logger)
        {
            _logger = logger;
            _priceService = priceService;
        }

        [HttpPost]
        [Route("EstimateFinalPrice")]
        public ActionResult<decimal> EstimateFinalPrice([FromBody] EstimatePriceRequest request)
        {
            try
            {
                var result = _priceService.EstimateFinalPrice(request.Weight, request.MaterialCode, request.HoursPrinting, request.PreparationTime);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpPost]
        [Route("EstimateDetailedPrice")]
        public ActionResult<PriceEstimation> EstimateDetailedPrice([FromBody] EstimatePriceRequest request)
        {
            try
            {
                var result = _priceService.EstimateDetailedCosts(request.Weight, request.MaterialCode, request.HoursPrinting, request.PreparationTime);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }
    }
}
