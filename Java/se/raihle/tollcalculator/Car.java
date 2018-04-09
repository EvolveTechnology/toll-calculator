package se.raihle.tollcalculator;

import se.raihle.tollcalculator.schedule.Schedule;
import se.raihle.tollcalculator.schedule.ScheduleBuilder;

import java.time.LocalTime;
import java.util.Calendar;

public class Car implements Vehicle {

	private static final Schedule schedule;

	static {
		schedule = ScheduleBuilder
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
	}

	@Override
	public int getTollAt(Calendar timeOfPassing) {
		if (isTollFreeDate(timeOfPassing)) {
			return 0;
		}

		return Car.schedule.getFeeAt(LocalTime.of(timeOfPassing.get(Calendar.HOUR_OF_DAY), timeOfPassing.get(Calendar.MINUTE)));
	}

	private Boolean isTollFreeDate(Calendar timeOfPassing) {
		int year = timeOfPassing.get(Calendar.YEAR);
		int month = timeOfPassing.get(Calendar.MONTH);
		int day = timeOfPassing.get(Calendar.DAY_OF_MONTH);

		int dayOfWeek = timeOfPassing.get(Calendar.DAY_OF_WEEK);
		if (dayOfWeek == Calendar.SATURDAY || dayOfWeek == Calendar.SUNDAY) return true;

		if (year == 2013) {
			if (month == Calendar.JANUARY && day == 1 ||
					month == Calendar.MARCH && (day == 28 || day == 29) ||
					month == Calendar.APRIL && (day == 1 || day == 30) ||
					month == Calendar.MAY && (day == 1 || day == 8 || day == 9) ||
					month == Calendar.JUNE && (day == 5 || day == 6 || day == 21) ||
					month == Calendar.JULY ||
					month == Calendar.NOVEMBER && day == 1 ||
					month == Calendar.DECEMBER && (day == 24 || day == 25 || day == 26 || day == 31)) {
				return true;
			}
		}
		return false;
	}
}
