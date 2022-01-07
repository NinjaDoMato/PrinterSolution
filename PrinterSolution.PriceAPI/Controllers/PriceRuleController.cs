using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrinterSolution.Common.DTOs.Requests;
using PrinterSolution.Common.Entities;
using PrinterSolution.Common.Services;
using PrinterSolution.Common.Utils.Enum;
using PrinterSolution.PriceAPI.Models.Requests;
using PrinterSolution.PriceAPI.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrinterSolution.PriceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PriceRuleController : ControllerBase
    {
        private readonly ILogger<PriceController> _logger;
        private readonly IPriceRuleService _priceRuleService;

        public PriceRuleController(IPriceRuleService priceRuleService, ILogger<PriceController> logger)
        {
            _logger = logger;
            _priceRuleService = priceRuleService;
        }

        [HttpGet]
        [Route("Get")]
        public ActionResult<List<PriceRule>> Get([FromQuery] string code, [FromQuery] PriceRuleTarget? target, [FromQuery] PriceRuleOperation? type)
        {
            try
            {
                var result = _priceRuleService.GetRules();

                result = result.Where(r =>
                    (!string.IsNullOrEmpty(code)? r.Code.ToLower().Contains(code.ToLower()) : true) &&
                    (target.HasValue? r.Target == target.Value : true) &&
                    (type.HasValue? r.Operation == type.Value : true)
                    ).ToList();

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
        public ActionResult<PriceRule> Create([FromBody] CreatePriceRuleRequest request)
        {
            try
            {
                var result = _priceRuleService.CreateRule(request.Name, request.Code, request.Description, request.Target, request.Type, request.Value, request.Priority);

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
        public ActionResult<PriceRule> Update([FromBody] CreatePriceRuleRequest request)
        {
            try
            {
                // TODO
                return Ok();
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
                var result = _priceRuleService.DelceteRule(id);

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
