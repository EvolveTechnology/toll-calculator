package se.raihle.tollcalculator;

import se.raihle.tollcalculator.schedule.FeeScheduleBuilder;
import se.raihle.tollcalculator.schedule.FeeSchedule;
import se.raihle.tollcalculator.schedule.HolidaySchedule;

import java.time.LocalDate;
import java.time.LocalTime;
import java.time.Month;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Calendar;
import java.util.List;

public class Car implements Vehicle {

	private static final FeeSchedule FEE_SCHEDULE;
	private static final HolidaySchedule HOLIDAY_SCHEDULE;

	static {
		FEE_SCHEDULE = FeeScheduleBuilder
				.start(0)
				.next(LocalTime.of(6, 0), 8)
				.next(LocalTime.of(6, 30), 13)
				.next(LocalTime.of(7, 0), 18)
				.next(LocalTime.of(8, 0), 13)
				.next(LocalTime.of(8, 30), 8)
				.next(LocalTime.of(15, 0), 13)
				.next(LocalTime.of(15, 30), 18)
				.next(LocalTime.of(17, 0), 13)
				.next(LocalTime.of(18, 0), 8)
				.next(LocalTime.of(18, 30), 0)
				.finish();

		List<LocalDate> holidays = new ArrayList<>(Arrays.asList(
				LocalDate.of(2013, Month.JANUARY, 1),
				LocalDate.of(2013, Month.MARCH, 28),
				LocalDate.of(2013, Month.MARCH, 29),
				LocalDate.of(2013, Month.APRIL, 1),
				LocalDate.of(2013, Month.APRIL, 30),
				LocalDate.of(2013, Month.MAY, 1),
				LocalDate.of(2013, Month.MAY, 8),
				LocalDate.of(2013, Month.MAY, 9),
				LocalDate.of(2013, Month.JUNE, 5),
				LocalDate.of(2013, Month.JUNE, 6),
				LocalDate.of(2013, Month.JUNE, 21),
				LocalDate.of(2013, Month.NOVEMBER, 1),
				LocalDate.of(2013, Month.DECEMBER, 24),
				LocalDate.of(2013, Month.DECEMBER, 25),
				LocalDate.of(2013, Month.DECEMBER, 26),
				LocalDate.of(2013, Month.DECEMBER, 31)
		));

		for (int dayOfMonth = 1; dayOfMonth <= 31; dayOfMonth++) {
			holidays.add(LocalDate.of(2013, Month.JULY, dayOfMonth));
		}

		HOLIDAY_SCHEDULE = new HolidaySchedule(holidays);
	}

	@Override
	public int getTollAt(Calendar timeOfPassing) {
		if (isTollFreeDate(timeOfPassing)) {
			return 0;
		}

		return Car.FEE_SCHEDULE.getFeeAt(LocalTime.of(timeOfPassing.get(Calendar.HOUR_OF_DAY), timeOfPassing.get(Calendar.MINUTE)));
	}

	private Boolean isTollFreeDate(Calendar timeOfPassing) {
		int dayOfWeek = timeOfPassing.get(Calendar.DAY_OF_WEEK);
		if (dayOfWeek == Calendar.SATURDAY || dayOfWeek == Calendar.SUNDAY) return true;

		return HOLIDAY_SCHEDULE.isHoliday(timeOfPassing);
	}
}
