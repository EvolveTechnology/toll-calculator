using Nager.Date;
using System;
using System.Collections.Generic;
using System.Linq;

public static class TollCalculator
{

	/** 
	 * KLASSER FÖR OLIKA FORDONSTYPER:
	 * Jag har tagit bort klasserna vehicle, car och motorbike. Såg ingen mening med att ha kvar dem eftersom de bara returnerade 
	 * fordonstyp. Det enda som jag behöver veta är om fordonet behöver betala vägtull eller inte. 
	 * 
	 * Jag har en enum för alla fordonstyper och en lista för alla fordon som ej ska betala vägtull. 
	 * Tänkte att jag kollar mot denna lista ifall ett fordon ska betala eller inte. 
	 * På så sätt kan jag skicka in fordon som enum istället för strängar till metoderna.
	 * 
	 * 
	 * METODER:
	 * 
	 * 'GetTollFee(Vehicle vehicle, DateTime[] dates)': 
	 * 
	 * Parametern 'tempFee' sparar senast adderad vägtull. Lyfte ur denna ur loopen eftersom den inte behöver hämtas varje gång. 
	 * Senaste vägtull sparas alltid här i så det finns inför nästa varv. 
	 * 
	 * Parametern 'previousDate' sparar senast kontrollerade datum för att kunna jämföra om det finns flera passerade vägtullar under samma timma. 
	 * 
	 * I loopen går jag igenom varje datum för att se hur mycket som ska adderas.  
	 * Jag kollar tiden för nuvarande vägtull mot den senaste för att se om de är inom samma tidsintervall.  
	 * Här har jag räknat från början av en timme fram till nästa, ex från 7.00-8.00. 
	 * Om de är inom samma tidsintervall så kollar jag om det har sparats någon tidigare vägtull, isåfall tar jag bort den senaste från totalen. 
	 * Sedan kollar jag om nuvarande hämtade vägtull är större än den innan, isåfall sparar jag den som temp. 
	 * Om nuvarande vägtull inte är inom samma tidsintervall som den innan så sparar jag nuvarande som temp så den kan adderas till totalen sedan. 
	 * 
	 * Efter loopen kollar jag om totalen är större än 60, isåfall ska 60 returneras.
	 * 
	 * 
	 * 'GetTollFee(date)': 
	 * Jag tyckte inte att tiderna stämde riktigt på vissa ställen. Ändrade till hur jag tror att det ska vara. 
	 * 
	 * Gäller dessa ställen: 
	 * 1. else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
	 * Så som det står nu verkar det endast vara tider varje timma från kl 8-14 där minuterna är mellan x.30 - x.59,  
	 * men jag tror att det ska vara all tid mellan 8.30-14.59.  
	 * 
	 * 2. else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18; 
	 * Denna är överlappande med tiden innan (15.00-15.29). Så jag tror att det ska vara 15.30 här istället.
	 */


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