package se.raihle.tollcalculator;

import java.util.Calendar;

public interface Vehicle {
	int getTollAt(Calendar timeOfPassing);
}
