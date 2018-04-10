package se.raihle.tollcalculator;

import java.time.LocalDateTime;
import java.time.ZoneId;
import java.util.*;
import java.util.stream.Collectors;

public class TollCalculator {

	/**
	 * Calculate the total toll fee for one day
	 *
	 * @param vehicle - the vehicle
	 * @param dates   - date and time of all passes on one day
	 * @return - the total toll fee for that day
	 */
	public int getTollFee(Vehicle vehicle, Date... dates) {
		if (dates.length == 0) {
			return 0;
		}

		Date[] sortedDates = Arrays.copyOf(dates, dates.length);
		Arrays.sort(sortedDates);

		Intervals intervals = arrangeIntervals(sortedDates);

		return Math.min(intervals.totalFeeFor(vehicle), 60);
	}

	public int getTollFee(final Date date, Vehicle vehicle) {
		LocalDateTime timeOfPassing = LocalDateTime.ofInstant(date.toInstant(), ZoneId.systemDefault());
		return vehicle.getTollAt(timeOfPassing);
	}

	private Intervals arrangeIntervals(Date[] passings) {
		return new Intervals(Arrays.stream(passings).map(this::dateToLocalDateTime).collect(Collectors.toList()));

	}

	private LocalDateTime dateToLocalDateTime(Date date) {
		return LocalDateTime.ofInstant(date.toInstant(), ZoneId.systemDefault());
	}

	/**
	 * Describes a collection of {@link Interval}s
	 */
	private static class Intervals {
		private final List<Interval> intervals;

		Intervals(List<LocalDateTime> timesOfPassing) {
			intervals = new ArrayList<>();
			timesOfPassing.forEach(this::addPassing);
		}

		/**
		 * Add a passing to this collection, creating a new Interval if necessary.
		 * Passings must be added in chronological order.
		 */
		void addPassing(LocalDateTime passing) {
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

		private void addIntervalStartingAt(LocalDateTime passing) {
			Interval interval = new Interval(passing);
			intervals.add(interval);
		}
	}

	/**
	 * Describes an interval that starts with an initial passing and may contain additional ones that occurred up to an
	 * hour after the first.
	 */
	private static class Interval {
		private final LocalDateTime firstTimeInsideInterval;
		private final LocalDateTime firstTimeAfterInterval;
		private final List<LocalDateTime> passingsInsideInterval;

		Interval(LocalDateTime firstPassing) {
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
			return passingsInsideInterval.stream().mapToInt(passer::getTollAt).max().orElse(0);
		}

		/**
		 * True if the given passing is less than an hour after the first passing in this interval
		 */
		boolean canContain(LocalDateTime passing) {
			if (passing.isBefore(firstTimeInsideInterval)) {
				return false;
			}
			return passing.isBefore(firstTimeAfterInterval);
		}

		/**
		 * Adds the given passing to this interval. It is the callers responsibility to check that this is appropriate
		 * first, using {@link #canContain(LocalDateTime)}.
		 */
		void add(LocalDateTime passing) {
			passingsInsideInterval.add(passing);
		}
	}
}

