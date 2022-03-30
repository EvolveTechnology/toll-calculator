package com.evolve_technology.calculator.service.impl;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.evolve_technology.calculator.service.TollFeeService;
import com.evolve_technology.calculator.service.TollFreeDatesService;
import com.evolve_technology.calculator.service.TollFreeVehicleService;

@Service
public class TollFeeServiceImpl implements TollFeeService {

	private static final Logger logger = LogManager.getLogger(TollFeeServiceImpl.class);
	
	@Autowired
	TollFreeDatesService tollFreeDatesService;
	
	@Autowired
	TollFreeVehicleService tollFreeVehicleService;
	
	
	@Override
	public int getTollFee(String date, String vehicle) {
		
		logger.info("Inside getTollFee method :: date = {} and vehicle = {}",date,vehicle);
		
		DateTimeFormatter formatter = DateTimeFormatter.ISO_LOCAL_DATE_TIME;
		LocalDateTime inputDate=LocalDateTime.parse(date,formatter);
		int hour=inputDate.getHour();
		int minute=inputDate.getMinute();
		LocalDate localDate=LocalDate.of(inputDate.getYear(), inputDate.getMonthValue(), inputDate.getDayOfMonth());
		
		if(tollFreeDatesService.isTollFreeDate(localDate.toString())||tollFreeVehicleService.isTollFreeVehicle(vehicle))
			return 0;
		if (hour == 6 && minute >= 0 && minute <= 29) return 8;
		    else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
		    else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
		    else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
		    else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
		    else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
		    else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
		    else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
		    else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
		    else return 0;
	}

	
}
