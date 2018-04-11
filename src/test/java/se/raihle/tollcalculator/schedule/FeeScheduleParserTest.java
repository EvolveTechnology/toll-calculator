package se.raihle.tollcalculator.schedule;

import org.junit.jupiter.api.Test;

import java.time.LocalTime;

import static org.junit.jupiter.api.Assertions.assertEquals;

class FeeScheduleParserTest {
	@Test
	void fromInputStream_creates_a_schedule_with_each_included_time_segment() {
		FeeSchedule schedule = FeeScheduleParser.fromInputStream(this.getClass().getResourceAsStream("/test-fees.txt"));
		assertEquals(13, schedule.getFeeAt(LocalTime.MIN));
		assertEquals(18, schedule.getFeeAt(LocalTime.of(7, 0)));
		assertEquals(8, schedule.getFeeAt(LocalTime.of(18, 0)));
		assertEquals(8, schedule.getFeeAt(LocalTime.MAX));
	}
}
