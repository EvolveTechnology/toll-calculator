using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Toll.Calculator.Service;
using Toll.Calculator.WebAPI.ApiModels;

namespace Toll.Calculator.WebAPI.Controllers
{
    [ApiController]
    public class FeeCalculatorController : ControllerBase
    {
        private readonly ITollFeeService _tollFeeService;

        public FeeCalculatorController(
            ITollFeeService tollFeeService)
        {
            _tollFeeService = tollFeeService;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpGet("api/total-fee")]
        public async Task<IActionResult> GetTotalFee([FromBody] TotalFeeRequestModel requestModel)
        {
            try
            {
                var totalFee = await _tollFeeService.GetTotalFee(requestModel.VehicleType, requestModel.PassageDates);

                return Ok(new TotalFeeResponseModel
                {
                    TotalFee = totalFee
                });
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }

            return Ok();
        }
    }
}