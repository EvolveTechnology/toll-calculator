package se.evolve.tollcalculator;

import java.time.Duration;
import java.time.LocalDateTime;
import java.time.LocalTime;
import java.util.List;
import java.util.Map;

import com.google.common.collect.ImmutableSet;
import com.google.common.collect.Maps;

/**
 * I have made a number of assumptions in the program. Normally these are issues
 * I would want clarified from a product owner or a person in a similar
 * position.
 *
 */
public class TollCalculator {
	public static final int NONE = 0;
	public static final int LOW = 8;
	public static final int MEDIUM = 13;
	public static final int HIGH = 18;
	public static final int DAILY_MAX = 60;

	/**
	 * Assumption: The time spans in the original program had an issue where between
	 * 8.30 and 15, the first 30 minutes of every hour would be free. I assumed that
	 * was a bug. I've assumed that for the rest, the fees were correct.
	 */
	private static ImmutableSet<TollInterval> tollIntervals = ImmutableSet.of(
			new TollInterval(LocalTime.of(0, 0), LocalTime.of(5, 59), NONE),
			new TollInterval(LocalTime.of(6, 0), LocalTime.of(6, 29), LOW),
			new TollInterval(LocalTime.of(6, 30), LocalTime.of(6, 59), MEDIUM),
			new TollInterval(LocalTime.of(7, 0), LocalTime.of(7, 59), HIGH),
			new TollInterval(LocalTime.of(8, 0), LocalTime.of(8, 29), MEDIUM),
			new TollInterval(LocalTime.of(8, 30), LocalTime.of(14, 59), LOW),
			new TollInterval(LocalTime.of(15, 0), LocalTime.of(15, 29), MEDIUM),
			new TollInterval(LocalTime.of(15, 30), LocalTime.of(16, 59), HIGH),
			new TollInterval(LocalTime.of(17, 0), LocalTime.of(17, 59), MEDIUM),
			new TollInterval(LocalTime.of(18, 0), LocalTime.of(18, 29), LOW),
			new TollInterval(LocalTime.of(18, 30), LocalTime.of(23, 59), NONE));

	/**
	 * Assumption: I've interpreted the requirements to mean that a vehicle
	 * basically gets a "ticket" when they pass a toll, valid for 60 minutes, and
	 * its price is the highest fee of any crossing during that hour.
	 * I'm also assuming that this function gets a list of times during the same day.
	 */
	public int calculateTollFeeForDay(VehicleType vehicleType, List<LocalDateTime> dateTimes) {
		if (vehicleType.isTollFree() || dateTimes == null || dateTimes.size() < 1 || isTollFreeDay(dateTimes.get(0))) {
			return 0;
		}
		int totalToll = 0;
		LocalDateTime firstPassForHour = LocalDateTime.of(0, 1, 1, 0, 0);
		Map<LocalDateTime, Integer> highestHourlyFees = Maps.newHashMap();
		for (LocalDateTime currentTime : dateTimes) {
			if (Duration.between(firstPassForHour, currentTime).toSeconds() > 3599) {
				firstPassForHour = currentTime;
			}
			int highestFeeForHour = Math.max(getTollForTime(currentTime.toLocalTime()),
					highestHourlyFees.computeIfAbsent(firstPassForHour, f -> 0));
			highestHourlyFees.put(firstPassForHour, highestFeeForHour);
		}
		totalToll = Math.min(DAILY_MAX, highestHourlyFees.values().stream().mapToInt(Integer::intValue).sum());
		return totalToll;
	}

	private boolean isTollFreeDay(LocalDateTime date) {
		return TollFreeDayCalculator.isTollFreeDay(date.toLocalDate());
	}

	private int getTollForTime(LocalTime time) {
		return tollIntervals.stream().filter(ti -> ti.containsTime(time)).findFirst().get().getFee();
	}
}
