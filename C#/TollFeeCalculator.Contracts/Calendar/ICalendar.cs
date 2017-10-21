using System;

namespace TollFeeCalculator.Contracts.Calendar
{
	/// <summary>
	/// Calendar intefrace
	/// </summary>
	public interface ICalendar
	{
		/// <summary>
		/// Determine whether the date is toll free
		/// </summary>
		bool IsDateTollFree(DateTime date);

	}
}
