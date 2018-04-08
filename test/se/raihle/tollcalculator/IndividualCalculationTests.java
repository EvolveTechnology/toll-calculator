package se.raihle.tollcalculator;

import org.junit.jupiter.api.Test;

import java.util.Calendar;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static se.raihle.tollcalculator.test.CalendarBuilder.*;

class IndividualCalculationTests {
	private static final Vehicle REGULAR_CAR = new Car();

	private static final int FREE_RATE = 0;
	private static final int LOW_RATE = 8;
	private static final int MEDIUM_RATE = 13;
	private static final int HIGH_RATE = 18;

	@Test
	void car_pays_nothing_for_passing_before_six() {
		TollCalculator unit = new TollCalculator();

		Calendar startOfDay = regularDayAt(0, 0);
		Calendar six = regularDayAt(6, 0);

		checkFeeBetween(unit, REGULAR_CAR, FREE_RATE, startOfDay, six);
	}

	@Test
	void car_pays_low_rate_between_six_and_six_thirty() {
		TollCalculator unit = new TollCalculator();

		Calendar six = regularDayAt(6, 0);
		Calendar sixThirty = regularDayAt(6, 30);

		checkFeeBetween(unit, REGULAR_CAR, LOW_RATE, six, sixThirty);
	}

	@Test
	void car_pays_medium_rate_between_six_thirty_and_seven() {
		TollCalculator unit = new TollCalculator();

		Calendar sixThirty = regularDayAt(6, 30);
		Calendar seven = regularDayAt(7, 0);

		checkFeeBetween(unit, REGULAR_CAR, MEDIUM_RATE, sixThirty, seven);
	}

	@Test
	void car_pays_high_rate_between_seven_and_eight() {
		TollCalculator unit = new TollCalculator();

		Calendar seven = regularDayAt(7, 0);
		Calendar eight = regularDayAt(8, 0);

		checkFeeBetween(unit, REGULAR_CAR, HIGH_RATE, seven, eight);
	}

	@Test
	void car_pays_medium_rate_between_eight_and_eight_thirty() {
		TollCalculator unit = new TollCalculator();

		Calendar eight = regularDayAt(8, 0);
		Calendar eightThirty = regularDayAt(8, 30);

		checkFeeBetween(unit, REGULAR_CAR, MEDIUM_RATE, eight, eightThirty);
	}

	/*
	 * TODO: This test identifies a bug that occurs between 8:30 and 15:00,
	 * which allows cars to pass for free during the first half of each hour.
	 */
	@Test
	void car_pays_low_rate_between_eight_thirty_and_fifteen() {
		TollCalculator unit = new TollCalculator();

		Calendar eightThirty = regularDayAt(8, 30);
		Calendar fifteen = regularDayAt(15, 0);

		checkFeeBetween(unit, REGULAR_CAR, LOW_RATE, eightThirty, fifteen);
	}

	@Test
	void car_pays_medium_rate_between_fifteen_and_fifteen_thirty() {
		TollCalculator unit = new TollCalculator();

		Calendar fifteen = regularDayAt(15, 0);
		Calendar fifteenThirty = regularDayAt(15, 30);

		checkFeeBetween(unit, REGULAR_CAR, MEDIUM_RATE, fifteen, fifteenThirty);
	}

	@Test
	void car_pays_high_rate_between_fifteen_thirty_and_seventeen() {
		TollCalculator unit = new TollCalculator();

		Calendar fifteenThirty = regularDayAt(15, 30);
		Calendar seventeen = regularDayAt(17, 0);

		checkFeeBetween(unit, REGULAR_CAR, HIGH_RATE, fifteenThirty, seventeen);
	}

	@Test
	void car_pays_medium_rate_between_seventeen_and_eighteen() {
		TollCalculator unit = new TollCalculator();

		Calendar seventeen = regularDayAt(17, 0);
		Calendar eighteen = regularDayAt(18, 0);

		checkFeeBetween(unit, REGULAR_CAR, MEDIUM_RATE, seventeen, eighteen);
	}

	@Test
	void car_pays_low_rate_between_eighteen_and_eighteen_thirty() {
		TollCalculator unit = new TollCalculator();

		Calendar eighteen = regularDayAt(18, 0);
		Calendar eighteenThirty = regularDayAt(18, 30);

		checkFeeBetween(unit, REGULAR_CAR, LOW_RATE, eighteen, eighteenThirty);
	}

	@Test
	void car_pays_nothing_after_eighteen_thirty() {
		TollCalculator unit = new TollCalculator();

		Calendar eighteenThirty = regularDayAt(18, 30);
		Calendar endOfDay = regularDayAt(24, 0);

		checkFeeBetween(unit, REGULAR_CAR, FREE_RATE, eighteenThirty, endOfDay);
	}

	/*
	 * TODO: This test identifies a bug where only holidays during 2013 are identified
	 */
	@Test
	void cars_pass_for_free_on_new_years_day_even_when_it_is_a_weekday() {
		TollCalculator unit = new TollCalculator();

		Calendar startOfDay = calendarAt(2018, Calendar.JANUARY, 1, 0, 0);
		Calendar endOfDay = calendarAt(2018, Calendar.JANUARY, 1, 24, 0);

		checkFeeBetween(unit, REGULAR_CAR, FREE_RATE, startOfDay, endOfDay);
	}

	/*
	 * TODO: This test identifies a bug where only holidays during 2013 are identified,
	 * and requires some sophisticated logic (or more likely configurable holidays) to solve
	 */
	@Test
	void cars_pass_for_free_on_easter_monday() {
		TollCalculator unit = new TollCalculator();

		Calendar startOfDay2018 = calendarAt(2018, Calendar.APRIL, 2, 0, 0);
		Calendar endOfDay2018 = calendarAt(2018, Calendar.APRIL, 2, 24, 0);

		checkFeeBetween(unit, REGULAR_CAR, FREE_RATE, startOfDay2018, endOfDay2018);

		Calendar startOfDay2019 = calendarAt(2018, Calendar.APRIL, 22, 0, 0);
		Calendar endOfDay2019 = calendarAt(2018, Calendar.APRIL, 22, 24, 0);

		checkFeeBetween(unit, REGULAR_CAR, FREE_RATE, startOfDay2019, endOfDay2019);
	}

	@Test
	void cars_pass_for_free_on_weekends() {
		TollCalculator unit = new TollCalculator();

		Calendar startOfDay = weekendAt(0, 0);
		Calendar endOfDay = weekendAt(24, 0);

		checkFeeBetween(unit, REGULAR_CAR, FREE_RATE, startOfDay, endOfDay);
	}

	@Test
	void motorbikes_pass_for_free() {
		TollCalculator unit = new TollCalculator();

		Calendar startOfYear = calendarAt(2018, Calendar.JANUARY, 1, 0, 0);
		Calendar startOfNextYear = calendarAt(2019, Calendar.JANUARY, 1, 0, 0);

		checkFeeBetween(unit, new Motorbike(), FREE_RATE, startOfYear, startOfNextYear);
	}

	/**
	 * Checks the fee for every minute between the two given points, asserting that it is equal to the expected fee
	 * @param unit the calculator being tested
	 * @param passer the vehicle passing
	 * @param expectedFee the expected result
	 * @param startInclusive the first time to check
	 * @param endExclusive the first time to not check (after the start time)
	 */
	private void checkFeeBetween(TollCalculator unit, Vehicle passer, int expectedFee, Calendar startInclusive, Calendar endExclusive) {
		if (!endExclusive.after(startInclusive)) {
			throw new IllegalArgumentException("End time must be after start time");
		}

		Calendar timeOfPassing = Calendar.getInstance();
		timeOfPassing.setTime(startInclusive.getTime());
		while(timeOfPassing.before(endExclusive)) {
			int fee = unit.getTollFee(timeOfPassing.getTime(), passer);
			assertEquals(expectedFee, fee, errorAt(timeOfPassing));
			timeOfPassing.add(Calendar.MINUTE, 1);
		}
	}

	/**
	 * Builds an error message out of the given calendar object
	 */
	private String errorAt(Calendar calendar) {
		return "Wrong fee given for " + calendar.getTime();
	}
}