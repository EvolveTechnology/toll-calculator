package com.evolve;

import static org.junit.jupiter.api.Assertions.assertEquals;

import java.time.LocalTime;

import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;

import com.evolve.service.TollTimeFeeService;
import com.evolve.service.TollTimeFeeServiceImpl;


public class TollTimeFeeServiceTest {

	TollTimeFeeService tollTimeFeeService;
	
	public TollTimeFeeServiceTest() {
		tollTimeFeeService = new TollTimeFeeServiceImpl();
	}

	@DisplayName("TOLL TIME FEE FROM 00:00 TO 5:59")
	@Test
	public void tollTimeFeeFrom00To5_59() {
		assertEquals(0,  tollTimeFeeService.getFee(LocalTime.of(0, 0))); //00:00
		assertEquals(0,  tollTimeFeeService.getFee(LocalTime.of(5, 59)));//5:59
	}
	
	@DisplayName("TOLL TIME FEE FROM 6:00 TO 6:29")
	@Test
	public void tollTimeFeeFrom6_00To6_29() {
		assertEquals(8,  tollTimeFeeService.getFee(LocalTime.of(6, 0))); //6:00
		assertEquals(8,  tollTimeFeeService.getFee(LocalTime.of(6, 29)));//6:29
	}
	
	@DisplayName("TOLL TIME FEE FROM 6:30 TO 6:59")
	@Test
	public void tollTimeFeeFrom6_30To6_59() {
		assertEquals(13,  tollTimeFeeService.getFee(LocalTime.of(6, 30))); //6:30
		assertEquals(13,  tollTimeFeeService.getFee(LocalTime.of(6, 59)));//6:59
	}
	
	@DisplayName("TOLL TIME FEE FROM 7:00 TO 7:59")
	@Test
	public void tollTimeFeeFrom7_00To7_59() {
		assertEquals(18,  tollTimeFeeService.getFee(LocalTime.of(7, 0))); //7:00
		assertEquals(18,  tollTimeFeeService.getFee(LocalTime.of(7, 59)));//7:59
	}
	
	@DisplayName("TOLL TIME FEE FROM 8:00 TO 8:29")
	@Test
	public void tollTimeFeeFrom8_00To8_29() {
		assertEquals(13,  tollTimeFeeService.getFee(LocalTime.of(8, 0))); //8:00
		assertEquals(13,  tollTimeFeeService.getFee(LocalTime.of(8, 29)));//8:29
	}
	
	@DisplayName("TOLL TIME FEE FROM 8:30 TO 14:59")
	@Test
	public void tollTimeFeeFrom8_30To14_59() {
		assertEquals(8,  tollTimeFeeService.getFee(LocalTime.of(8, 30))); //8:30
		assertEquals(8,  tollTimeFeeService.getFee(LocalTime.of(14, 59)));//14:59
	}
	
	@DisplayName("TOLL TIME FEE FROM 15:00 TO 15:59")
	@Test
	public void tollTimeFeeFrom15_00To15_29() {
		assertEquals(13,  tollTimeFeeService.getFee(LocalTime.of(15, 0))); //15:00
		assertEquals(13,  tollTimeFeeService.getFee(LocalTime.of(15, 29)));//15:29
	}
	
	@DisplayName("TOLL TIME FEE FROM 15:30 TO 16:29")
	@Test
	public void tollTimeFeeFrom15_30To16_59() {
		assertEquals(18,  tollTimeFeeService.getFee(LocalTime.of(15, 30))); //15:30
		assertEquals(18,  tollTimeFeeService.getFee(LocalTime.of(16, 59)));//16:59
	}
	
	@DisplayName("TOLL TIME FEE FROM 17:00 TO 17:59")
	@Test
	public void tollTimeFeeFrom17_00To17_59() {
		assertEquals(13,  tollTimeFeeService.getFee(LocalTime.of(17, 0))); //17:00
		assertEquals(13,  tollTimeFeeService.getFee(LocalTime.of(17, 59)));//17:59
	}
	
	@DisplayName("TOLL TIME FEE FROM 18:00 TO 18:29")
	@Test
	public void tollTimeFeeFrom18_00To18_29() {
		assertEquals(8,  tollTimeFeeService.getFee(LocalTime.of(18, 0))); //18:00
		assertEquals(8,  tollTimeFeeService.getFee(LocalTime.of(18, 29)));//18:29
	}
	
	@DisplayName("TOLL TIME FEE FROM 18:30 TO 23:59")
	@Test
	public void tollTimeFeeFrom18_30To23_59() {
		assertEquals(0,  tollTimeFeeService.getFee(LocalTime.of(18, 30))); //18:30
		assertEquals(0,  tollTimeFeeService.getFee(LocalTime.of(23, 59)));//23:59
	}
}
