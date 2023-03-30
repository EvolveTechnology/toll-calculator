package com.presis.code.challenge.toll.model;

public enum VehicleType {

	CAR("Car"), MOTORBIKE("Motorbike"), TRACTOR("Tractor"), EMERGENCY("Emergency"), DIPLOMAT("Diplomat"),
	FOREIGN("Foreign"), MILITARY("Military");

	private final String type;

	VehicleType(String type) {
		this.type = type;
	}

	public String getType() {
		return type;
	}
}
