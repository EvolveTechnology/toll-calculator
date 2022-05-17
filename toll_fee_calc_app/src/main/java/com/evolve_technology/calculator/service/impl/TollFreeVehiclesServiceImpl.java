package com.evolve_technology.calculator.service.impl;

import java.util.List;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import com.evolve_technology.calculator.properties.TollConfiguration;
import com.evolve_technology.calculator.service.TollFreeVehicleService;

public class TollFreeVehiclesServiceImpl implements TollFreeVehicleService {

	TollConfiguration tollConfiguration;

	private static final Logger logger = LogManager.getLogger(TollFreeVehiclesServiceImpl.class);

	public TollFreeVehiclesServiceImpl(TollConfiguration tollConfiguration) {
		this.tollConfiguration = tollConfiguration;
	}

	public List<String> getTollFreeVehicles() {
		logger.info("Inside getTollFreeVehicles method ");
		return tollConfiguration.getVehicles();
	}

	public boolean isTollFreeVehicle(String vehicle) {
		logger.info("Inside isTollFreeVehicle method :: vehicle = {} ", vehicle);
		List<String> vehicles = getTollFreeVehicles();
		return vehicles.stream().filter(v -> v.trim().equalsIgnoreCase(vehicle)).findAny().isPresent();
	}

}
