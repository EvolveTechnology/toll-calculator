using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using TollFeeCalculator.Contracts.Calendar;
using TollFeeCalculator.Entities;

namespace TollFeeCalculator.Sweden
{
	/// <summary>
	/// Implementation of ICalendar interface for Sweden
	/// </summary>
	public class Calendar : ICalendar
	{
		private readonly string _apiBaseAddress = ConfigurationManager.AppSettings["DateCheckAPIBaseAddress"];
		private readonly string _apiPathAndQuery = ConfigurationManager.AppSettings["DateCheckAPIPathAndQuery"];
		private readonly HttpClient _client = new HttpClient();

		public Calendar()
		{
			_client.BaseAddress = new Uri(_apiBaseAddress);
		}

		public bool IsDateTollFree(DateTime date)
		{
			//Saturdays, Sundays and whole July are toll free
			return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || date.Month == 7 ||
				//Public holidays are toll free
				!string.IsNullOrWhiteSpace(GetDayAsync(_apiPathAndQuery + date.ToString("yyyyMd")).Result.Helgdag) ||
				//Days before public holiday are toll free
				!string.IsNullOrWhiteSpace(GetDayAsync(_apiPathAndQuery + date.AddDays(1).ToString("yyyyMd")).Result.Helgdag);
		}

		private async Task<Day> GetDayAsync(string path)
		{
			Day day = null;
			HttpResponseMessage response = await _client.GetAsync(path);
			if (response.IsSuccessStatusCode)
			{
				day = await response.Content.ReadAsAsync<Day>();
			}
			return day;
		}
	}
}
