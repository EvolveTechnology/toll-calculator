package se.raihle.tollcalculator;

import java.util.Calendar;

public class Motorbike implements Vehicle {
	@Override
	public int getTollAt(Calendar timeOfPassing) {
		return 0;
	}
}
