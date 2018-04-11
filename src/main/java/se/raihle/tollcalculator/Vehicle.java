package se.raihle.tollcalculator;

import java.time.LocalTime;

public interface Vehicle {
	int getTollAt(LocalTime timeOfPassing);
}
