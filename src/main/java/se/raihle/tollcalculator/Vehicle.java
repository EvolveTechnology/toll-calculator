package se.raihle.tollcalculator;

import java.time.LocalTime;

@SuppressWarnings("WeakerAccess")
public interface Vehicle {
	int getTollAt(LocalTime timeOfPassing);
}
