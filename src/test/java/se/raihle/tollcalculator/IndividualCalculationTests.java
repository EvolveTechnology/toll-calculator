package se.raihle.tollcalculator;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import se.raihle.tollcalculator.schedule.FeeSchedule;
import se.raihle.tollcalculator.schedule.FeeScheduleParser;
import se.raihle.tollcalculator.schedule.HolidaySchedule;
import se.raihle.tollcalculator.schedule.HolidayScheduleParser;

import java.time.LocalDateTime;
import java.time.Month;

import static org.junit.jupiter.api.Assertions.assertEquals;

class IndividualCalculationTests {
	private static final int FREE_RATE = 0;
	private static final int LOW_RATE = 8;
	private static final int MEDIUM_RATE = 13;
	private static final int HIGH_RATE = 18;

	private TollCalculator unit;
	private Vehicle car;
	private Vehicle motorbike;

	@BeforeEach
	void setup() {
		FeeSchedule fees = FeeScheduleParser.fromInputStream(getClass().getResourceAsStream("/regular-fees.txt"));
		HolidaySchedule holidays = HolidayScheduleParser.fromInputStream(this.getClass().getResourceAsStream("/2018-2019-holidays.txt"));
		unit = new TollCalculator(Integer.MAX_VALUE, holidays);
		car = new DefaultVehicle(fees);
		motorbike = new DefaultVehicle(FeeSchedule.FREE);
	}

	@Test
	void car_pays_nothing_for_passing_before_six() {
		LocalDateTime startOfDay = regularDayAt(0, 0);
		LocalDateTime six = regularDayAt(6, 0);

		checkFeeBetween(unit, car, FREE_RATE, startOfDay, six);
	}

	@Test
	void car_pays_low_rate_between_six_and_six_thirty() {
		LocalDateTime six = regularDayAt(6, 0);
		LocalDateTime sixThirty = regularDayAt(6, 30);

		checkFeeBetween(unit, car, LOW_RATE, six, sixThirty);
	}

	@Test
	void car_pays_medium_rate_between_six_thirty_and_seven() {
		LocalDateTime sixThirty = regularDayAt(6, 30);
		LocalDateTime seven = regularDayAt(7, 0);

		checkFeeBetween(unit, car, MEDIUM_RATE, sixThirty, seven);
	}

	@Test
	void car_pays_high_rate_between_seven_and_eight() {
		LocalDateTime seven = regularDayAt(7, 0);
		LocalDateTime eight = regularDayAt(8, 0);

		checkFeeBetween(unit, car, HIGH_RATE, seven, eight);
	}

	@Test
	void car_pays_medium_rate_between_eight_and_eight_thirty() {
		LocalDateTime eight = regularDayAt(8, 0);
		LocalDateTime eightThirty = regularDayAt(8, 30);

		checkFeeBetween(unit, car, MEDIUM_RATE, eight, eightThirty);
	}

	@Test
	void car_pays_low_rate_between_eight_thirty_and_fifteen() {
		LocalDateTime eightThirty = regularDayAt(8, 30);
		LocalDateTime fifteen = regularDayAt(15, 0);

		checkFeeBetween(unit, car, LOW_RATE, eightThirty, fifteen);
	}

	@Test
	void car_pays_medium_rate_between_fifteen_and_fifteen_thirty() {
		LocalDateTime fifteen = regularDayAt(15, 0);
		LocalDateTime fifteenThirty = regularDayAt(15, 30);

		checkFeeBetween(unit, car, MEDIUM_RATE, fifteen, fifteenThirty);
	}

	@Test
	void car_pays_high_rate_between_fifteen_thirty_and_seventeen() {
		LocalDateTime fifteenThirty = regularDayAt(15, 30);
		LocalDateTime seventeen = regularDayAt(17, 0);

		checkFeeBetween(unit, car, HIGH_RATE, fifteenThirty, seventeen);
	}

	@Test
	void car_pays_medium_rate_between_seventeen_and_eighteen() {
		LocalDateTime seventeen = regularDayAt(17, 0);
		LocalDateTime eighteen = regularDayAt(18, 0);

		checkFeeBetween(unit, car, MEDIUM_RATE, seventeen, eighteen);
	}

	@Test
	void car_pays_low_rate_between_eighteen_and_eighteen_thirty() {
		LocalDateTime eighteen = regularDayAt(18, 0);
		LocalDateTime eighteenThirty = regularDayAt(18, 30);

		checkFeeBetween(unit, car, LOW_RATE, eighteen, eighteenThirty);
	}

	@Test
	void car_pays_nothing_after_eighteen_thirty() {
		LocalDateTime eighteenThirty = regularDayAt(18, 30);
		LocalDateTime endOfDay = regularDayAt(23, 59);

		checkFeeBetween(unit, car, FREE_RATE, eighteenThirty, endOfDay);
	}

	@Test
	void cars_pass_for_free_on_new_years_day_even_when_it_is_a_weekday() {
		LocalDateTime startOfDay = LocalDateTime.of(2018, Month.JANUARY, 1, 0, 0);
		LocalDateTime endOfDay = LocalDateTime.of(2018, Month.JANUARY, 1, 23, 59);

		checkFeeBetween(unit, car, FREE_RATE, startOfDay, endOfDay);
	}

	@Test
	void cars_pass_for_free_on_easter_monday() {
		LocalDateTime startOfDay2018 = LocalDateTime.of(2018, Month.APRIL, 2, 0, 0);
		LocalDateTime endOfDay2018 = LocalDateTime.of(2018, Month.APRIL, 2, 23, 59);

		checkFeeBetween(unit, car, FREE_RATE, startOfDay2018, endOfDay2018);

		LocalDateTime startOfDay2019 = LocalDateTime.of(2019, Month.APRIL, 22, 0, 0);
		LocalDateTime endOfDay2019 = LocalDateTime.of(2019, Month.APRIL, 22, 23, 59);

		checkFeeBetween(unit, car, FREE_RATE, startOfDay2019, endOfDay2019);
	}

	@Test
	void cars_pass_for_free_on_weekends() {
		LocalDateTime startOfDay = weekendAt(0, 0);
		LocalDateTime endOfDay = weekendAt(23, 59);

		checkFeeBetween(unit, car, FREE_RATE, startOfDay, endOfDay);
	}

	@Test
	void motorbikes_pass_for_free() {
		LocalDateTime startOfYear = LocalDateTime.of(2018, Month.JANUARY, 1, 0, 0);
		LocalDateTime startOfNextYear = LocalDateTime.of(2019, Month.JANUARY, 1, 0, 0);

		checkFeeBetween(unit, motorbike, FREE_RATE, startOfYear, startOfNextYear);
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
	private void checkFeeBetween(TollCalculator unit, Vehicle passer, int expectedFee, LocalDateTime startInclusive, LocalDateTime endExclusive) {
		if (!endExclusive.isAfter(startInclusive)) {
			throw new IllegalArgumentException("End time must be after start time");
		}

		LocalDateTime passing = startInclusive;
		while (passing.isBefore(endExclusive)) {
			int fee = unit.getTollFee(passer, passing);
			assertEquals(expectedFee, fee, errorAt(passing));
			passing = passing.plusMinutes(1);
		}
	}

	private static LocalDateTime regularDayAt(int hour, int minute) {
		return LocalDateTime.of(2018, Month.APRIL, 9, hour, minute);
	}

	private static LocalDateTime weekendAt(int hour, int minute) {
		return LocalDateTime.of(2018, Month.APRIL, 7, hour, minute);
	}
	/**
	 * Builds an error message out of the given LocalDateTime object
	 */
	private String errorAt(LocalDateTime passing) {
		return "Wrong fee given for " + passing;
	}
}