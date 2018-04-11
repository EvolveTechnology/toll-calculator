package se.raihle.tollcalculator.schedule;

import org.junit.jupiter.api.Test;

import java.io.InputStream;
import java.time.LocalDate;
import java.time.Month;

import static org.junit.jupiter.api.Assertions.*;

class HolidayScheduleParserTest {
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