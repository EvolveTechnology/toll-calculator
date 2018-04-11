package se.raihle.tollcalculator.schedule;

import org.junit.jupiter.api.Test;

import java.io.InputStream;
import java.time.LocalDate;
import java.time.Month;

import static org.junit.jupiter.api.Assertions.*;

class HolidayScheduleParserTest {

	@Test
	void parsing_a_string_of_dates_creates_a_holiday_schedule_with_those_dates() {
		String dates = "2018-01-01 2018-06-15 2018-07-25";
		HolidaySchedule holidays = HolidayScheduleParser.fromString(dates);

		assertTrue(holidays.isHoliday(LocalDate.of(2018, Month.JANUARY, 1)));
		assertTrue(holidays.isHoliday(LocalDate.of(2018, Month.JUNE, 15)));
		assertTrue(holidays.isHoliday(LocalDate.of(2018, Month.JULY, 25)));
	}

	@Test
	void parsing_a_file_of_dates_creates_a_holiday_schedule_with_those_dates() {
		System.out.println(HolidayScheduleParserTest.class.getResource(".").getPath());
		InputStream dates = HolidayScheduleParserTest.class.getResourceAsStream("/test-holidays.txt");
		assertNotNull(dates);
		HolidaySchedule holidays = HolidayScheduleParser.fromInputStream(dates);

		assertTrue(holidays.isHoliday(LocalDate.of(2018, Month.APRIL, 16)));
		assertTrue(holidays.isHoliday(LocalDate.of(2018, Month.AUGUST, 24)));
		assertTrue(holidays.isHoliday(LocalDate.of(2018, Month.DECEMBER, 17)));
	}
}