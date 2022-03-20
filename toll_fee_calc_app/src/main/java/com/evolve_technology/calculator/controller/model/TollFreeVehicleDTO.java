package com.evolve_technology.calculator.controller.model;

import lombok.Data;

@Data
public class TollFreeVehicleDTO {
	String name;
	boolean isTollFreeVehicle;
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public boolean isTollFreeVehicle() {
		return isTollFreeVehicle;
	}
	public void setTollFreeVehicle(boolean isTollFreeVehicle) {
		this.isTollFreeVehicle = isTollFreeVehicle;
	}
	
}
