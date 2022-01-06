using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PrinterSolution.PriceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaterialController : ControllerBase
    {
        private readonly ILogger<MaterialController> _logger;
        private readonly IMaterialService _priceService;

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
