package se.evolve.tollcalculator;

import java.time.DayOfWeek;
import java.time.LocalDate;
import java.util.HashSet;
import java.util.Set;

public class MidsummerCalculator {
	
	public static LocalDate get(int year) {
		Set<LocalDate> possibleDates = new HashSet<>();
		possibleDates.add(LocalDate.of(year, 6, 19));
		possibleDates.add(LocalDate.of(year, 6, 20));
		possibleDates.add(LocalDate.of(year, 6, 21));
		possibleDates.add(LocalDate.of(year, 6, 22));
		possibleDates.add(LocalDate.of(year, 6, 23));
		possibleDates.add(LocalDate.of(year, 6, 24));
		possibleDates.add(LocalDate.of(year, 6, 25));
		return possibleDates.stream().filter(d -> d.getDayOfWeek() == DayOfWeek.FRIDAY).findFirst().get();
	}
}
