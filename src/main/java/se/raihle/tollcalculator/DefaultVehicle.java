package se.raihle.tollcalculator;

import se.raihle.tollcalculator.schedule.FeeSchedule;

import java.time.LocalTime;

public class DefaultVehicle implements Vehicle {

	private FeeSchedule feeSchedule;

	public DefaultVehicle(FeeSchedule feeSchedule) {
		this.feeSchedule = feeSchedule;
	}

	@Override
	public int getTollAt(LocalTime timeOfPassing) {
		return feeSchedule.getFeeAt(timeOfPassing);
	}
}
