using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Linq;
using TollFeeCalculator.Logic;
using TollFeeCalculator.Models;
using TollFeeCalculator.Models.ViewModels;
using TollFeeCalculator.Services;

namespace TollFeeCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TollCalculatorController : ControllerBase
    {
        private readonly ITollCalculator _tollCalculator;
        private ITollPassService _tollPassService;

        public TollCalculatorController(ITollCalculator tollCalculator, ITollPassService tollPassService)
        {
            _tollCalculator = tollCalculator;
            _tollPassService = tollPassService;
        }

        /// <summary>
        /// Toll Pass fee for an entry
        /// </summary>
        /// <param name="tollFeeViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(200, Type = typeof(int), Description = "The request was successful")]
        public int Post(TollFeeViewModel tollFeeViewModel)
        {
            try
            {
                var fee = _tollCalculator.GetTollFee(tollFeeViewModel.VehicleType, tollFeeViewModel.Date);
                var tollPass = new TollPass
                {
                    VehicleId = tollFeeViewModel.VehicleId,
                    VehicleType = tollFeeViewModel.VehicleType,
                    Date = tollFeeViewModel.Date.ToString(),
                };
                _tollPassService.Create(tollPass);
                var result = _tollCalculator.GetTollFee(tollFeeViewModel.VehicleType, tollFeeViewModel.Date);
                return result;
            }
            catch (Exception)
            {
                return default;
            }
            
        }

        /// <summary>
        /// Toll Passes for entire day
        /// </summary>
        /// <param name="tollFeeViewModel"></param>
        /// <returns></returns>
        [HttpPost("EntireDay")]
        [SwaggerResponse(200, Type = typeof(int), Description = "The request was successful")]
        public int PostEntireDay(TollFeeViewModel tollFeeViewModel)
        {
            try
            {
                var tollPasses = _tollPassService.Get(tollFeeViewModel.VehicleId);
                var dates = tollPasses.Select(t => DateTime.Parse(t.Date)).ToArray();
                var fee = _tollCalculator.GetTollFee(tollPasses.First()?.VehicleType, dates);
                return fee;
            }
            catch (Exception)
            {
                return default;
            }
            
        }
    }
}
