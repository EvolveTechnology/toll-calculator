package com.evolve.service;

import java.time.DayOfWeek;
import java.time.LocalDate;
import java.util.HashMap;
import java.util.Map;

public class FreeDayServiceImpl implements FreeDayService {

	private static final Map<LocalDate, Boolean> holidayMap = new HashMap<>();

	static {
		populateHolidayMap();
	}

	public boolean isFreeDay(LocalDate day) {
		if (day.getDayOfWeek() == DayOfWeek.SATURDAY || day.getDayOfWeek() == DayOfWeek.SUNDAY) {
			return true;
		} else {
			return holidayMap.containsKey(day);
		}
	}

	private static void populateHolidayMap() {
		int year = LocalDate.now().getYear();

		// January:1
		holidayMap.put(LocalDate.of(year, 1, 1), true);

		// March:28,29
		holidayMap.put(LocalDate.of(year, 3, 28), true);
		holidayMap.put(LocalDate.of(year, 3, 29), true);

		// April:1,30
		holidayMap.put(LocalDate.of(year, 4, 1), true);
		holidayMap.put(LocalDate.of(year, 3, 30), true);

		// May:1,8,9
		holidayMap.put(LocalDate.of(year, 5, 1), true);
		holidayMap.put(LocalDate.of(year, 5, 8), true);
		holidayMap.put(LocalDate.of(year, 5, 9), true);

		// June:5,6,21
		holidayMap.put(LocalDate.of(year, 6, 5), true);
		holidayMap.put(LocalDate.of(year, 6, 6), true);
		holidayMap.put(LocalDate.of(year, 6, 21), true);

		// July
		for (int i = 1; i <= 31; i++) {
			holidayMap.put(LocalDate.of(year, 7, i), true);
		}

		// November:1
		holidayMap.put(LocalDate.of(year, 11, 1), true);

		// December:24,25,26,31
		holidayMap.put(LocalDate.of(year, 12, 24), true);
		holidayMap.put(LocalDate.of(year, 12, 25), true);
		holidayMap.put(LocalDate.of(year, 12, 26), true);
		holidayMap.put(LocalDate.of(year, 12, 31), true);
	}

}
