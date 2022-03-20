package com.evolve_technology.calculator.constant;

import java.time.LocalDate;
import java.time.YearMonth;

public enum TollFreeDates {
	JANUARY_1_2013(LocalDate.of(2013,01,01).toString()),
	MARCH_28_2013(LocalDate.of(2013,03,28).toString()),
	MARCH_29_2013(LocalDate.of(2013,03,29).toString()),
	APRIL_1_2013(LocalDate.of(2013,04,01).toString()),
	APRIL_30_2013(LocalDate.of(2013,04,30).toString()),
	MAY_1_2013(LocalDate.of(2013,05,01).toString()),
	MAY_8_2013(LocalDate.of(2013,05,8).toString()),
	MAY_9_2013(LocalDate.of(2013,05,9).toString()),
	JUNE_5_2013(LocalDate.of(2013,06,5).toString()),
	JUNE_6_2013(LocalDate.of(2013,06,6).toString()),
	JUNE_21_2013(LocalDate.of(2013,06,21).toString()),
	JULY(YearMonth.of(2013,07).toString()),
	NOVEMBER_1_2013(LocalDate.of(2013,11,1).toString()),
	DECEMBER_24_2013(LocalDate.of(2013,12,24).toString()),
	DECEMBER_25_2013(LocalDate.of(2013,12,25).toString()),
	DECEMBER_26_2013(LocalDate.of(2013,12,26).toString()),
	DECEMBER_31_2013(LocalDate.of(2013,12,31).toString());
	
	private String date;
	
	private TollFreeDates(String date) {
		this.date=date;
	}
	
	public String getDate() {
		return date;
	}
}
//	 if (month == Calendar.JANUARY && day == 1 ||
//	          month == Calendar.MARCH && (day == 28 || day == 29) ||
//	          month == Calendar.APRIL && (day == 1 || day == 30) ||
//	          month == Calendar.MAY && (day == 1 || day == 8 || day == 9) ||
//	          month == Calendar.JUNE && (day == 5 || day == 6 || day == 21) ||
//	          month == Calendar.JULY ||
//	          month == Calendar.NOVEMBER && day == 1 ||
//	          month == Calendar.DECEMBER && (day == 24 || day == 25 || day == 26 || day == 31)) {
//	        return true;
//}
