package se.raihle.tollcalculator.schedule;

import java.io.InputStream;
import java.time.LocalTime;
import java.util.Scanner;

public class FeeScheduleParser {
	/**
	 * Creates a FeeSchedule from the given input stream, then closes it.
	 * Each line in the input stream should contain a time (in HH:MM format) followed by the fee to charge starting from that time, separated by whitespace.
	 * The first time must be 00:00. The last segment is assumed to continue until midnight.
	 */
	public static FeeSchedule fromInputStream(InputStream input) {
		try (Scanner scanner = new Scanner(input).useDelimiter("\\s+")) {
			String firstTime = scanner.next();
			if (!"00:00".equals(firstTime)) {
				throw new IllegalArgumentException("A fee schedule must start at midnight (00:00), I was given " + firstTime);
			}
			int firstFee = scanner.nextInt();
			FeeScheduleBuilder builder = FeeScheduleBuilder.start(firstFee);

			while (scanner.hasNext()) {
				String timeString = scanner.next();
				LocalTime time = LocalTime.parse(timeString);
				int fee = scanner.nextInt();
				builder.next(time, fee);
			}
			return builder.finish();
		}
	}
}
