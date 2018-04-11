package se.raihle.tollcalculator;

import se.raihle.tollcalculator.schedule.FeeSchedule;
import se.raihle.tollcalculator.schedule.FeeScheduleBuilder;

import java.time.LocalTime;

public class Car implements Vehicle {

	private static final FeeSchedule FEE_SCHEDULE;


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
	}

	@Override
	public int getTollAt(LocalTime timeOfPassing) {
		return Car.FEE_SCHEDULE.getFeeAt(timeOfPassing);
	}
}
