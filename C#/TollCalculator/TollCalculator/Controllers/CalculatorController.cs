using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TollFeeCalculator.Logic;
using TollFeeCalculator.Models;
using TollFeeCalculator.Models.ViewModels;
using TollFeeCalculator.Services;

namespace TollFeeCalculator.Controllers
{
    public class CalculatorController : Controller
    {
        private ITollCalculator _tollCalculator;
        private ITollPassService _tollPassService;

        public CalculatorController(ITollCalculator tollCalculator, ITollPassService tollPassService)
        {
            _tollCalculator = tollCalculator;
            _tollPassService = tollPassService;
        }

        /// <summary>
        /// The landing page view to calculate Toll Fee for an entry
        /// </summary>
        /// <returns>TollCalculator View</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// A view to display the Fees. Could be made as a partial view or the result displayed on the same
        /// </summary>
        /// <returns>The calculated Toll Fees</returns>
        public ActionResult Fees()
        {
            if (TempData.ContainsKey("Fee"))
            {
                ViewBag.Fee = TempData["Fee"].ToString();
            }
                
            return View();
        }

        /// <summary>
        /// Calculates the Toll Fee for an entry
        /// </summary>
        /// <param name="tollFeeViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CalculateFee(TollFeeViewModel tollFeeViewModel)
        {
            try
            {
                var fee = _tollCalculator.GetTollFee(tollFeeViewModel.VehicleType, tollFeeViewModel.Date);
                TempData["Fee"] = fee;
                var tollPass = new TollPass
                {
                    VehicleId = tollFeeViewModel.VehicleId,
                    VehicleType = tollFeeViewModel.VehicleType,
                    Date = tollFeeViewModel.Date.ToString(),
                };
                _tollPassService.Create(tollPass);
                return RedirectToAction(nameof(Fees), "Calculator");
            }
            catch
            {
                return View("Error");
            }
        }

        /// <summary>
        /// The landing page view to calculate Toll Fee for the entire day
        /// </summary>
        /// <returns></returns>
        public ActionResult TollPassesForTheDay()
        {
            return View();
        }

        /// <summary>
        /// Calculates the Toll Fee for the entire day
        /// </summary>
        /// <param name="tollFeeViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CalculateTollPassesForTheDay(TollFeeViewModel tollFeeViewModel)
        {
            try
            {
                var result = _tollPassService.Get(tollFeeViewModel.VehicleId);
                var dates = result.Select(t => DateTime.Parse(t.Date)).Where(d => d.Date == tollFeeViewModel.Date.Date).ToArray();
                var fee = _tollCalculator.GetTollFee(result.First()?.VehicleType, dates);
                TempData["Fee"] = fee;
                return RedirectToAction(nameof(Fees), "Calculator");
            }
            catch(Exception)
            {
                return View("Error");
            }
        }
    }
}
