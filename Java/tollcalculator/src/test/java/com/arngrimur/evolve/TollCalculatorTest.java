package com.arngrimur.evolve;

import static org.junit.Assert.*;

import java.time.Duration;
import java.time.Instant;
import java.time.LocalDateTime;
import java.time.Month;
import java.time.ZoneId;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Date;
import java.util.List;

import org.junit.Test;

public class TollCalculatorTest {
	private TollCalculator tollCalculator = new TollCalculator();
	private LocalDateTime jan15st = LocalDateTime.of(2017, Month.JANUARY, 15, 0, 0, 0);
	private Instant sunday = jan15st.atZone(ZoneId.systemDefault()).toInstant();
	private Instant monday = jan15st.atZone(ZoneId.systemDefault()).toInstant().plus(Duration.ofDays(1));

	@Test
	public void emptyDate() {
		assertEquals(0, tollCalculator.getTollFee(Vehicle.CAR, Arrays.asList(new LocalDateTime[] {})));
	}

	@Test
	public void nullDate() {
		assertEquals(0, tollCalculator.getTollFee(Vehicle.CAR, null));
	}

	@Test
	public void nullVehicle() {
		assertEquals(0, tollCalculator.getTollFee(null, Arrays.asList(new LocalDateTime[] { jan15st })));
	}

	@Test
	public void motorbikeIsTollFree() {
		int tollFee = tollCalculator.getTollFee(Vehicle.MOTORBIKE, Arrays.asList(new LocalDateTime[] {
				LocalDateTime.ofInstant(monday.plus(Duration.ofHours(1)), ZoneId.systemDefault()) }));
		assertEquals(0, tollFee);
	}

	@Test
	public void tractorIsTollFree() {
		int tollFee = tollCalculator.getTollFee(Vehicle.TRACTOR, Arrays.asList(new LocalDateTime[] {
				LocalDateTime.ofInstant(monday.plus(Duration.ofHours(1)), ZoneId.systemDefault()) }));
		assertEquals(0, tollFee);
	}

	@Test
	public void emergencyIsTollFree() {
		int tollFee = tollCalculator.getTollFee(Vehicle.EMERGENCY, Arrays.asList(new LocalDateTime[] {
				LocalDateTime.ofInstant(monday.plus(Duration.ofHours(1)), ZoneId.systemDefault()) }));
		assertEquals(0, tollFee);
	}

	@Test
	public void diplomatIsTollFree() {
		int tollFee = tollCalculator.getTollFee(Vehicle.DIPLOMAT, Arrays.asList(new LocalDateTime[] {
				LocalDateTime.ofInstant(monday.plus(Duration.ofHours(1)), ZoneId.systemDefault()) }));
		assertEquals(0, tollFee);
	}

	@Test
	public void foreignIsTollFree() {
		int tollFee = tollCalculator.getTollFee(Vehicle.FOREIGN, Arrays.asList(new LocalDateTime[] {
				LocalDateTime.ofInstant(monday.plus(Duration.ofHours(1)), ZoneId.systemDefault()) }));
		assertEquals(0, tollFee);
	}

	@Test
	public void carPaysToll() {
		int tollFee = tollCalculator.getTollFee(Vehicle.CAR, Arrays.asList(new LocalDateTime[] {
				LocalDateTime.ofInstant(monday.plus(Duration.ofHours(6)), ZoneId.systemDefault()) }));
		assertEquals(8, tollFee);
	}

	@Test
	public void noFeeOnSundays() {
		int tollFee = tollCalculator.getTollFee(Vehicle.CAR, Arrays.asList(new LocalDateTime[] {
				LocalDateTime.ofInstant(sunday.plus(Duration.ofHours(6L)), ZoneId.systemDefault()) }));
		assertEquals(0, tollFee);
	}

	@Test
	public void noFeeOnSaturdays() {
		int tollFee = tollCalculator.getTollFee(Vehicle.CAR, Arrays.asList(new LocalDateTime[] { LocalDateTime
				.ofInstant(sunday.plus(Duration.ofHours(6L).minus(Duration.ofDays(1L))), ZoneId.systemDefault()) }));
		assertEquals(0, tollFee);
	}

	@Test
	public void carPaysTollOnlyOnceEachHour() {
		int tollFee = tollCalculator.getTollFee(Vehicle.CAR,
				Arrays.asList(new LocalDateTime[] { LocalDateTime.ofInstant(monday.plus(Duration.ofHours(6)), ZoneId.systemDefault()),
						LocalDateTime.ofInstant(monday.plus(Duration.ofHours(6).plus(Duration.ofMinutes(5L))),
								ZoneId.systemDefault()) }));
		assertEquals(8, tollFee);
	}

	@Test
	public void maximChargeForOneDay() {
		int tollFee = tollCalculator.getTollFee(Vehicle.CAR, manyPassages(monday));
		assertEquals(60, tollFee);
	}

	@Test
	public void maximChargeForTwoDays() {
		int tollFee = tollCalculator.getTollFee(Vehicle.CAR, manyPassages(monday, monday.plus(Duration.ofDays(1L))));
		assertEquals(120, tollFee);
	}

	@Test
	public void carPaysTollTwoConsecutiveDays() {
		int tollFee = tollCalculator.getTollFee(Vehicle.CAR,
				Arrays.asList(new LocalDateTime[] { LocalDateTime.ofInstant(monday.plus(Duration.ofHours(6)), ZoneId.systemDefault()),
						LocalDateTime.ofInstant(monday.plus(Duration.ofHours(6).plus(Duration.ofDays(1L))),
								ZoneId.systemDefault()) }));
		assertEquals(16, tollFee);
	}

	@Test
	public void rushHourRendersHighestFee() {
		int tollFee = tollCalculator.getTollFee(Vehicle.CAR,
				Arrays.asList(new LocalDateTime[] { LocalDateTime.ofInstant(monday.plus(Duration.ofHours(6)), ZoneId.systemDefault()),
						LocalDateTime.ofInstant(monday.plus(Duration.ofHours(6).plus(Duration.ofMinutes(30))),
								ZoneId.systemDefault()) }));
		assertEquals(13, tollFee);
	}

	@Test
	public void rushHourRendersHighestFee2() {
		int tollFee = tollCalculator.getTollFee(Vehicle.CAR,
				Arrays.asList(new LocalDateTime[] { LocalDateTime.ofInstant(monday.plus(Duration.ofHours(8)), ZoneId.systemDefault()),
						LocalDateTime.ofInstant(monday.plus(Duration.ofHours(8).plus(Duration.ofMinutes(30))),
								ZoneId.systemDefault()) }));
		assertEquals(13, tollFee);
	}

	@Test
	public void lowFeeOnMidday() {
		int tollFee = tollCalculator.getTollFee(Vehicle.CAR, Arrays.asList(new LocalDateTime[] { LocalDateTime
				.ofInstant(monday.plus(Duration.ofHours(13).plus(Duration.ofMinutes(5L))), ZoneId.systemDefault()) }));
		assertEquals(8, tollFee);
	}

	public void noFeeAfter1830() {
		int tollFee = tollCalculator.getTollFee(Vehicle.CAR, Arrays.asList(new LocalDateTime[] { LocalDateTime
				.ofInstant(monday.plus(Duration.ofHours(18).plus(Duration.ofMinutes(30L))), ZoneId.systemDefault()) }));
		assertEquals(0, tollFee);
	}

	public void noFeeBefor6() {
		int tollFee = tollCalculator.getTollFee(Vehicle.CAR, Arrays.asList(new LocalDateTime[] { LocalDateTime
				.ofInstant(monday.plus(Duration.ofHours(5).plus(Duration.ofMinutes(59L))), ZoneId.systemDefault()) }));
		assertEquals(0, tollFee);
	}

	private List<LocalDateTime> manyPassages(Instant... days) {
		List<LocalDateTime> passages = new ArrayList<LocalDateTime>();
		for (Instant day : days) {
			List<LocalDateTime> tmp = Arrays.asList(new LocalDateTime[] {
					LocalDateTime.ofInstant(day.plus(Duration.ofHours(6)), ZoneId.systemDefault()),
					LocalDateTime.ofInstant(day.plus(Duration.ofHours(7).plus(Duration.ofMinutes(5L))),
							ZoneId.systemDefault()),
					LocalDateTime.ofInstant(day.plus(Duration.ofHours(8).plus(Duration.ofMinutes(5L))),
							ZoneId.systemDefault()),
					LocalDateTime.ofInstant(day.plus(Duration.ofHours(9).plus(Duration.ofMinutes(5L))),
							ZoneId.systemDefault()),
					LocalDateTime.ofInstant(day.plus(Duration.ofHours(15).plus(Duration.ofMinutes(5L))),
							ZoneId.systemDefault()),
					LocalDateTime.ofInstant(day.plus(Duration.ofHours(16).plus(Duration.ofMinutes(5L))),
							ZoneId.systemDefault()),
					LocalDateTime.ofInstant(day.plus(Duration.ofHours(17).plus(Duration.ofMinutes(5L))),
							ZoneId.systemDefault()),
					LocalDateTime.ofInstant(day.plus(Duration.ofHours(18).plus(Duration.ofMinutes(5L))),
							ZoneId.systemDefault()) });
			passages.addAll(tmp);
		}
		return passages;
	}
}
