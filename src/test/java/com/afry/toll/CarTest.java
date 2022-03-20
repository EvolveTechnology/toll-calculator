package com.afry.toll;

import static org.junit.jupiter.api.Assertions.assertEquals;

import org.junit.jupiter.api.Test;

import com.afry.toll.Car;

public class CarTest {
	@Test
	public void testType() {
		Car c = new Car();
		assertEquals("Car", c.getType());
		
	}

}
