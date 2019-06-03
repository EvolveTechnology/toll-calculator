package se.evolve.tollcalculator;

import java.time.LocalDate;

import com.google.common.annotations.VisibleForTesting;

public enum EasterHoliday {
	MAUNDY_THURSDAY(-3), GOOD_FRIDAY(-2), EASTER(0), EASTER_MONDAY(1), BEFORE_ASCENSION(+38), ASCENSION(+39);
	
	private int easterOffset;
	
	private EasterHoliday(int easterOffset) {
		this.easterOffset = easterOffset;
	}

	public LocalDate getDate(int year) {
		return getEaster(year).plusDays(easterOffset);
	}
	
	/**
	 * Computus/Meeus Gregorian algorithm
	 */
	@VisibleForTesting
	protected LocalDate getEaster(int year) {
		int a = year % 19;
		int b = year / 100;
		int c = year % 100;
		int d = b / 4;
		int e = b % 4;
		int f = (b + 8) / 25;
		int g = (b - f + 1) / 3;
		int h = (19 * a + b - d - g + 15) % 30;
		int i = c / 4;
		int k = c % 4;
		int L = (32 + 2 * e + 2 * i - h - k) % 7;
		int m = (a + 11 * h + 22 * L) / 451;
		int month = (h + L - 7 * m + 114) / 31;
		int day = ((h + L - 7 * m + 114) % 31) + 1;
		return LocalDate.of(year, month, day);
	}
}
