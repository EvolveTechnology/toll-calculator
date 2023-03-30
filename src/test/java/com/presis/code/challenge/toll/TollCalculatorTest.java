package com.presis.code.challenge.toll;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertTrue;

import java.time.LocalDate;
import java.time.LocalDateTime;

import org.junit.Test;

import com.presis.code.challenge.toll.config.ApplicationProperties;
import com.presis.code.challenge.toll.model.Car;
import com.presis.code.challenge.toll.model.Motorbike;
import com.presis.code.challenge.toll.model.TollMaster;
import com.presis.code.challenge.util.TollUtil;

public class TollCalculatorTest {

	TollMaster configData = new ApplicationProperties().readMaster();

	@Test
	public void testTollFreeVehicles() {
		assertFalse(TollUtil.isTollFreeVehicle(new Car()));
		assertTrue(TollUtil.isTollFreeVehicle(new Motorbike()));
	}

	@Test
	public void testTollFreeWeekends() {
		// Weekends
		assertTrue(TollUtil.isTollFreeDate(configData, LocalDate.parse("2023-03-04")));
		assertTrue(TollUtil.isTollFreeDate(configData, LocalDate.parse("2023-03-12")));
		assertTrue(TollUtil.isTollFreeDate(configData, LocalDate.parse("2023-03-25")));

		// Non weekends
		assertFalse(TollUtil.isTollFreeDate(configData, LocalDate.parse("2023-03-07")));
		assertFalse(TollUtil.isTollFreeDate(configData, LocalDate.parse("2023-03-27")));
		assertFalse(TollUtil.isTollFreeDate(configData, LocalDate.parse("2023-03-31")));
	}

	@Test
	public void testTollFreeJulyMonths() {
		// Different dates in 2023/07
		assertTrue(TollUtil.isTollFreeDate(configData, LocalDate.parse("2023-07-05")));
		assertTrue(TollUtil.isTollFreeDate(configData, LocalDate.parse("2023-07-10")));
		assertTrue(TollUtil.isTollFreeDate(configData, LocalDate.parse("2023-07-30")));

		// Wrong year
		assertFalse(TollUtil.isTollFreeDate(configData, LocalDate.parse("2021-07-05")));
		assertFalse(TollUtil.isTollFreeDate(configData, LocalDate.parse("2022-07-08")));
		assertFalse(TollUtil.isTollFreeDate(configData, LocalDate.parse("2014-07-01")));
	}

	@Test
	public void testRandomDays() {
		assertTrue(TollUtil.isTollFreeDate(configData, LocalDate.parse("2023-03-28")));
		assertTrue(TollUtil.isTollFreeDate(configData, LocalDate.parse("2023-06-05")));
		assertTrue(TollUtil.isTollFreeDate(configData, LocalDate.parse("2023-12-26")));

		// Wrong day (not weekend)
		assertFalse(TollUtil.isTollFreeDate(configData, LocalDate.parse("2023-03-09")));
		assertFalse(TollUtil.isTollFreeDate(configData, LocalDate.parse("2023-06-07")));
		assertFalse(TollUtil.isTollFreeDate(configData, LocalDate.parse("2023-12-22")));

		// Wrong year
		assertFalse(TollUtil.isTollFreeDate(configData, LocalDate.parse("2022-03-28")));
		assertFalse(TollUtil.isTollFreeDate(configData, LocalDate.parse("2024-11-01")));
	}

	@Test
	public void testDatesOnSameDaySuccess() {
		assertTrue(TollUtil.checkSameDay(LocalDateTime.parse("2023-03-28T06:00:00"),
				LocalDateTime.parse("2023-03-28T15:30:00"), LocalDateTime.parse("2023-03-28T19:59:59")));
	}

	@Test
	public void testOneDateSuccess() {
		assertTrue(TollUtil.checkSameDay(LocalDateTime.parse("2023-03-28T18:00:00")));
	}

	@Test
	public void testTripsFee() {

		testTollFeeCalculation(8, LocalDateTime.parse("2023-03-23T06:15:01"));
		testTollFeeCalculation(13, LocalDateTime.parse("2023-03-23T15:15:01"));
		testTollFeeCalculation(18, LocalDateTime.parse("2023-03-23T15:59:01"));

		testTollFeeCalculation(0, LocalDateTime.parse("2023-03-27T16:59:01"),
				LocalDateTime.parse("2023-03-28T00:00:00"));
		testTollFeeCalculation(18, LocalDateTime.parse("2023-03-23T16:59:01"),
				LocalDateTime.parse("2023-03-23T00:00:00"));

		// All different times in same day
		testTollFeeCalculation(34, LocalDateTime.parse("2023-03-23T06:59:01"),
				LocalDateTime.parse("2023-03-23T13:15:01"), LocalDateTime.parse("2023-03-23T18:00:00"));

		// Few with-in one hour, few not
		testTollFeeCalculation(47, LocalDateTime.parse("2023-03-23T06:00:00"),
				LocalDateTime.parse("2023-03-23T06:45:00"), LocalDateTime.parse("2023-03-23T07:05:00"),
				LocalDateTime.parse("2023-03-23T07:35:00"), LocalDateTime.parse("2023-03-23T08:40:00"),
				LocalDateTime.parse("2023-03-23T09:10:00"), LocalDateTime.parse("2023-03-23T12:15:00"));
		
		// With-in one hour trips
		testTollFeeCalculation(13, LocalDateTime.parse("2023-03-23T17:05:00"),
				LocalDateTime.parse("2023-03-23T17:10:00"), LocalDateTime.parse("2023-03-23T17:25:00"),
				LocalDateTime.parse("2023-03-23T17:35:00"), LocalDateTime.parse("2023-03-23T18:05:00"));
	}

	private void testTollFeeCalculation(int expectedFee, LocalDateTime... dates) {
		int calculatedTollFee = new TollCalculator().calculateTotalTollFee(new Car(), dates);
		assertEquals("Fee Calculation", expectedFee, calculatedTollFee);
	}

}
