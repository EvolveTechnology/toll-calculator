package com.evolve_technology.calculator.util;

import java.time.LocalDate;

import com.evolve_technology.calculator.service.TollFreeDatesService;
import com.evolve_technology.calculator.service.TollFreeVehicleService;
import com.evolve_technology.calculator.service.impl.TollFreeDatesServiceImpl;
import com.evolve_technology.calculator.service.impl.TollFreeVehiclesServiceImpl;

public class TollUtil {
	TollFreeDatesService tollFreeDatesService;

	TollFreeVehicleService tollFreeVehicleService;
	
	public TollUtil() {
		tollFreeDatesService=new TollFreeDatesServiceImpl();
		tollFreeVehicleService=new TollFreeVehiclesServiceImpl();
	}
	
	
	public int tollCompute(String vehicle, LocalDate localDate, int hour, int minute) {
		if (tollFreeDatesService.isTollFreeDate(localDate.toString())
				|| tollFreeVehicleService.isTollFreeVehicle(vehicle))
			return 0;
		return TollRules.getHourlyRate(hour,minute);
	}
	
}
