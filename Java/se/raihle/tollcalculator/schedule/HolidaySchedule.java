package se.raihle.tollcalculator.schedule;

import java.time.LocalDate;
import java.util.Collections;
import java.util.List;

public class HolidaySchedule {
	private final List<LocalDate> dates;

	public HolidaySchedule(List<LocalDate> holidays) {
		dates = Collections.unmodifiableList(holidays);
	}

	public boolean isHoliday(LocalDate date) {
		return dates.contains(date);
	}
}
