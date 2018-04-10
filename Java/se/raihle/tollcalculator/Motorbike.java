package se.raihle.tollcalculator;

import java.time.LocalDateTime;

public class Motorbike implements Vehicle {
	@Override
	public int getTollAt(LocalDateTime timeOfPassing) {
		return 0;
	}
}
