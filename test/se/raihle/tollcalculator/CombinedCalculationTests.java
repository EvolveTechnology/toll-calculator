package se.raihle.tollcalculator;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Disabled;
import org.junit.jupiter.api.Test;
import se.raihle.tollcalculator.test.CalendarStream;

import java.util.Calendar;
import java.util.Collections;
import java.util.Date;
import java.util.List;
import java.util.stream.Collectors;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;
import static se.raihle.tollcalculator.test.CalendarBuilder.regularDayAt;

class CombinedCalculationTests {
	private static final Vehicle REGULAR_CAR = new Car();

	private static final int DAILY_RATE = 60;

	private TollCalculator unit;

	@BeforeEach
	void setup() {
		unit = new TollCalculator();
	}

	@Test
	void car_passing_two_times_within_an_hour_is_only_charged_the_higher_rate() {
		CalendarStream.from(regularDayAt(0, 0), Calendar.MINUTE, 15).limit(4 * 24).forEach(startingPoint -> {
			List<Calendar> timesOfPassing = CalendarStream.takeAsList(2, startingPoint, Calendar.MINUTE, 30);
			if (timesAreOnSameDay(timesOfPassing)) {
				int expectedFee = highestFeeAmong(timesOfPassing, REGULAR_CAR);
				assertEquals(expectedFee, totalFeeFor(timesOfPassing, REGULAR_CAR), errorAt(timesOfPassing.get(0)));
			}
		});
	}

	@Test
	void car_passing_three_times_within_an_hour_is_only_charged_the_highest_rate() {
		CalendarStream.from(regularDayAt(0, 0), Calendar.MINUTE, 15).limit(4 * 24).forEach(startingPoint -> {
			List<Calendar> timesOfPassing = CalendarStream.takeAsList(3, startingPoint, Calendar.MINUTE, 20);
			if (timesAreOnSameDay(timesOfPassing)) {
				int expectedFee = highestFeeAmong(timesOfPassing, REGULAR_CAR);
				assertEquals(expectedFee, totalFeeFor(timesOfPassing, REGULAR_CAR), errorAt(timesOfPassing.get(0)));
			}
		});
	}

	@Test
	void car_passing_twice_per_hour_for_two_hours_is_charged_the_sum_of_higher_rates_for_each_hour() {
		CalendarStream.from(regularDayAt(0, 0), Calendar.MINUTE, 15).limit(4 * 24).forEach(startingPoint -> {
			List<Calendar> timesOfPassing = CalendarStream.takeAsList(4, startingPoint, Calendar.MINUTE, 30);
			List<Calendar> passingsInFirstHour = timesOfPassing.subList(0, 2);
			List<Calendar> passingsInSecondHour = timesOfPassing.subList(2, 4);
			if (timesAreOnSameDay(timesOfPassing)) {
				int expectedFee = highestFeeAmong(passingsInFirstHour, REGULAR_CAR) + highestFeeAmong(passingsInSecondHour, REGULAR_CAR);
				assertEquals(expectedFee, totalFeeFor(timesOfPassing, REGULAR_CAR), errorAt(timesOfPassing.get(0)));
			}
		});
	}

	@Test
	void car_passing_twice_per_hour_for_two_hours_is_charged_the_sum_of_higher_rates_for_each_hour_if_times_are_out_of_order() {
		CalendarStream.from(regularDayAt(0, 0), Calendar.MINUTE, 15).limit(4 * 24).forEach(startingPoint -> {
			List<Calendar> timesOfPassing = CalendarStream.takeAsList(4, startingPoint, Calendar.MINUTE, 30);
			List<Calendar> passingsInFirstHour = timesOfPassing.subList(0, 2);
			List<Calendar> passingsInSecondHour = timesOfPassing.subList(2, 4);
			Collections.reverse(timesOfPassing);
			if (timesAreOnSameDay(timesOfPassing)) {
				int expectedFee = highestFeeAmong(passingsInFirstHour, REGULAR_CAR) + highestFeeAmong(passingsInSecondHour, REGULAR_CAR);
				assertEquals(expectedFee, totalFeeFor(timesOfPassing, REGULAR_CAR), errorAt(timesOfPassing.get(0)));
			}
		});
	}

	@Test
	void car_passing_once_per_hour_for_a_day_is_only_billed_the_daily_rate() {
		List<Calendar> timesOfPassing = CalendarStream.takeAsList(24, regularDayAt(0, 30), Calendar.HOUR_OF_DAY, 1);

		int fee = unit.getTollFee(REGULAR_CAR, calendarsToDates(timesOfPassing));
		assertEquals(DAILY_RATE, fee);
	}

	@Test
	void calculating_tolls_for_two_days_at_once_throws_an_IllegalArgumentException() {
		List<Calendar> timesOfPassing = CalendarStream.takeAsList(48, regularDayAt(0, 30), Calendar.HOUR_OF_DAY, 1);

		assertThrows(IllegalArgumentException.class, () -> unit.getTollFee(REGULAR_CAR, calendarsToDates(timesOfPassing)));
	}


	/**
	 * Gets the highest individual fee for any of the given passings
	 */
	private int highestFeeAmong(List<Calendar> timesOfPassing, Vehicle passer) {
		return timesOfPassing.stream()
				.mapToInt(time -> unit.getTollFee(time.getTime(), passer))
				.max()
				.orElse(0);
	}

	/**
	 * Gets the total fee for the passings as a group
	 */
	private int totalFeeFor(List<Calendar> timesOfPassing, Vehicle passer) {
		return unit.getTollFee(passer, calendarsToDates(timesOfPassing));
	}

	/**
	 * Most tests are not concerned with cases when the vehicle passes on multiple days, so we want to filter those out
	 * @param timesofpassing times of passing sorted from first to last
	 */
	private boolean timesAreOnSameDay(List<Calendar> timesofpassing) {
		Calendar first = timesofpassing.get(0);
		Calendar last = timesofpassing.get(timesofpassing.size() - 1);
		return areSame(first, last, Calendar.YEAR) && areSame(first, last, Calendar.DAY_OF_YEAR);
	}

	/**
	 * Checks that the two given times have the same value for the given unit.
	 * The unit is <em>not</em> a minimum granularity - e.g. this function will ignore hours if asked to check minutes.
	 */
	private boolean areSame(Calendar a, Calendar b, int unit) {
		return a.get(unit) == b.get(unit);
	}

	/**
	 * Converts a List of Calendars to an array of Dates
	 */
	private static Date[] calendarsToDates(List<Calendar> calendars) {
		return calendars.stream().map(Calendar::getTime).collect(Collectors.toList()).toArray(new Date[calendars.size()]);
	}

	/**
	 * Builds an error message out of the given calendar object
	 */
	private String errorAt(Calendar calendar) {
		return "Wrong fee given for sequence starting at " + calendar.getTime();
	}
}