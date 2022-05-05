package com.evolve_technology.calculator.service.impl;

import java.time.DayOfWeek;
import java.time.LocalDate;
import java.util.List;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.evolve_technology.calculator.properties.TollConfiguration;
import com.evolve_technology.calculator.service.TollFreeDatesService;


@Service
public class TollFreeDatesServiceImpl implements TollFreeDatesService {
	private static final Logger logger = LogManager.getLogger(TollFreeDatesServiceImpl.class);
	
	@Autowired
	TollConfiguration tollConfiguration;
	
	@Override
	public Boolean isTollFreeDate(String date) {
		logger.info("Inside isTollFreeDate method :: date = {} ",date);

		LocalDate input=LocalDate.parse(date);
		if( input.getDayOfWeek()==DayOfWeek.SATURDAY || input.getDayOfWeek()==DayOfWeek.SUNDAY) {
			logger.info("date {} is actually a weekend so no toll Enjoy.",date);
			return true;
		}
		
		if(tollConfiguration.getMonths().stream().filter(month-> month.trim().equalsIgnoreCase(input.getMonth().toString())).findAny().isPresent()) {
			logger.info("date {} lies in JULY month so no toll Enjoy.",date);
			return true;
		}
		
		return getTollFreeDates().stream().filter(x -> x.trim().equalsIgnoreCase(input.toString())).findAny().isPresent();
	}

	@Override
	public List<String> getTollFreeDates() {
		logger.info("Inside getTollFreeDates method ");
		return tollConfiguration.getDates();
	}


}
