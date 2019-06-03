package se.evolve.tollcalculator;

import static java.time.DayOfWeek.SATURDAY;
import static java.time.DayOfWeek.SUNDAY;
import static java.time.Month.JULY;

import java.time.LocalDate;
import java.util.EnumSet;
import java.util.HashSet;
import java.util.Set;

/**
 * Assumption: The choice of toll free dates in the original file are correct,
 * aside from not considering epiphany (which was a Sunday in 2013). The actual
 * requirements are very open interpretation, but the original seemed to have
 * the same rules as tolls in Sweden (including July), which seems reasonable to
 * me. So I'll use the same holidays.
 * 
 * Assumption: Tolls will be collected every year.
 *
 */
public class TollFreeDayCalculator {

	public static boolean isTollFreeDay(LocalDate date) {
		if (date.getDayOfWeek() == SATURDAY || date.getDayOfWeek() == SUNDAY || date.getMonth() == JULY) {
			return true;
		}
		final int year = date.getYear();
		Set<LocalDate> holidays = new HashSet<>();
		EnumSet.allOf(FixedHoliday.class).forEach(h -> holidays.add(h.getDate(year)));
		EnumSet.allOf(EasterHoliday.class).forEach(h -> holidays.add(h.getDate(year)));
		holidays.add(MidsummerCalculator.get(year));
		holidays.add(AllSaintsEveCalculator.get(year));
		return holidays.contains(date);
	}
}
