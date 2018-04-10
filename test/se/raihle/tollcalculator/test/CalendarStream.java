package se.raihle.tollcalculator.test;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.GregorianCalendar;
import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.Stream;

public class CalendarStream {
	public static Stream<Calendar> from(Calendar startInclusive, int stepUnit, int stepAmount) {
		Calendar defensiveCopyOfStart = GregorianCalendar.getInstance();
		defensiveCopyOfStart.setTime(startInclusive.getTime());
		return Stream.iterate(defensiveCopyOfStart, previous -> offsetCalendar(previous, stepUnit, stepAmount));
	}

	/**
	 * Returns a modifiable list of the first <code>numToTake</code> elements returned by {@link #from(Calendar, int, int)} for the same arguments
	 */
	public static List<Calendar> takeAsList(int numToTake, Calendar startInclusive, int stepUnit, int stepAmount) {
		// We use this form (instead of Collectors.toList) to guarantee that the list is modifiable
		return CalendarStream.from(startInclusive, stepUnit, stepAmount).limit(numToTake).collect(Collectors.toCollection(ArrayList::new));
	}

	private static Calendar offsetCalendar(Calendar original, int offsetUnit, int offsetAmount) {
		 Calendar result = GregorianCalendar.getInstance();
		 result.setTime(original.getTime());
		 result.add(offsetUnit, offsetAmount);
		 return result;
	}
}
