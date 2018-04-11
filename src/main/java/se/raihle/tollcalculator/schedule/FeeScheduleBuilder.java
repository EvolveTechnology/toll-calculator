package se.raihle.tollcalculator.schedule;

import java.time.LocalTime;
import java.util.LinkedList;
import java.util.List;

public class FeeScheduleBuilder {
	private final List<FeeSchedule.Part> parts;
	private int currentFee;
	private LocalTime currentStart;
	private boolean finished;

	private FeeScheduleBuilder(int feeFromMidnight) {
		parts = new LinkedList<>();
		currentFee = feeFromMidnight;
		currentStart = LocalTime.MIN;
		finished = false;
	}

	public static FeeScheduleBuilder start(int feeFromMidnight) {
		return new FeeScheduleBuilder(feeFromMidnight);
	}

	public FeeScheduleBuilder next(LocalTime time, int fee) {
		assertNotFinished();
		endCurrentPartAt(time);
		startNewPart(time, fee);
		return this;
	}

	public FeeSchedule finish() {
		assertNotFinished();
		endCurrentPartAt(LocalTime.MAX);
		finished = true;
		return new FeeSchedule(parts);
	}

	private void endCurrentPartAt(LocalTime untilExclusive) {
		parts.add(new FeeSchedule.Part(currentStart, untilExclusive, currentFee));
	}

	private void startNewPart(LocalTime fromInclusive, int fee) {
		currentStart = fromInclusive;
		currentFee = fee;
	}

	private void assertNotFinished() {
		if (finished) {
			throw new IllegalStateException("Tried to add a new part to a finished FeeScheduleBuilder");
		}
	}
}
