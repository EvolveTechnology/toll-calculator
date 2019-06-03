package se.evolve.tollcalculator;

import static org.assertj.core.api.Assertions.assertThat;

import java.time.LocalTime;

import org.junit.Test;

import se.evolve.tollcalculator.TollInterval;

public class TollIntervalTest {

	@Test
	public void time_and_toll_is_set() {
		TollInterval tollPeriod = new TollInterval(LocalTime.MIDNIGHT, LocalTime.NOON, 5);
		assertThat(tollPeriod.getStartTime()).isEqualTo(LocalTime.MIDNIGHT);
		assertThat(tollPeriod.getEndTime()).isEqualTo(LocalTime.NOON);
		assertThat(tollPeriod.getFee()).isEqualTo(5);
	}
	
	@Test
	public void true_when_time_is_in_range() {
		TollInterval tollPeriod = new TollInterval(LocalTime.of(8, 0), LocalTime.of(9, 30), 10);
		assertThat(tollPeriod.containsTime(LocalTime.of(8, 47))).isTrue();
	}
	
	@Test
	public void true_when_first_minute() {
		TollInterval tollPeriod = new TollInterval(LocalTime.of(8, 0), LocalTime.of(9, 30), 10);
		assertThat(tollPeriod.containsTime(LocalTime.of(8, 0))).isTrue();
	}
	
	@Test
	public void true_when_last_minute() {
		TollInterval tollPeriod = new TollInterval(LocalTime.of(8, 0), LocalTime.of(9, 30), 10);
		assertThat(tollPeriod.containsTime(LocalTime.of(9, 30))).isTrue();
	}
	
	@Test
	public void false_when_time_is_before_range() {
		TollInterval tollPeriod = new TollInterval(LocalTime.of(8, 0), LocalTime.of(9, 30), 10);
		assertThat(tollPeriod.containsTime(LocalTime.of(7, 59))).isFalse();
	}
	
	@Test
	public void false_when_time_is_after_range() {
		TollInterval tollPeriod = new TollInterval(LocalTime.of(8, 0), LocalTime.of(9, 30), 10);
		assertThat(tollPeriod.containsTime(LocalTime.of(9, 31))).isFalse();
	}
}
