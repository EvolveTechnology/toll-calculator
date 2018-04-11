package se.raihle.tollcalculator.schedule;

import java.io.File;
import java.io.FileNotFoundException;
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

	public static final String DELIMITER = "\\s+";

	public static HolidaySchedule fromString(String dates) {
		Scanner scanner = new Scanner(dates).useDelimiter(DELIMITER);
		return buildScheduleFromScanner(scanner);
	}

	/**
	 * Creates a schedule from the given input stream, then closes it
	 */
	public static HolidaySchedule fromInputStream(InputStream dates) {
		Scanner scanner = new Scanner(dates).useDelimiter(DELIMITER);
		return buildScheduleFromScanner(scanner);
	}

	/**
	 * Creates a schedule using the input in a scanner, then closes the scanner.
	 */
	private static HolidaySchedule buildScheduleFromScanner(Scanner scanner) {
		List<LocalDate> holidays = new ArrayList<>();
		try {
			while (scanner.hasNext()) {
				String dateString = scanner.next();
				holidays.add(LocalDate.parse(dateString));
			}
		} finally {
			scanner.close();
		}
		return new HolidaySchedule(holidays);
	}
}
