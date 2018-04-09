package se.raihle.tollcalculator;

import java.util.Calendar;

public class Car implements Vehicle {
	@Override
	public int getTollAt(Calendar timeOfPassing) {
		if (isTollFreeDate(timeOfPassing)) {
			return 0;
		}

		int hour = timeOfPassing.get(Calendar.HOUR_OF_DAY);
		int minute = timeOfPassing.get(Calendar.MINUTE);

		if (hour == 6 && minute >= 0 && minute <= 29) return 8;
		else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
		else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
		else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
		else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
		else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
		else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
		else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
		else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
		else return 0;
	}

	private Boolean isTollFreeDate(Calendar timeOfPassing) {
		int year = timeOfPassing.get(Calendar.YEAR);
		int month = timeOfPassing.get(Calendar.MONTH);
		int day = timeOfPassing.get(Calendar.DAY_OF_MONTH);

		int dayOfWeek = timeOfPassing.get(Calendar.DAY_OF_WEEK);
		if (dayOfWeek == Calendar.SATURDAY || dayOfWeek == Calendar.SUNDAY) return true;

		if (year == 2013) {
			if (month == Calendar.JANUARY && day == 1 ||
					month == Calendar.MARCH && (day == 28 || day == 29) ||
					month == Calendar.APRIL && (day == 1 || day == 30) ||
					month == Calendar.MAY && (day == 1 || day == 8 || day == 9) ||
					month == Calendar.JUNE && (day == 5 || day == 6 || day == 21) ||
					month == Calendar.JULY ||
					month == Calendar.NOVEMBER && day == 1 ||
					month == Calendar.DECEMBER && (day == 24 || day == 25 || day == 26 || day == 31)) {
				return true;
			}
		}
		return false;
	}
}
