package se.raihle.tollcalculator;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Disabled;
import org.junit.jupiter.api.Test;
import se.raihle.tollcalculator.schedule.HolidaySchedule;
import se.raihle.tollcalculator.schedule.HolidayScheduleParser;
import se.raihle.tollcalculator.test.CalendarBuilder;

import java.util.Calendar;

import static org.junit.jupiter.api.Assertions.assertEquals;

class IndividualCalculationTests {
	private static final Vehicle REGULAR_CAR = new Car();

	private static final int FREE_RATE = 0;
	private static final int LOW_RATE = 8;
	private static final int MEDIUM_RATE = 13;
	private static final int HIGH_RATE = 18;

	private TollCalculator unit;

	@BeforeEach
	void setup() {
		HolidaySchedule holidays = HolidayScheduleParser.fromInputStream(this.getClass().getResourceAsStream("/2018-holidays.txt"));
		unit = new TollCalculator(holidays);
	}

	@Test
	void car_pays_nothing_for_passing_before_six() {
		Calendar startOfDay = CalendarBuilder.regularDayAt(0, 0);
		Calendar six = CalendarBuilder.regularDayAt(6, 0);

		checkFeeBetween(unit, REGULAR_CAR, FREE_RATE, startOfDay, six);
	}

	@Test
	void car_pays_low_rate_between_six_and_six_thirty() {
		Calendar six = CalendarBuilder.regularDayAt(6, 0);
		Calendar sixThirty = CalendarBuilder.regularDayAt(6, 30);

		checkFeeBetween(unit, REGULAR_CAR, LOW_RATE, six, sixThirty);
	}

	@Test
	void car_pays_medium_rate_between_six_thirty_and_seven() {
		Calendar sixThirty = CalendarBuilder.regularDayAt(6, 30);
		Calendar seven = CalendarBuilder.regularDayAt(7, 0);

		checkFeeBetween(unit, REGULAR_CAR, MEDIUM_RATE, sixThirty, seven);
	}

	@Test
	void car_pays_high_rate_between_seven_and_eight() {
		Calendar seven = CalendarBuilder.regularDayAt(7, 0);
		Calendar eight = CalendarBuilder.regularDayAt(8, 0);

		checkFeeBetween(unit, REGULAR_CAR, HIGH_RATE, seven, eight);
	}

	@Test
	void car_pays_medium_rate_between_eight_and_eight_thirty() {
		Calendar eight = CalendarBuilder.regularDayAt(8, 0);
		Calendar eightThirty = CalendarBuilder.regularDayAt(8, 30);

		checkFeeBetween(unit, REGULAR_CAR, MEDIUM_RATE, eight, eightThirty);
	}

	@Test
	void car_pays_low_rate_between_eight_thirty_and_fifteen() {
		Calendar eightThirty = CalendarBuilder.regularDayAt(8, 30);
		Calendar fifteen = CalendarBuilder.regularDayAt(15, 0);

		checkFeeBetween(unit, REGULAR_CAR, LOW_RATE, eightThirty, fifteen);
	}

	@Test
	void car_pays_medium_rate_between_fifteen_and_fifteen_thirty() {
		Calendar fifteen = CalendarBuilder.regularDayAt(15, 0);
		Calendar fifteenThirty = CalendarBuilder.regularDayAt(15, 30);

		checkFeeBetween(unit, REGULAR_CAR, MEDIUM_RATE, fifteen, fifteenThirty);
	}

	@Test
	void car_pays_high_rate_between_fifteen_thirty_and_seventeen() {
		Calendar fifteenThirty = CalendarBuilder.regularDayAt(15, 30);
		Calendar seventeen = CalendarBuilder.regularDayAt(17, 0);

		checkFeeBetween(unit, REGULAR_CAR, HIGH_RATE, fifteenThirty, seventeen);
	}

	@Test
	void car_pays_medium_rate_between_seventeen_and_eighteen() {
		Calendar seventeen = CalendarBuilder.regularDayAt(17, 0);
		Calendar eighteen = CalendarBuilder.regularDayAt(18, 0);

		checkFeeBetween(unit, REGULAR_CAR, MEDIUM_RATE, seventeen, eighteen);
	}

	@Test
	void car_pays_low_rate_between_eighteen_and_eighteen_thirty() {
		Calendar eighteen = CalendarBuilder.regularDayAt(18, 0);
		Calendar eighteenThirty = CalendarBuilder.regularDayAt(18, 30);

		checkFeeBetween(unit, REGULAR_CAR, LOW_RATE, eighteen, eighteenThirty);
	}

	@Test
	void car_pays_nothing_after_eighteen_thirty() {
		Calendar eighteenThirty = CalendarBuilder.regularDayAt(18, 30);
		Calendar endOfDay = CalendarBuilder.regularDayAt(24, 0);

		checkFeeBetween(unit, REGULAR_CAR, FREE_RATE, eighteenThirty, endOfDay);
	}

	@Test
	void cars_pass_for_free_on_new_years_day_even_when_it_is_a_weekday() {
		Calendar startOfDay = CalendarBuilder.calendarAt(2018, Calendar.JANUARY, 1, 0, 0);
		Calendar endOfDay = CalendarBuilder.calendarAt(2018, Calendar.JANUARY, 1, 24, 0);

		checkFeeBetween(unit, REGULAR_CAR, FREE_RATE, startOfDay, endOfDay);
	}

	@Test
	void cars_pass_for_free_on_easter_monday() {
		Calendar startOfDay2018 = CalendarBuilder.calendarAt(2018, Calendar.APRIL, 2, 0, 0);
		Calendar endOfDay2018 = CalendarBuilder.calendarAt(2018, Calendar.APRIL, 2, 24, 0);

		checkFeeBetween(unit, REGULAR_CAR, FREE_RATE, startOfDay2018, endOfDay2018);

		Calendar startOfDay2019 = CalendarBuilder.calendarAt(2018, Calendar.APRIL, 22, 0, 0);
		Calendar endOfDay2019 = CalendarBuilder.calendarAt(2018, Calendar.APRIL, 22, 24, 0);

		checkFeeBetween(unit, REGULAR_CAR, FREE_RATE, startOfDay2019, endOfDay2019);
	}

	@Test
	void cars_pass_for_free_on_weekends() {
		Calendar startOfDay = CalendarBuilder.weekendAt(0, 0);
		Calendar endOfDay = CalendarBuilder.weekendAt(24, 0);

		checkFeeBetween(unit, REGULAR_CAR, FREE_RATE, startOfDay, endOfDay);
	}

	@Test
	void motorbikes_pass_for_free() {
		Calendar startOfYear = CalendarBuilder.calendarAt(2018, Calendar.JANUARY, 1, 0, 0);
		Calendar startOfNextYear = CalendarBuilder.calendarAt(2019, Calendar.JANUARY, 1, 0, 0);

		checkFeeBetween(unit, new Motorbike(), FREE_RATE, startOfYear, startOfNextYear);
	}

	/**
	 * Checks the fee for every minute between the two given points, asserting that it is equal to the expected fee
	 *
	 * @param unit           the calculator being tested
	 * @param passer         the vehicle passing
	 * @param expectedFee    the expected result
	 * @param startInclusive the first time to check
	 * @param endExclusive   the first time to not check (after the start time)
	 */
	private void checkFeeBetween(TollCalculator unit, Vehicle passer, int expectedFee, Calendar startInclusive, Calendar endExclusive) {
		if (!endExclusive.after(startInclusive)) {
			throw new IllegalArgumentException("End time must be after start time");
		}

		Calendar timeOfPassing = Calendar.getInstance();
		timeOfPassing.setTime(startInclusive.getTime());
		while (timeOfPassing.before(endExclusive)) {
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