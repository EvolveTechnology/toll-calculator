package com.afry.toll;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.fail;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;

import org.junit.jupiter.api.Test;

import com.afry.toll.Car;
import com.afry.toll.Motorbike;
import com.afry.toll.TollCalculator;
import com.afry.toll.Vehicle;

public class TollCalculatorTest {
	private static final SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm");
	
	@Test
	public void testTollFeeMultipleTimes() {
		TollCalculator calculator = new TollCalculator();
		Vehicle c = new Car();
		Date time1 = getDateTime("2021-03-19 06:45"); //13
		int toll = calculator.getTollFee(c, time1);
		assertEquals(13, toll);
		
		Date time2 = getDateTime("2021-03-19 07:50"); //18
		toll = calculator.getTollFee(c, time1, time2);
		assertEquals(31, toll);
		
		time2 = getDateTime("2021-03-19 07:21"); //18
		toll = calculator.getTollFee(c, time1, time2);
		assertEquals(18, toll);
		
		Date time3 = getDateTime("2021-03-19 08:29"); // 13
		Date time4 = getDateTime("2021-03-19 05:29"); // 0
		toll = calculator.getTollFee(c, time1, time2, time3, time4);
		assertEquals(31, toll);	
		
		try {
			time2 = getDateTime("2021-03-20 05:21"); //18
			toll = calculator.getTollFee(c, time1, time2);
			fail();
		} catch (RuntimeException e) {
			
		}
		
		time2 = getDateTime("2021-03-19 07:21"); //18
		Date time5 = getDateTime("2021-03-19 09:45"); // 8
		Date time6 = getDateTime("2021-03-19 10:50"); // 8
		Date time7 = getDateTime("2021-03-19 11:55"); // 8
		Date time8 = getDateTime("2021-03-19 15:05"); // 13
		toll = calculator.getTollFee(c, time1, time2, time3, time4, time5, time6, time7, time8);
		assertEquals(60, toll);	
		
		Date time9 = getDateTime("2021-03-19 16:05"); // 13
		toll = calculator.getTollFee(c, time1, time2, time3, time4, time5, time6, time7, time8, time9);
		assertEquals(60, toll);	
	}
	
	@Test
	public void testTollFeePerOneUsage() {
		TollCalculator calc = new TollCalculator();
		Vehicle c = new Car();
		
		//Toll for each time bands
		assertEquals(0, calc.getTollFee(c, getDateTime("2021-03-19 05:22")));
		assertEquals(8, calc.getTollFee(c, getDateTime("2021-03-19 06:22")));
		assertEquals(13, calc.getTollFee(c, getDateTime("2021-03-19 06:45")));
		assertEquals(18, calc.getTollFee(c, getDateTime("2021-03-19 07:25")));
		assertEquals(13, calc.getTollFee(c, getDateTime("2021-03-19 08:25")));
		assertEquals(8, calc.getTollFee(c, getDateTime("2021-03-19 12:51")));
		assertEquals(13, calc.getTollFee(c, getDateTime("2021-03-19 15:15")));
		assertEquals(18, calc.getTollFee(c, getDateTime("2021-03-19 16:15")));
		assertEquals(13, calc.getTollFee(c, getDateTime("2021-03-19 17:15")));
		assertEquals(8, calc.getTollFee(c, getDateTime("2021-03-19 18:29")));
		assertEquals(0, calc.getTollFee(c, getDateTime("2021-03-19 20:51")));
		
		//Toll free vehicle
		assertEquals(0, calc.getTollFee(new Motorbike(), getDateTime("2021-03-19 12:51")));
		
		//Holidays in 2013
		assertEquals(0, calc.getTollFee(c, getDateTime("2013-03-28 12:51")));
		
		//Week end
		assertEquals(0, calc.getTollFee(c, getDateTime("2022-03-19 08:25")));
		assertEquals(0, calc.getTollFee(c, getDateTime("2022-03-20 08:25")));
		
	}
	
	private Date getDateTime(String dateTime) {
		try {
			return sdf.parse(dateTime);
		} catch (ParseException e) {
			e.printStackTrace();
		}
		return null;
	}
}
