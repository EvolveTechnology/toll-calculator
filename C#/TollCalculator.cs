using Nager.Date;
using System;
using System.Collections.Generic;
using System.Linq;

public static class TollCalculator
{
	/**
	 * Calculate the total toll fee for one day
	 *
	 * @param vehicle - the vehicle
	 * @param dates - date and time of all passes on one day
	 * @return - the total toll fee for that day
	 */

	public enum Vehicles
	{
		Motorbike = 0,
		Tractor = 1,
		Emergency = 2,
		Diplomat = 3,
		Foreign = 4,
		Military = 5,
		Car = 6
	}

	public static List<string> TollFreeVehicles = new List<string>()
	{
		"Motorbike",
		"Tractor",
		"Emergency",
		"Diplomat",
		"Foreign",
		"Military"
	};

	public static int GetTollFee(Vehicles vehicle, List<DateTime> dates)
	{
		//No need to go through the dates if the vehicle is toll free
		if (IsTollFreeVehicle(vehicle))
			return 0;

		//Sort ascending
		dates.OrderBy(x => x.ToString());

		int totalFee = 0,
			 tempFee = 0; //Param for the latest added fee
		DateTime previousDate = dates[0]; //Param for the last checked date

		foreach (DateTime date in dates)
		{
			int currentFee = GetTollFee(date);

			if (previousDate.Hour == date.Hour) //Multiple fees in the same hour period
			{
				if (totalFee > 0)
					totalFee -= tempFee; //Remove last added fee from total

				//Save current fee in temp if it's higher than the last added fee
				if (currentFee > tempFee)
					tempFee = currentFee;
			}
			else //One fee in the same hour period
				tempFee = currentFee; //Save current fee

			totalFee += tempFee; //Add toll fee to total
			previousDate = date; //Save this date so it can be compared to the next one
		}

		if (totalFee > 60) //Maximum fee for a day is 60
			totalFee = 60;

		return totalFee;
	}

	public static int GetTollFee(DateTime date)
	{
		if (IsTollFreeDate(date))
			return 0;

		if (IsTollFeeEight(date))
			return 8;

		if (IsTollFeeThirteen(date))
			return 13;

		if (IsTollFeeEighteen(date))
			return 18;

		return 0;
	}

	public static bool IsTollFeeEight(DateTime date)
	{
		if (TimeIsBetween(date, new TimeSpan(6, 0, 0), new TimeSpan(6, 29, 0)) //6.00 - 6.29
			 || TimeIsBetween(date, new TimeSpan(8, 30, 0), new TimeSpan(14, 59, 0)) //8.30 - 14.59
			 || TimeIsBetween(date, new TimeSpan(18, 0, 0), new TimeSpan(18, 29, 0))) //18.00 - 18.29
			return true;

		return false;
	}

	public static bool IsTollFeeThirteen(DateTime date)
	{
		if (TimeIsBetween(date, new TimeSpan(6, 30, 0), new TimeSpan(6, 59, 0)) //18.00 - 18.29
			|| TimeIsBetween(date, new TimeSpan(8, 0, 0), new TimeSpan(8, 29, 0)) //8.00 - 8.29
			|| TimeIsBetween(date, new TimeSpan(15, 0, 0), new TimeSpan(15, 29, 0)) //15.00 - 15.29
			|| TimeIsBetween(date, new TimeSpan(17, 0, 0), new TimeSpan(17, 59, 0))) //17.00 - 17.59
			return true;

		return false;
	}

	public static bool IsTollFeeEighteen(DateTime date)
	{
		if (TimeIsBetween(date, new TimeSpan(7, 0, 0), new TimeSpan(7, 59, 0)) //7.00 - 7.59
			|| TimeIsBetween(date, new TimeSpan(15, 30, 0), new TimeSpan(16, 59, 0))) //15.30 - 16.59
			return true;

		return false;
	}

	/**
	 * Kollar om fordonet inte behöver betala vägtull genom att kolla om det finns med i listan 'TollFreeVehicles'
	 */
	public static bool IsTollFreeVehicle(Vehicles vehicle)
	{
		string vehicleName = Enum.GetName(typeof(Vehicles), vehicle);

		//Check if the vehicle exist in the defined list for toll free vehicles
		string result = TollFreeVehicles.FirstOrDefault(x => x == vehicleName); //Using LINQ 

		if (result != null)
			return true; //Vehicle is toll free
		else
			return false;
	}

	/**
	 * Kollar om tiden för vägtullen är inom ett visst intervall
	 */
	public static bool TimeIsBetween(DateTime now, TimeSpan start, TimeSpan end)
	{
		TimeSpan time = now.TimeOfDay;
		return time >= start && time <= end;
	}

	/**
	 * Kollar om det är helg eller en helgdag, isåfall behöver ingen vägtull betalas
	 */
	public static bool IsTollFreeDate(DateTime date)
	{
		if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
			return true;

		//Use nager.date to check if a date is a holiday
		if (DateSystem.IsPublicHoliday(date, CountryCode.SE))
			return true;

		return false;
	}
}