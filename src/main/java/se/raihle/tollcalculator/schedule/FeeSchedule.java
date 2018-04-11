package se.raihle.tollcalculator.schedule;

import java.time.LocalTime;
import java.util.Collections;
import java.util.List;

/**
 * Collects the fees at different times of a single day. Create Schedules using a {@link FeeScheduleBuilder}.
 */
public class FeeSchedule {

	public static final FeeSchedule DEFAULT;
	public static final FeeSchedule FREE;

	static {
		DEFAULT = FeeScheduleBuilder
				.start(0)
				.next(LocalTime.of(6, 0), 8)
				.next(LocalTime.of(6, 30), 13)
				.next(LocalTime.of(7, 0), 18)
				.next(LocalTime.of(8, 0), 13)
				.next(LocalTime.of(8, 30), 8)
				.next(LocalTime.of(15, 0), 13)
				.next(LocalTime.of(15, 30), 18)
				.next(LocalTime.of(17, 0), 13)
				.next(LocalTime.of(18, 0), 8)
				.next(LocalTime.of(18, 30), 0)
				.finish();

		FREE = FeeScheduleBuilder
				.start(0)
				.finish();
	}

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
