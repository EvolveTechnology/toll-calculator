package se.raihle.tollcalculator;

import se.raihle.tollcalculator.schedule.FeeSchedule;

public interface Vehicles {
	Vehicle CAR = new DefaultVehicle(FeeSchedule.DEFAULT);
	Vehicle MOTORBIKE = new DefaultVehicle(FeeSchedule.FREE);
}
