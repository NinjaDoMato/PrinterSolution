using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrinterSolution.Common.DTOs.Requests;
using PrinterSolution.Common.Entities;
using PrinterSolution.Common.Services;
using PrinterSolution.PriceAPI.Models.Requests;
using System;
using System.Collections.Generic;

namespace PrinterSolution.PriceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaterialController : ControllerBase
    {
        private readonly ILogger<MaterialController> _logger;
        private readonly IMaterialService _materialService;

        public MaterialController(IMaterialService materialService, ILogger<MaterialController> logger)
        {
            _logger = logger;
            _materialService = materialService;
        }

        [HttpGet]
        [Route("Get")]
        public ActionResult<List<Material>> Get()
        {
            try
            {
                var result = _materialService.GetMaterials();

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
        public ActionResult<Material> Create([FromBody] CreateMaterialRequest request)
        {
            try
            {
                var material = _materialService.CreateMaterial(request.Name, request.Code, request.PricePerKilo, request.Type);

                return Ok(material);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpPost]
        [Route("Update")]
        public ActionResult<PriceRule> Update([FromBody] Material request)
        {
            try
            {
                var material = _materialService.UpdateMaterial(request);
                return Ok(material);
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
                var result = _materialService.DeleteMaterial(id);

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
