package se.evolve.tollcalculator;

public enum VehicleType {
	CAR(false), MOTORBIKE(true), TRACTOR(true), EMERGENCY(true), DIPLOMAT(true), FOREIGN(true), MILITARY(true);
	private boolean tollFree;
	
	private VehicleType(boolean tollFree) {
		this.tollFree = tollFree;
	}
	
	public boolean isTollFree() {
		return tollFree;
	}
}
