package se.raihle.tollcalculator;

import se.raihle.tollcalculator.schedule.HolidaySchedule;

import java.time.*;
import java.util.*;
import java.util.stream.Collectors;

public class TollCalculator {

	private final HolidaySchedule holidaySchedule;

	public TollCalculator(HolidaySchedule holidaySchedule) {
		this.holidaySchedule = holidaySchedule;
	}

	/**
	 * Calculate the total toll fee for one day
	 *
	 * @param vehicle - the vehicle
	 * @param dates   - date and time of all passings on one day
	 * @return - the total toll fee for that day
	 */
	public int getTollFee(Vehicle vehicle, Date... dates) {
		if (dates.length == 0) {
			return 0;
		}

		List<LocalDateTime> passings = toLocalDateTimes(dates);
		passings.sort(Comparator.naturalOrder());
		assertPassingsAreOnSameDay(passings);
		passings.removeIf(this::isTollFreeDay);
		Intervals intervals = arrangeInOneHourIntervals(passings);

		return Math.min(intervals.totalFeeFor(vehicle), 60);
	}

	/**
	 * Calculate the fee for an individual passing.
	 *
	 * @param date the time of passing
	 * @param vehicle the vehicle
	 * @return the fee for the passing, assuming no other passings in the previous hour
	 */
	public int getTollFee(final Date date, Vehicle vehicle) {
		LocalDateTime passing = toLocalDateTime(date);
		if (isTollFreeDay(passing)) {
			return 0;
		}
		return vehicle.getTollAt(passing.toLocalTime());
	}

	/**
	 * Checks that all passings in the list are on the same day. Assumes the list to be sorted.
	 */
	private static void assertPassingsAreOnSameDay(List<LocalDateTime> passings) {
		LocalDateTime firstPassing = passings.get(0);
		LocalDateTime lastPassing = passings.get(passings.size() - 1);
		if (!firstPassing.toLocalDate().isEqual(lastPassing.toLocalDate())) {
			throw new IllegalArgumentException("All passings must be on the same day, arguments spanned from " + firstPassing + " to " + lastPassing);
		}
	}

	private boolean isTollFreeDay(LocalDateTime passing) {
		LocalDate dayOfPassing = passing.toLocalDate();
		return isHoliday(dayOfPassing) || isWeekend(dayOfPassing);
	}

	private boolean isHoliday(LocalDate passing) {
		return holidaySchedule.isHoliday(passing);
	}

	private static boolean isWeekend(LocalDate passing) {
		DayOfWeek dayOfWeek = passing.getDayOfWeek();
		return dayOfWeek == DayOfWeek.SATURDAY || dayOfWeek == DayOfWeek.SUNDAY;
	}

	/**
	 * Create a collection of one-hour {@link Interval}s out of a sorted list of passings.
	 */
	private static Intervals arrangeInOneHourIntervals(List<LocalDateTime> passings) {
		List<LocalTime> timesOfPassing = toLocalTimes(passings);
		return new Intervals(timesOfPassing);
	}

	private static List<LocalTime> toLocalTimes(List<LocalDateTime> passings) {
		return passings.stream().map(LocalDateTime::toLocalTime).collect(Collectors.toList());
	}

	private static List<LocalDateTime> toLocalDateTimes(Date[] sortedDates) {
		return Arrays.stream(sortedDates).map(TollCalculator::toLocalDateTime).collect(Collectors.toList());
	}

	private static LocalDateTime toLocalDateTime(Date date) {
		return LocalDateTime.ofInstant(date.toInstant(), ZoneId.systemDefault());
	}

	/**
	 * Describes a collection of {@link Interval}s
	 */
	private static class Intervals {
		private final List<Interval> intervals;

		Intervals(List<LocalTime> timesOfPassing) {
			intervals = new ArrayList<>();
			timesOfPassing.forEach(this::addPassing);
		}

		/**
		 * Add a passing to this collection, creating a new Interval if necessary.
		 * Passings must be added in chronological order.
		 */
		void addPassing(LocalTime passing) {
			if (intervals.isEmpty()) {
				addIntervalStartingAt(passing);
			} else {
				Interval lastInterval = intervals.get(intervals.size() - 1);
				if (lastInterval.canContain(passing)) {
					lastInterval.add(passing);
				} else {
					addIntervalStartingAt(passing);
				}
			}
		}

		/**
		 * Calculates the total fee for the vehicle responsible for all of these passings, acknowledging the
		 * once-per-hour rule but without any cap on the total.
		 */
		int totalFeeFor(Vehicle passer) {
			return intervals.stream().mapToInt(interval -> interval.totalFeeFor(passer)).sum();
		}

		private void addIntervalStartingAt(LocalTime passing) {
			Interval interval = new Interval(passing);
			intervals.add(interval);
		}
	}

	/**
	 * Describes an interval that starts with an initial passing and may contain additional ones that occurred up to an
	 * hour after the first.
	 */
	private static class Interval {
		private final LocalTime firstTimeInsideInterval;
		private final LocalTime firstTimeAfterInterval;
		private final List<LocalTime> passingsInsideInterval;

		Interval(LocalTime firstPassing) {
			this.firstTimeInsideInterval = firstPassing;
			this.firstTimeAfterInterval = firstPassing.plusHours(1);
			passingsInsideInterval = new ArrayList<>();
			passingsInsideInterval.add(firstPassing);
		}

		/**
		 * Calculates the fee for a passing vehicle, acknowledging the rule that only the most expensive passing in an
		 * interval should be tolled.
		 */
		int totalFeeFor(Vehicle passer) {
			return passingsInsideInterval.stream()
					.mapToInt(passer::getTollAt)
					.max()
					.orElse(0);
		}

		/**
		 * True if the given passing is less than an hour after the first passing in this interval
		 */
		boolean canContain(LocalTime passing) {
			if (passing.isBefore(firstTimeInsideInterval)) {
				return false;
			}
			return passing.isBefore(firstTimeAfterInterval);
		}

		/**
		 * Adds the given passing to this interval. It is the callers responsibility to check that this is appropriate
		 * first, using {@link #canContain(LocalTime)}.
		 */
		void add(LocalTime passing) {
			passingsInsideInterval.add(passing);
		}
	}
}

