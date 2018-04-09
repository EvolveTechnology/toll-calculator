package se.raihle.tollcalculator.schedule;

import java.time.LocalTime;
import java.util.Collections;
import java.util.List;

/**
 * Collects the fees at different times of a single day. Create Schedules using a {@link ScheduleBuilder}.
 */
public class Schedule {
	private final List<Part> parts;

	Schedule(List<Part> parts) {
		this.parts = Collections.unmodifiableList(parts);
	}

	public int getFeeAt(LocalTime timeOfPassing) {
		Part partAtTimeOfPassing = parts.stream()
				.filter(part -> partCoversTime(part, timeOfPassing))
				.findAny()
				.orElseThrow(() -> new RuntimeException("Could not find a schedule part to match the time of passing"));

		return partAtTimeOfPassing.fee;
	}

	private boolean partCoversTime(Part part, LocalTime time) {
		return !part.fromInclusive.isAfter(time) && part.untilExclusive.isAfter(time);
	}
}
