package com.evolve_technology.calculator.service.impl;

import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.Stream;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.springframework.stereotype.Service;

import com.evolve_technology.calculator.constant.TollFreeVehicles;
import com.evolve_technology.calculator.service.TollFreeVehicleService;


@Service
public class TollFreeVehiclesServiceImpl implements TollFreeVehicleService {

	private static final Logger logger = LogManager.getLogger(TollFreeVehiclesServiceImpl.class);
	
	public List<String> getTollFreeVehicles() {
		logger.info("Inside getTollFreeVehicles method ");
		List<String> list=Stream.of(TollFreeVehicles.values()).map(TollFreeVehicles::getType).collect(Collectors.toList());
		return list;
	}

	public boolean isTollFreeVehicle(String vehicle) {
		logger.info("Inside isTollFreeVehicle method :: vehicle = {} ",vehicle);
		 for (TollFreeVehicles tollFreeVehicle : TollFreeVehicles.values())
		    {
		        if (tollFreeVehicle.name().equalsIgnoreCase(vehicle))
		        {
		            return true;
		        }
		    }
		return false;
	}


}
