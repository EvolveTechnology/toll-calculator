package se.raihle.tollcalculator.test;

import java.time.LocalDateTime;
import java.time.temporal.TemporalUnit;
import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.Stream;

public class LocalDateTimeStream {
	public static Stream<LocalDateTime> from(LocalDateTime startInclusive, TemporalUnit stepUnit, long stepAmount) {
		return Stream.iterate(startInclusive, previous -> previous.plus(stepAmount, stepUnit));
	}

	/**
	 * Returns a modifiable list of the first <code>numToTake</code> elements returned by {@link #from(LocalDateTime, TemporalUnit, long)} for the same arguments
	 */
	public static List<LocalDateTime> takeAsList(int numToTake, LocalDateTime startInclusive, TemporalUnit stepUnit, long stepAmount) {
		// We use this form (instead of Collectors.toList) to guarantee that the list is modifiable
		return LocalDateTimeStream.from(startInclusive, stepUnit, stepAmount).limit(numToTake).collect(Collectors.toCollection(ArrayList::new));
	}
}
