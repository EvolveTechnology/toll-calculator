package se.raihle.tollcalculator.schedule;

import java.time.LocalDate;
import java.util.Calendar;
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

	public boolean isHoliday(Calendar date) {
		// Calendar's months are 0-based, LocalDate's are 1-based
		return isHoliday(LocalDate.of(date.get(Calendar.YEAR), 1 + date.get(Calendar.MONTH), date.get(Calendar.DAY_OF_MONTH)));
	}
}
