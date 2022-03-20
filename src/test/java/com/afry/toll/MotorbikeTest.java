package com.afry.toll;

import static org.junit.jupiter.api.Assertions.assertEquals;

import org.junit.jupiter.api.Test;

import com.afry.toll.Motorbike;

public class MotorbikeTest {
	@Test
	public void testType() {
		Motorbike m = new Motorbike();
		assertEquals("Motorbike", m.getType());
		
	}
}
