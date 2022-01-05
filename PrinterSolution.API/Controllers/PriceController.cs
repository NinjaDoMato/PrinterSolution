using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrinterSolution.PriceAPI.Models.Middleware.Requests;
using PrinterSolution.PriceAPI.Models.Middleware.Responses;
using PrinterSolution.PriceAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        [Route("EstimatePrice")]
        public ActionResult<PriceEstimation> EstimatePrice([FromBody] EstimatePriceRequest request)
        {
            var result = _priceService.EstimateFinalPrice(request.Weight, request.MaterialCode, request.HoursPrinting, request.PreparationTime);

            return Ok(result);
        }
    }
}
