package com.evolve_technology.calculator.service.impl;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNotEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.Stream;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import com.evolve_technology.calculator.exception.CustomErrorException;
import com.evolve_technology.calculator.properties.TollConfiguration;
import com.evolve_technology.calculator.service.TollFeeService;
import com.evolve_technology.calculator.service.TollFreeDatesService;
import com.evolve_technology.calculator.service.TollFreeVehicleService;
import com.evolve_technology.calculator.util.TollUtil;

class TollFeeServiceImplTest {

	TollFeeService tollFeeService;

	@BeforeEach
	public void setup() {
		TollConfiguration tollConfiguration = new TollConfiguration();
		TollFreeVehicleService tollFreeVehicleService = new TollFreeVehiclesServiceImpl(
				tollConfiguration);
		TollFreeDatesService tollFreeDatesService = new TollFreeDatesServiceImpl(
				tollConfiguration);
		TollUtil tollUtil = new TollUtil(tollFreeDatesService,
				tollFreeVehicleService);
		tollFeeService = new TollFeeServiceImpl(tollUtil);

	}

	/*
	 * Jan 2013 5,12,19,26 are saturday
	 */
	@Test
	void testGetTollFeeSaturdayJanuary2013() {

		List<LocalDateTime> inputDates = Stream
				.of(LocalDateTime.of(2013, 01, 05, 8, 8),
						LocalDateTime.of(2013, 01, 12, 8, 8),
						LocalDateTime.of(2013, 01, 19, 8, 8),
						LocalDateTime.of(2013, 01, 26, 8, 8))
				.collect(Collectors.toList());
		assertEquals(tollFeeService.getTollFee(inputDates, "car"), 0);
	}

	/*
	 * Null as inputs
	 */
	@Test
	void testGetTollFeeWithNullInputs() {
		// Both input null
		assertThrows(CustomErrorException.class,
				() -> tollFeeService.getTollFee(null, null));

		// only vehicle null
		List<LocalDateTime> inputDates = Stream
				.of(LocalDateTime.of(2013, 01, 05, 8, 8),
						LocalDateTime.of(2013, 01, 12, 8, 8),
						LocalDateTime.of(2013, 01, 19, 8, 8),
						LocalDateTime.of(2013, 01, 26, 8, 8))
				.collect(Collectors.toList());

		assertThrows(CustomErrorException.class,
				() -> tollFeeService.getTollFee(inputDates, null));

		List<LocalDateTime> inputDatesEmpty = new ArrayList<>();
		assertThrows(CustomErrorException.class,
				() -> tollFeeService.getTollFee(inputDatesEmpty, ""));
	}

	/*
	 * Feb 2013 2,9,16,23 are saturday
	 */
	@Test
	void testGetTollFeeSaturdayFebruary2013() {
		List<LocalDateTime> inputDates = Stream
				.of(LocalDateTime.of(2013, 02, 02, 8, 8),
						LocalDateTime.of(2013, 02, 16, 8, 8),
						LocalDateTime.of(2013, 02, 9, 8, 8),
						LocalDateTime.of(2013, 02, 23, 8, 8))
				.collect(Collectors.toList());
		assertEquals(tollFeeService.getTollFee(inputDates, "car"), 0);
	}

	/*
	 * Feb 2013 4,11,18,25 are Monday NON Free Vehicles Test
	 */
	@Test
	void testGetTollFeeMondayFebruary2013Car() {
		List<LocalDateTime> inputDates = Stream
				.of(LocalDateTime.of(2013, 02, 04, 8, 8),
						LocalDateTime.of(2013, 02, 18, 8, 8),
						LocalDateTime.of(2013, 02, 11, 8, 8),
						LocalDateTime.of(2013, 02, 25, 8, 8))
				.collect(Collectors.toList());
		assertNotEquals(tollFeeService.getTollFee(inputDates, "car"), 0);

	}

	/*
	 * Feb 2013 4,11,18,25 are Monday Free Vehicles Test
	 */
	@Test
	void testGetTollFeeMondayFebruary2013FreeVehicles() {
		List<LocalDateTime> inputDates = Stream
				.of(LocalDateTime.of(2013, 02, 04, 8, 8),
						LocalDateTime.of(2013, 02, 18, 8, 8),
						LocalDateTime.of(2013, 02, 11, 8, 8),
						LocalDateTime.of(2013, 02, 25, 8, 8))
				.collect(Collectors.toList());

		assertEquals(tollFeeService.getTollFee(inputDates, "Tractor"), 0);
		assertEquals(tollFeeService.getTollFee(inputDates, "Motorbike"), 0);
		assertEquals(tollFeeService.getTollFee(inputDates, "Emergency"), 0);
		assertEquals(tollFeeService.getTollFee(inputDates, "Diplomat"), 0);
		assertEquals(tollFeeService.getTollFee(inputDates, "Foreign"), 0);
	}

	/*
	 * Feb 2013 Monday 4th 04-02-2013 06:10 As per the Rule it is charged 8.
	 */
	@Test
	void testGetTollFeeMondayFebruary2013At6Hr10Min() {
		List<LocalDateTime> inputDates = Stream
				.of(LocalDateTime.of(2013, 02, 04, 06, 10))
				.collect(Collectors.toList());

		assertEquals(tollFeeService.getTollFee(inputDates, "Car"), 8);
	}

	/*
	 * Feb 2013 Monday 4th 04-02-2013 06:10 && 06:20 As per the Rule it is
	 * charged 8. if (hour == 6 && minute >= 0 && minute <= 29) return 8; else
	 * if (hour == 6 && minute >= 30 && minute <= 59) return 13;
	 */
	@Test
	void testGetTollFeeMondayFebruary2013At6Hr10MinAnd20Min() {
		List<LocalDateTime> inputDates = Stream
				.of(LocalDateTime.of(2013, 02, 04, 06, 10),
						LocalDateTime.of(2013, 02, 04, 06, 20))
				.collect(Collectors.toList());

		assertEquals(tollFeeService.getTollFee(inputDates, "Car"), 8);
	}

	/*
	 * Feb 2013 Monday 4th 04-02-2013 06:10 && 06:20 && 06:40 As per the Rule
	 * max within an hour is charged that is 13 max of(8,13)
	 */
	@Test
	void testGetTollFeeMondayFebruary2013At6Hr10MinAnd20MinAnd40Min() {
		List<LocalDateTime> inputDates = Stream
				.of(LocalDateTime.of(2013, 02, 04, 06, 10),
						LocalDateTime.of(2013, 02, 04, 06, 20),
						LocalDateTime.of(2013, 02, 04, 06, 40))
				.collect(Collectors.toList());

		assertEquals(tollFeeService.getTollFee(inputDates, "Car"), 13);
	}

	/*
	 * Feb 2013 Monday 4th 04-02-2013 06:10 && 06:20 && 06:40 && 07:10 As per
	 * the Rule max within an hour is charged that is max of 8,13 plus 18 =31
	 */
	@Test
	void testGetTollFeeMondayFebruary2013At6Hr10MinAnd20MinAnd40MinAnd7Hr10Min() {
		List<LocalDateTime> inputDates = Stream
				.of(LocalDateTime.of(2013, 02, 04, 06, 10),
						LocalDateTime.of(2013, 02, 04, 06, 20),
						LocalDateTime.of(2013, 02, 04, 06, 40),
						LocalDateTime.of(2013, 02, 04, 07, 10))
				.collect(Collectors.toList());

		assertEquals(tollFeeService.getTollFee(inputDates, "Car"), 31);
	}

	/*
	 * Feb 2013 Monday 4th 04-02-2013 06:10 && 06:20 && 06:40 && 07:10 && 08:10
	 * As per the Rule max within an hour is charged that is max of 8,13 plus 18
	 * =31 plus 13 =44
	 */
	@Test
	void testGetTollFeeMondayFebruary2013At6Hr10MinAnd20MinAnd40MinAnd7Hr10MinAnd8Hr10Min() {
		List<LocalDateTime> inputDates = Stream
				.of(LocalDateTime.of(2013, 02, 04, 06, 10),
						LocalDateTime.of(2013, 02, 04, 06, 20),
						LocalDateTime.of(2013, 02, 04, 06, 40),
						LocalDateTime.of(2013, 02, 04, 07, 10),
						LocalDateTime.of(2013, 02, 04, 8, 10))
				.collect(Collectors.toList());

		assertEquals(tollFeeService.getTollFee(inputDates, "Car"), 44);
	}

	/*
	 * Feb 2013 Monday 4th 04-02-2013 06:10 && 06:20 && 06:40 && 07:10 && 08:10
	 * && 08:30 As per the Rule max within an hour is charged that is max of
	 * 8,13 plus 18 =31 plus 13 =44
	 */
	@Test
	void testGetTollFeeMondayFebruary2013At6Hr10MinAnd20MinAnd40MinAnd7Hr10MinAnd8Hr10MinAnd8Hr30Min() {
		List<LocalDateTime> inputDates = Stream
				.of(LocalDateTime.of(2013, 02, 04, 06, 10),
						LocalDateTime.of(2013, 02, 04, 06, 20),
						LocalDateTime.of(2013, 02, 04, 06, 40),
						LocalDateTime.of(2013, 02, 04, 07, 10),
						LocalDateTime.of(2013, 02, 04, 8, 10),
						LocalDateTime.of(2013, 02, 04, 8, 30))
				.collect(Collectors.toList());

		assertEquals(tollFeeService.getTollFee(inputDates, "Car"), 44);
	}

	/*
	 * Feb 2013 Monday 4th 04-02-2013 06:10 && 06:20 && 06:40 && 07:10 && 08:10
	 * && 08:30 && 15:10 As per the Rule max within an hour is charged that is
	 * max of 8,13 plus 18 =31 plus 13 =44 plus 13= 57
	 */
	@Test
	void testGetTollFeeMondayFebruary2013At6Hr10MinAnd20MinAnd40MinAnd7Hr10MinAnd8Hr10MinAnd8Hr30MinAnd15Hr10Min() {
		List<LocalDateTime> inputDates = Stream
				.of(LocalDateTime.of(2013, 02, 04, 06, 10),
						LocalDateTime.of(2013, 02, 04, 06, 20),
						LocalDateTime.of(2013, 02, 04, 06, 40),
						LocalDateTime.of(2013, 02, 04, 07, 10),
						LocalDateTime.of(2013, 02, 04, 8, 10),
						LocalDateTime.of(2013, 02, 04, 8, 30),
						LocalDateTime.of(2013, 02, 04, 15, 10))
				.collect(Collectors.toList());

		assertEquals(tollFeeService.getTollFee(inputDates, "Car"), 57);
	}

	/*
	 * Feb 2013 Monday 4th 04-02-2013 06:10 && 06:20 && 06:40 && 07:10 && 08:10
	 * && 08:30 && 15:10 && 15:30 As per the Rule max within an hour is charged
	 * that is max of 8,13 plus 18 =31 plus 13 =44 plus max of (13,18) is 18= 62
	 * but it can't go beyond 60 for a day.
	 */
	@Test
	void testGetTollFeeMondayFebruary2013At6Hr10MinAnd20MinAnd40MinAnd7Hr10MinAnd8Hr10MinAnd8Hr30MinAnd15Hr10MinAnd15HrMin30() {
		List<LocalDateTime> inputDates = Stream
				.of(LocalDateTime.of(2013, 02, 04, 06, 10),
						LocalDateTime.of(2013, 02, 04, 06, 20),
						LocalDateTime.of(2013, 02, 04, 06, 40),
						LocalDateTime.of(2013, 02, 04, 07, 10),
						LocalDateTime.of(2013, 02, 04, 8, 10),
						LocalDateTime.of(2013, 02, 04, 8, 30),
						LocalDateTime.of(2013, 02, 04, 15, 10),
						LocalDateTime.of(2013, 02, 04, 15, 30))
				.collect(Collectors.toList());

		assertEquals(tollFeeService.getTollFee(inputDates, "Car"), 60);
	}

	/*
	 * Feb 2013 Monday 4th 04-02-2013 06:10 && 06:20 && 06:40 && 07:10 && 08:10
	 * && 08:30 && 15:10 && 15:30 As per the Rule max within an hour is charged
	 * that is max of 8,13 plus 18 =31 plus 13 =44 plus max of (13,18) is 18= 62
	 * but it can't go beyond 60 for a day.
	 * 
	 * And Feb 2013 Tuesday 5th 05-02-2013 06:10
	 * 
	 * Monday=60 plus Tuesday=8= 68
	 */
	@Test
	void testGetTollFeeMondayFebruary2013MultipleDatesMondayPlusTuesday() {
		List<LocalDateTime> inputDates = Stream
				.of(LocalDateTime.of(2013, 02, 04, 06, 10),
						LocalDateTime.of(2013, 02, 04, 06, 20),
						LocalDateTime.of(2013, 02, 04, 06, 40),
						LocalDateTime.of(2013, 02, 04, 07, 10),
						LocalDateTime.of(2013, 02, 04, 8, 10),
						LocalDateTime.of(2013, 02, 04, 8, 30),
						LocalDateTime.of(2013, 02, 04, 15, 10),
						LocalDateTime.of(2013, 02, 04, 15, 30),
						LocalDateTime.of(2013, 02, 05, 6, 10))
				.collect(Collectors.toList());

		assertEquals(tollFeeService.getTollFee(inputDates, "Car"), 68);
	}

	/*
	 * Feb 2013 Monday 4th 04-02-2013 06:10 && 06:20 && 06:40 && 07:10 && 08:10
	 * && 08:30 && 15:10 && 15:30 As per the Rule max within an hour is charged
	 * that is max of 8,13 plus 18 =31 plus 13 =44 plus max of (13,18) is 18= 62
	 * but it can't go beyond 60 for a day.
	 * 
	 * And Feb 2013 Tuesday 5th 05-02-2013 06:10
	 * 
	 * And Feb 2013 Wednesday 6th 06-02-2013 17:10 && 18:10
	 * 
	 * Monday=60 plus Tuesday=8 plus Wednesday= 13 plus 8 =89
	 */
	@Test
	void testGetTollFeeMondayFebruary2013MultipleDatesMondayPlusTuesdayPlusWednesday() {
		List<LocalDateTime> inputDates = Stream
				.of(LocalDateTime.of(2013, 02, 04, 06, 10),
						LocalDateTime.of(2013, 02, 04, 06, 20),
						LocalDateTime.of(2013, 02, 04, 06, 40),
						LocalDateTime.of(2013, 02, 04, 07, 10),
						LocalDateTime.of(2013, 02, 04, 8, 10),
						LocalDateTime.of(2013, 02, 04, 8, 30),
						LocalDateTime.of(2013, 02, 04, 15, 10),
						LocalDateTime.of(2013, 02, 04, 15, 30),
						LocalDateTime.of(2013, 02, 05, 6, 10),
						LocalDateTime.of(2013, 02, 06, 17, 10),
						LocalDateTime.of(2013, 02, 06, 18, 10))
				.collect(Collectors.toList());

		assertEquals(tollFeeService.getTollFee(inputDates, "Car"), 89);
	}
}
