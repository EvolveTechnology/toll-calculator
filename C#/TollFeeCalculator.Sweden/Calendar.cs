using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public bool IsDateTollFree(DateTime date)
		{
			//Saturdays, Sundays and whole July are toll free
			return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || date.Month == 7 ||
				//Public holidays are toll free
				!string.IsNullOrWhiteSpace(GetDayAsync(_apiPathAndQuery + date.ToString("yyyyMMdd")).Result.Helgdag) ||
				//Days before public holiday are toll free
				!string.IsNullOrWhiteSpace(GetDayAsync(_apiPathAndQuery + date.AddDays(1).ToString("yyyyMMdd")).Result.Helgdag);
		}

		private async Task<Day> GetDayAsync(string path)
		{
			HttpResponseMessage response = await _client.GetAsync(path);
			if (!response.IsSuccessStatusCode) return null;
			var responseString = await response.Content.ReadAsStringAsync();
			var day = JsonConvert.DeserializeObject<Day>(responseString);
			return day;
		}
	}
}
