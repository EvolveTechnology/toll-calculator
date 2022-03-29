package com.evolve_technology.calculator.service;

import java.util.List;

public interface TollFreeVehicleService {
	public List<String> getTollFreeVehicles();
	public boolean isTollFreeVehicle(String vehicle);
}
