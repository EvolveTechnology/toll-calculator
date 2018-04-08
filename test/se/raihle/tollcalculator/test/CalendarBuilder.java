package se.raihle.tollcalculator.test;

import java.util.Calendar;

public class CalendarBuilder {
	/**
	 * Creates a calendar representing a regular day (always the same) at the given hour and minute
	 */
	public static Calendar regularDayAt(int hour, int minute) {
		return calendarAt(2018, Calendar.APRIL, 9, hour, minute);
	}

	/**
	 * Creates a calendar representing a day on the weekend (always the same) at the given hour and minute
	 */
	public static Calendar weekendAt(int hour, int minute) {
		return calendarAt(2018, Calendar.APRIL, 7, hour, minute);
	}

	/**
	 * Creates a calendar at the given date and time with the default timezone and locale
	 */
	public static Calendar calendarAt(int year, int month, int dayOfMonth, int hour, int minute) {
		Calendar result = Calendar.getInstance();
		result.clear();
		result.set(year, month, dayOfMonth, hour, minute);
		return result;
	}
}
