package se.raihle.tollcalculator.schedule;

import java.time.LocalTime;

class Part {
	final LocalTime fromInclusive;
	final LocalTime untilExclusive;
	final int fee;

	Part(LocalTime fromInclusive, LocalTime untilExclusive, int fee) {
		this.fromInclusive = fromInclusive;
		this.untilExclusive = untilExclusive;
		this.fee = fee;
	}
}
