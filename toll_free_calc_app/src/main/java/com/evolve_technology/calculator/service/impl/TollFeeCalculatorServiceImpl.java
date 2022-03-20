package com.evolve_technology.calculator.service.impl;

import java.time.DayOfWeek;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.Month;
import java.time.format.DateTimeFormatter;
import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.Stream;

import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Service;

import com.evolve_technology.calculator.constant.TollFreeDates;
import com.evolve_technology.calculator.constant.TollFreeVehicles;
import com.evolve_technology.calculator.exception.CustomErrorException;
import com.evolve_technology.calculator.service.TollFeeCalculatorService;

@Service
public class TollFeeCalculatorServiceImpl implements TollFeeCalculatorService {

	public List<String> getTollFreeVehicles() {
		List<String> list=Stream.of(TollFreeVehicles.values()).map(TollFreeVehicles::getType).collect(Collectors.toList());
		return list;
	}

	public boolean isTollFreeVehicle(String vehicle) {
		 for (TollFreeVehicles tollFreeVehicle : TollFreeVehicles.values())
		    {
		        if (tollFreeVehicle.name().equalsIgnoreCase(vehicle))
		        {
		            return true;
		        }
		    }
		return false;
	}

	@Override
	public int getTollFee(String date, String vehicle) {
		DateTimeFormatter formatter = DateTimeFormatter.ISO_LOCAL_DATE_TIME;
		LocalDateTime inputDate=LocalDateTime.parse(date,formatter);
		int hour=inputDate.getHour();
		int minute=inputDate.getMinute();
		LocalDate localDate=LocalDate.of(inputDate.getYear(), inputDate.getMonthValue(), inputDate.getDayOfMonth());
		
		if(isTollFreeDate(localDate.toString())||isTollFreeVehicle(vehicle))
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

	@Override
	public Boolean isTollFreeDate(String date) {
		try {
		LocalDate input=LocalDate.parse(date);
		if(input.getMonth()==Month.JULY || input.getDayOfWeek()==DayOfWeek.SATURDAY || input.getDayOfWeek()==DayOfWeek.SUNDAY)
			return true;
		for (TollFreeDates tollFreeDate : TollFreeDates.values()) {
			 	if(!tollFreeDate.getDate().equals("2013-07")) {
			 		LocalDate dateEnum=LocalDate.parse(tollFreeDate.getDate());
			        if (dateEnum.equals(input)) {
			            return true;
			        }
			 	}
			 	
		    }
		}catch(Exception e) {
			throw new CustomErrorException(HttpStatus.BAD_REQUEST, e.getMessage(), date);
		}
		return false;
	}

	@Override
	public List<String> getTollFreeDates() {
		List<String> list=Stream.of(TollFreeDates.values()).map(TollFreeDates::getDate).collect(Collectors.toList());
		return list;
	}

}
