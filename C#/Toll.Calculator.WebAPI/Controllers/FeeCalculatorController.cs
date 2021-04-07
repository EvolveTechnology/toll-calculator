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
        ///     Get the total fee for all passages with provided vehicle
        /// </summary>
        /// <param name="vehicleType"></param>
        /// <param name="passageDates">DateTimes in a semicolon-separated string, eg. "2021-04-06T15:12:00;2021-04-06T15:35:00"</param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpGet("api/total-fee")]
        public async Task<IActionResult> GetTotalFee(Vehicle vehicleType, string passageDates)
        {
            try
            {
                var totalFee =
                    await _tollFeeService.GetTotalFee(vehicleType, GetPassageDateTimes(passageDates));

                return Ok(new TotalFeeResponseModel
                {
                    TotalFee = totalFee
                });
            }
            catch (DateFormatException)
            {
                return BadRequest("Unable to parse input dates");
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e);
            }
        }

        private List<DateTime> GetPassageDateTimes(string passageDates)
        {
            var stringDates = passageDates.Split(";");

            try
            {
                return stringDates.Select(stringDate => DateTime.Parse(stringDate)).ToList();
            }
            catch (Exception)
            {
                throw new DateFormatException();
            }
        }
    }
}