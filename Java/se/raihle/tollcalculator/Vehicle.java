package se.raihle.tollcalculator;

import java.time.LocalDateTime;

public interface Vehicle {
	int getTollAt(LocalDateTime timeOfPassing);
}
