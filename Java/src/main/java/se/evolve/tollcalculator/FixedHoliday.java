package se.evolve.tollcalculator;

import java.time.LocalDate;

public enum FixedHoliday {
	NEW_YEARS_DAY(1, 1), BEFORE_EPIPHANY(1, 5), EPIPHANY(1, 6), WALPURGIS(4, 30), MAY_FIRST(5, 1),
	BEFORE_NATIONAL_DAY(6, 5), NATIONAL_DAY(6, 6), CHRISTMAS_EVE(12, 24), CHRISTMAS_DAY(12, 25), BOXING_DAY(12, 26),
	NEW_YEARS_EVE(12, 31);

	private int month;
	private int day;

	private FixedHoliday(int month, int day) {
		this.month = month;
		this.day = day;
	}

	public LocalDate getDate(int year) {
		return LocalDate.of(year, month, day);
	}
}
