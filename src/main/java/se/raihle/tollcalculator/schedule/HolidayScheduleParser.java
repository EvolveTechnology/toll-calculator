package se.raihle.tollcalculator.schedule;

import java.io.InputStream;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

/**
 * Reads a list of dates in YYYY-MM-DD format and creates a schedule from them.
 * The dates should be separated by any whitespace (including newline).
 */
public class HolidayScheduleParser {
	/**
	 * Creates a schedule from the given input stream, then closes it
	 */
	public static HolidaySchedule fromInputStream(InputStream dates) {
		try (Scanner scanner = new Scanner(dates).useDelimiter("\\s+")) {
			List<LocalDate> holidays = new ArrayList<>();
			while (scanner.hasNext()) {
				String dateString = scanner.next();
				holidays.add(LocalDate.parse(dateString));
			}
			return new HolidaySchedule(holidays);
		}
	}
}
