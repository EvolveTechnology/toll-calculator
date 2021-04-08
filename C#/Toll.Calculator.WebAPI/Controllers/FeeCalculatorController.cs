using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Toll.Calculator.Domain;
using Toll.Calculator.Infrastructure.CustomExceptions;
using Toll.Calculator.Service;
using Toll.Calculator.Service.Interface;
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

        /// <summary>
        /// Get the total fee for all passages with provided vehicle
        /// </summary>
        /// <param name="requestModel">PassageDates eg. 2021-04-07T14:25:00</param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpGet("api/total-fee")]
        public async Task<IActionResult> GetTotalFee([FromQuery] TotalFeeRequestModel requestModel)
        {
            try
            {
                var totalFee = await _tollFeeService.GetTotalFee(requestModel.VehicleTypeToDomain(), requestModel.PassageDates.ToList());

                return Ok(new TotalFeeResponseModel
                {
                    TotalFee = totalFee
                });
            }
            catch (EnumCastException)
            {
                return BadRequest("Unable to parse VehicleType input");
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e);
            }
        }
    }
}