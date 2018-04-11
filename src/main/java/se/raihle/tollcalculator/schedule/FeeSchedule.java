package se.raihle.tollcalculator.schedule;

import java.time.LocalTime;
import java.util.Collections;
import java.util.List;

/**
 * Collects the fees at different times of a single day. Create Schedules using a {@link FeeScheduleBuilder}.
 */
public class FeeSchedule {
	private final List<Part> parts;

	FeeSchedule(List<Part> parts) {
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

	static class Part {
		final LocalTime fromInclusive;
		final LocalTime untilExclusive;
		final int fee;

		Part(LocalTime fromInclusive, LocalTime untilExclusive, int fee) {
			this.fromInclusive = fromInclusive;
			this.untilExclusive = untilExclusive;
			this.fee = fee;
		}
	}
}
