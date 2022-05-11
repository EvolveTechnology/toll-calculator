package com.evolve_technology.calculator.util;

import java.time.LocalDate;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import com.evolve_technology.calculator.service.TollFreeDatesService;
import com.evolve_technology.calculator.service.TollFreeVehicleService;

@Component
public class TollUtil {
	@Autowired
	TollFreeDatesService tollFreeDatesService;

	@Autowired
	TollFreeVehicleService tollFreeVehicleService;
	
	
	public int tollCompute(String vehicle, LocalDate localDate, int hour, int minute) {
		if (tollFreeDatesService.isTollFreeDate(localDate.toString())
				|| tollFreeVehicleService.isTollFreeVehicle(vehicle))
			return 0;
		return TollRules.getHourlyRate(hour,minute);
	}
	
}
