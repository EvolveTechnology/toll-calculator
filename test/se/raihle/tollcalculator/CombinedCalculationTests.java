package se.raihle.tollcalculator;

import org.junit.jupiter.api.Disabled;
import org.junit.jupiter.api.Test;

import java.util.Calendar;
import java.util.Date;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static se.raihle.tollcalculator.test.CalendarBuilder.regularDayAt;

class CombinedCalculationTests {
	private static final Vehicle REGULAR_CAR = new Car();

	private static final int DAILY_RATE = 60;

	@Test
	void car_passing_two_times_within_an_hour_is_only_charged_the_higher_rate() {
		TollCalculator unit = new TollCalculator();

		for (int hour = 0; hour < 24; hour++) {
			for (int minute = 0; minute < 60; minute += 15) {
				Calendar firstPassing = regularDayAt(hour, minute);
				Calendar secondPassing = regularDayAt(hour, minute + 30);

				int firstFee = unit.getTollFee(firstPassing.getTime(), REGULAR_CAR);
				int secondFee = unit.getTollFee(secondPassing.getTime(), REGULAR_CAR);

				int expectedFee = Math.max(firstFee, secondFee);
				int totalFee = unit.getTollFee(REGULAR_CAR, firstPassing.getTime(), secondPassing.getTime());

				assertEquals(expectedFee, totalFee, errorAt(firstPassing));
			}
		}
	}

	/*
	 * TODO: Identifies a bug when a car is charged multiple times if it passes more than twice per hour
	 */
	@Test
	@Disabled("Known issue")
	void car_passing_three_times_within_an_hour_is_only_charged_the_highest_rate() {
		TollCalculator unit = new TollCalculator();

		for (int hour = 0; hour < 24; hour++) {
			for (int minute = 0; minute < 60; minute += 15) {
				Calendar firstPassing = regularDayAt(hour, minute);
				Calendar secondPassing = regularDayAt(hour, minute + 20);
				Calendar thirdPassing = regularDayAt(hour, minute + 40);

				int firstFee = unit.getTollFee(firstPassing.getTime(), REGULAR_CAR);
				int secondFee = unit.getTollFee(secondPassing.getTime(), REGULAR_CAR);
				int thirdFee = unit.getTollFee(thirdPassing.getTime(), REGULAR_CAR);

				int expectedFee = Math.max(Math.max(firstFee, secondFee), thirdFee);
				int totalFee = unit.getTollFee(REGULAR_CAR, firstPassing.getTime(), secondPassing.getTime(), thirdPassing.getTime());

				assertEquals(expectedFee, totalFee, errorAt(firstPassing));
			}
		}
	}

	@Test
	void car_passing_once_per_hour_for_a_day_is_only_billed_the_daily_rate() {
		TollCalculator unit = new TollCalculator();

		Date[] passings = new Date[24];
		for (int hour = 0; hour < 24; hour++) {
			Calendar timeOfPassing = regularDayAt(hour, 30);
			passings[hour] = timeOfPassing.getTime();
		}

		int fee = unit.getTollFee(REGULAR_CAR, passings);
		assertEquals(DAILY_RATE, fee);
	}

	/*
	 * TODO: Identifies unintuitive behavior (though it is consistent with the specification)
	 * (getTollFee only handles passings during a single day)
	 */
	@Test
	@Disabled("Known issue")
	void car_passing_once_per_hour_for_two_days_is_billed_double_the_daily_rate() {
		TollCalculator unit = new TollCalculator();

		Date[] passings = new Date[48];
		for (int hour = 0; hour < 48; hour++) {
			Calendar timeOfPassing = regularDayAt(hour, 30);
			passings[hour] = timeOfPassing.getTime();
		}

		int fee = unit.getTollFee(REGULAR_CAR, passings);
		assertEquals(DAILY_RATE * 2, fee);
	}

	/**
	 * Builds an error message out of the given calendar object
	 */
	private String errorAt(Calendar calendar) {
		return "Wrong fee given for sequence starting at " + calendar.getTime();
	}
}