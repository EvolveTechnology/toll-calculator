package com.evolve_technology.calculator.service.impl;

import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertNotNull;
import static org.junit.Assert.assertTrue;
import static org.junit.jupiter.api.Assertions.assertDoesNotThrow;
import static org.junit.jupiter.api.Assertions.assertThrows;

import java.time.DateTimeException;
import java.time.LocalDate;

import org.junit.Before;
import org.junit.Test;

import com.evolve_technology.calculator.exception.CustomErrorException;


public class TollFeeCalculatorServiceImplTest {

	TollFeeCalculatorServiceImpl tollFeeCalculatorService;
	
	@Before
	public void init() {
		tollFeeCalculatorService=new TollFeeCalculatorServiceImpl();
	}
	
	@Test
	public void getTollFreeVehiclesTest(){
		assertNotNull(tollFeeCalculatorService.getTollFreeVehicles());
		assertDoesNotThrow(()->tollFeeCalculatorService.getTollFreeVehicles());
	}

	@Test
	public void getTollFreeDatesTest() {
		assertNotNull(tollFeeCalculatorService.getTollFreeDates());
		assertDoesNotThrow(()->tollFeeCalculatorService.getTollFreeDates());
	}
	
	@Test
	public void isTollFreeDate_Positive_Scenarios_Test() {
		// JULY MONTH
		String july1=LocalDate.of(2013, 07, 01).toString();
		assertTrue(tollFeeCalculatorService.isTollFreeDate(july1));
		
		String july21=LocalDate.of(2013, 07, 21).toString();
		assertTrue(tollFeeCalculatorService.isTollFreeDate(july21));
		
		// Saturday  verifying
		String june_saturday=LocalDate.of(2013, 06, 8).toString();
		assertTrue(tollFeeCalculatorService.isTollFreeDate(june_saturday));
		
		// sunday Verifying
		String june_sunday=LocalDate.of(2013, 06, 9).toString();
		assertTrue(tollFeeCalculatorService.isTollFreeDate(june_sunday));
		
		// Enum specific holidays Verifying - 2013-01-01  
		String jan1=LocalDate.of(2013, 01, 1).toString();
		assertTrue(tollFeeCalculatorService.isTollFreeDate(jan1));
		
		// Enum specific holidays Verifying - 2013-03-28  
		String mar28=LocalDate.of(2013, 03, 28).toString();
		assertTrue(tollFeeCalculatorService.isTollFreeDate(mar28));
	}
	
	@Test
	public void isTollFreeDate_Negative_Scenarios_Test() {
		// Non Enum specific holidays Verifying - 2013-01-02 Wednesday
		String jan1=LocalDate.of(2013, 01, 2).toString();
		assertFalse(tollFeeCalculatorService.isTollFreeDate(jan1));
		
		// Non Enum specific holidays Verifying - 2013-01-03 Thursday
		String jan3=LocalDate.of(2013, 01, 3).toString();
		assertFalse(tollFeeCalculatorService.isTollFreeDate(jan3));
		
		// Passing Invalid date  2013-02-30
		String feb30=" 2013-02-30";
		assertThrows(CustomErrorException.class,()->tollFeeCalculatorService.isTollFreeDate(feb30));
	}
}
