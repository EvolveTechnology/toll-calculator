package se.raihle.tollcalculator;

import java.time.LocalTime;

public class Motorbike implements Vehicle {
	@Override
	public int getTollAt(LocalTime timeOfPassing) {
		return 0;
	}
}
