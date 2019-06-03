package se.evolve.tollcalculator;

import java.time.DayOfWeek;
import java.time.LocalDate;
import java.util.HashSet;
import java.util.Set;

public class AllSaintsEveCalculator {
	
	public static LocalDate get(int year) {
		Set<LocalDate> possibleDates = new HashSet<>();
		possibleDates.add(LocalDate.of(year, 10, 30));
		possibleDates.add(LocalDate.of(year, 10, 31));
		possibleDates.add(LocalDate.of(year, 11, 1));
		possibleDates.add(LocalDate.of(year, 11, 2));
		possibleDates.add(LocalDate.of(year, 11, 3));
		possibleDates.add(LocalDate.of(year, 11, 4));
		possibleDates.add(LocalDate.of(year, 11, 5));
		return possibleDates.stream().filter(d -> d.getDayOfWeek() == DayOfWeek.FRIDAY).findFirst().get();
	}
}
