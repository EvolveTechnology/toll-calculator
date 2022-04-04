package com.evolve_technology.calculator.service.impl;

import java.util.List;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.evolve_technology.calculator.properties.TollConfiguration;
import com.evolve_technology.calculator.service.TollFreeVehicleService;


@Service
public class TollFreeVehiclesServiceImpl implements TollFreeVehicleService {

	@Autowired
	TollConfiguration tollConfiguration;
	
	private static final Logger logger = LogManager.getLogger(TollFreeVehiclesServiceImpl.class);
	
	public List<String> getTollFreeVehicles() {
		logger.info("Inside getTollFreeVehicles method ");
		return tollConfiguration.getVehicles();
	}

	public boolean isTollFreeVehicle(String vehicle) {
		logger.info("Inside isTollFreeVehicle method :: vehicle = {} ",vehicle);
		List<String> vehicles=getTollFreeVehicles();
		return vehicles.stream().filter(v->v.equalsIgnoreCase(vehicle)).findAny().isPresent();
	}


}
