package se.raihle.tollcalculator;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import se.raihle.tollcalculator.schedule.FeeSchedule;
import se.raihle.tollcalculator.schedule.FeeScheduleParser;
import se.raihle.tollcalculator.schedule.HolidaySchedule;
import se.raihle.tollcalculator.schedule.HolidayScheduleParser;
import se.raihle.tollcalculator.test.LocalDateTimeStream;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.Month;
import java.time.temporal.ChronoUnit;
import java.util.Collections;
import java.util.List;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;

class CombinedCalculationTests {
	private static final int DAILY_MAXIMUM = 60;

	private TollCalculator unit;
	private Vehicle car;

	@BeforeEach
	void setup() {
		FeeSchedule fees = FeeScheduleParser.fromInputStream(getClass().getResourceAsStream("/regular-fees.txt"));
		HolidaySchedule holidays = HolidayScheduleParser.fromInputStream(this.getClass().getResourceAsStream("/2018-2019-holidays.txt"));
		unit = new TollCalculator(DAILY_MAXIMUM, holidays);
		car = new DefaultVehicle(fees);
	}

	@Test
	void car_passing_two_times_within_an_hour_is_only_charged_the_higher_rate() {
		LocalDateTimeStream.from(regularDayAt(0, 0), ChronoUnit.MINUTES, 15).limit(4 * 24).forEach(startingPoint -> {
			List<LocalDateTime> timesOfPassing = LocalDateTimeStream.takeAsList(2, startingPoint, ChronoUnit.MINUTES, 30);
			if (timesAreOnSameDay(timesOfPassing)) {
				int expectedFee = highestFeeAmong(timesOfPassing, car);
				assertEquals(expectedFee, totalFeeFor(timesOfPassing, car), errorAt(timesOfPassing.get(0)));
			}
		});
	}

	@Test
	void car_passing_three_times_within_an_hour_is_only_charged_the_highest_rate() {
		LocalDateTimeStream.from(regularDayAt(0, 0), ChronoUnit.MINUTES, 15).limit(4 * 24).forEach(startingPoint -> {
			List<LocalDateTime> timesOfPassing = LocalDateTimeStream.takeAsList(3, startingPoint, ChronoUnit.MINUTES, 20);
			if (timesAreOnSameDay(timesOfPassing)) {
				int expectedFee = highestFeeAmong(timesOfPassing, car);
				assertEquals(expectedFee, totalFeeFor(timesOfPassing, car), errorAt(timesOfPassing.get(0)));
			}
		});
	}

	@Test
	void car_passing_twice_per_hour_for_two_hours_is_charged_the_sum_of_higher_rates_for_each_hour() {
		LocalDateTimeStream.from(regularDayAt(0, 0), ChronoUnit.MINUTES, 15).limit(4 * 24).forEach(startingPoint -> {
			List<LocalDateTime> timesOfPassing = LocalDateTimeStream.takeAsList(4, startingPoint, ChronoUnit.MINUTES, 30);
			List<LocalDateTime> passingsInFirstHour = timesOfPassing.subList(0, 2);
			List<LocalDateTime> passingsInSecondHour = timesOfPassing.subList(2, 4);
			if (timesAreOnSameDay(timesOfPassing)) {
				int expectedFee = highestFeeAmong(passingsInFirstHour, car) + highestFeeAmong(passingsInSecondHour, car);
				assertEquals(expectedFee, totalFeeFor(timesOfPassing, car), errorAt(timesOfPassing.get(0)));
			}
		});
	}

	@Test
	void car_passing_twice_per_hour_for_two_hours_is_charged_the_sum_of_higher_rates_for_each_hour_if_times_are_out_of_order() {
		LocalDateTimeStream.from(regularDayAt(0, 0), ChronoUnit.MINUTES, 15).limit(4 * 24).forEach(startingPoint -> {
			List<LocalDateTime> timesOfPassing = LocalDateTimeStream.takeAsList(4, startingPoint, ChronoUnit.MINUTES, 30);
			List<LocalDateTime> passingsInFirstHour = timesOfPassing.subList(0, 2);
			List<LocalDateTime> passingsInSecondHour = timesOfPassing.subList(2, 4);
			Collections.reverse(timesOfPassing);
			if (timesAreOnSameDay(timesOfPassing)) {
				int expectedFee = highestFeeAmong(passingsInFirstHour, car) + highestFeeAmong(passingsInSecondHour, car);
				assertEquals(expectedFee, totalFeeFor(timesOfPassing, car), errorAt(timesOfPassing.get(0)));
			}
		});
	}

	@Test
	void car_passing_once_per_hour_for_a_day_is_only_billed_the_daily_rate() {
		List<LocalDateTime> timesOfPassing = LocalDateTimeStream.takeAsList(24, regularDayAt(0, 30), ChronoUnit.HOURS, 1);

		int fee = unit.getTollFee(car, timesOfPassing);
		assertEquals(DAILY_MAXIMUM, fee);
	}

	@Test
	void calculating_tolls_for_two_days_at_once_throws_an_IllegalArgumentException() {
		List<LocalDateTime> timesOfPassing = LocalDateTimeStream.takeAsList(48, regularDayAt(0, 30), ChronoUnit.HOURS, 1);

		assertThrows(IllegalArgumentException.class, () -> unit.getTollFee(car, timesOfPassing));
	}


	/**
	 * Gets the highest individual fee for any of the given passings
	 */
	private int highestFeeAmong(List<LocalDateTime> timesOfPassing, Vehicle passer) {
		return timesOfPassing.stream()
				.mapToInt(time -> unit.getTollFee(passer, time))
				.max()
				.orElse(0);
	}

	/**
	 * Gets the total fee for the passings as a group
	 */
	private int totalFeeFor(List<LocalDateTime> timesOfPassing, Vehicle passer) {
		return unit.getTollFee(passer, timesOfPassing);
	}

	/**
	 * Most tests are not concerned with cases when the vehicle passes on multiple days, so we want to filter those out
	 * @param timesofpassing times of passing sorted from first to last
	 */
	private boolean timesAreOnSameDay(List<LocalDateTime> timesofpassing) {
		LocalDate first = timesofpassing.get(0).toLocalDate();
		LocalDate last = timesofpassing.get(timesofpassing.size() - 1).toLocalDate();
		return first.equals(last);
	}

	private static LocalDateTime regularDayAt(int hour, int minute) {
		return LocalDateTime.of(2018, Month.APRIL, 9, hour, minute);
	}

	/**
	 * Builds an error message out of the given LocalDateTime object
	 */
	private String errorAt(LocalDateTime passing) {
		return "Wrong fee given for sequence starting at " + passing;
	}
}