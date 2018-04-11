package se.raihle.tollcalculator.schedule;

import java.time.LocalDate;
import java.util.Collections;
import java.util.List;

/**
 * Keep track of toll-free holidays.
 * Create one by collecting LocalDates in a list or read one from file with a {@link HolidayScheduleParser}.
 */
public class HolidaySchedule {
	private final List<LocalDate> dates;

	public HolidaySchedule(List<LocalDate> holidays) {
		dates = Collections.unmodifiableList(holidays);
	}

	public boolean isHoliday(LocalDate date) {
		return dates.contains(date);
	}
}
