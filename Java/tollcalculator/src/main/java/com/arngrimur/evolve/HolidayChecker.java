package com.arngrimur.evolve;

import java.time.LocalDate;
import java.time.Month;

/**
 * This is just a mock class. Preferably we use some library or service for
 * this.
 */
public class HolidayChecker {

	/** 
	 * In this universe we only have one public holiday, the 13th of November.
	 * @param date
	 * @return
	 */
	public Boolean isPublicHoliday(LocalDate date) {
		LocalDate holiday = LocalDate.of(LocalDate.now().getDayOfYear(), Month.NOVEMBER, 13);
		return holiday.equals(date);
	}
	
}