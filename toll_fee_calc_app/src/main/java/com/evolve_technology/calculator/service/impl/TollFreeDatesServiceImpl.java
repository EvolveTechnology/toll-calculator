package com.evolve_technology.calculator.service.impl;

import java.time.DayOfWeek;
import java.time.LocalDate;
import java.time.Month;
import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.Stream;

import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Service;

import com.evolve_technology.calculator.constant.TollFreeDates;
import com.evolve_technology.calculator.exception.CustomErrorException;
import com.evolve_technology.calculator.service.TollFreeDatesService;


@Service
public class TollFreeDatesServiceImpl implements TollFreeDatesService {

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
