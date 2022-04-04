package com.evolve_technology.calculator.service.impl;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.HashMap;
import java.util.Map;
import java.util.stream.Collectors;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.evolve_technology.calculator.controller.model.TollFee;
import com.evolve_technology.calculator.properties.TollConfiguration;
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

	private static Map<String, Map<String, Map<Integer, Integer>>> aggregateTollMap = new HashMap<>();
	
	private static boolean vehicleExists = false;

	@Override
	public TollFee getTollFee(String date, String vehicle) {

		logger.info("Inside getTollFee method :: date = {} and vehicle = {}", date, vehicle);

		DateTimeFormatter formatter = DateTimeFormatter.ISO_LOCAL_DATE_TIME;
		LocalDateTime inputDate = LocalDateTime.parse(date, formatter);
		int hour = inputDate.getHour();
		int minute = inputDate.getMinute();
		LocalDate localDate = LocalDate.of(inputDate.getYear(), inputDate.getMonthValue(), inputDate.getDayOfMonth());

		logger.info(" aggregateTollMap = {} ", aggregateTollMap);

		Integer newFee = tollCalculate(vehicle, localDate, hour, minute);
		if (aggregateTollMap.containsKey(vehicle)) {
			vehicleExists = true;
		}
		Map<String, Map<Integer, Integer>> aggregateTollMapValue = aggregateTollMap.computeIfAbsent(vehicle, k -> {
			logger.info("Vehicle {} has crossed the toll first time ", vehicle);
			Map<String, Map<Integer, Integer>> outerMap = new HashMap<>();
			Map<Integer, Integer> innerMap = new HashMap<>();
			innerMap.put(hour, newFee);
			outerMap.put(localDate.toString(), innerMap);
			return outerMap;
		});
		logger.info("key = {} , value = {} ", vehicle, aggregateTollMapValue);

		if (vehicleExists) {
			Map<String, Map<Integer, Integer>> aggregateTollMapUpdatedValue = aggregateTollMap.computeIfPresent(vehicle,
					(k, outerMap) -> {
						Map<Integer, Integer> innerMap = outerMap.get(localDate.toString());
						if (innerMap == null) {
							logger.info("Vehicle {} has not crossed the toll on day {}", vehicle, localDate);
							innerMap = new HashMap<>();
							innerMap.put(hour, newFee);
							outerMap.put(localDate.toString(), innerMap);
						} else {
							logger.info("Vehicle {} has crossed the toll on day {}", vehicle, localDate);
							Integer existingFee = innerMap.get(hour);
							if (existingFee == null) {
								logger.info("Vehicle {} has crossed the toll on day {} but not at hour {}", vehicle,
										localDate, hour);
								innerMap.put(hour, newFee);
								outerMap.put(localDate.toString(), innerMap);
							}
							if (existingFee != null && existingFee < newFee) {
								logger.info(
										"Vehicle {} has crossed the toll on day {} at hour {} with oldFee {} and new Fee {}",
										vehicle, localDate, hour, existingFee, newFee);
								innerMap.put(hour, newFee);
								outerMap.put(localDate.toString(), innerMap);
							}
						}
						return outerMap;
					});
			logger.info("updated value = {} ", aggregateTollMapUpdatedValue);
		}
		return buildResult(aggregateTollMap, vehicle, localDate.toString());
	}

	private TollFee buildResult(Map<String, Map<String, Map<Integer, Integer>>> aggregateTollMap, String vehicle,
			String date) {
		TollFee tollFee = new TollFee();
		tollFee.setVehicle(vehicle);
		tollFee.setDate(date);
		tollFee.setTotalAmount(0);
		aggregateTollMap.computeIfPresent(vehicle, (k, v) -> {
			Map<Integer, Integer> innerMap = v.get(date);
			if (innerMap != null) {
				int amount = innerMap.entrySet().stream().map(x -> x.getValue())
						.collect(Collectors.summingInt(Integer::intValue));
				if (amount < 60) {
					tollFee.setTotalAmount(amount);
				} else {
					tollFee.setTotalAmount(60);
				}
				tollFee.setTravelRecordsWithinDay(innerMap.entrySet().stream().collect(Collectors.toMap(x -> {
					int key = x.getKey();
					String modifiedKey = String.format(
							"vehicle = %1$s passed the toll booth on date = %2$s at hour = %3$s", vehicle, date,
							Integer.toString(key));
					return modifiedKey;
				}, y -> {
					int value = y.getValue();
					String modifiedValue = String.format("Amount charged = %1$s ", Integer.toString(value));
					return modifiedValue;
				})));
			}
			return v;
		});

		return tollFee;
	}

	private int tollCalculate(String vehicle, LocalDate localDate, int hour, int minute) {
		if (tollFreeDatesService.isTollFreeDate(localDate.toString())
				|| tollFreeVehicleService.isTollFreeVehicle(vehicle))
			return 0;
		if (hour == 6 && minute >= 0 && minute <= 29)
			return 8;
		else if (hour == 6 && minute >= 30 && minute <= 59)
			return 13;
		else if (hour == 7 && minute >= 0 && minute <= 59)
			return 18;
		else if (hour == 8 && minute >= 0 && minute <= 29)
			return 13;
		else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59)
			return 8;
		else if (hour == 15 && minute >= 0 && minute <= 29)
			return 13;
		else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59)
			return 18;
		else if (hour == 17 && minute >= 0 && minute <= 59)
			return 13;
		else if (hour == 18 && minute >= 0 && minute <= 29)
			return 8;
		else
			return 0;
	}

	@Override
	public Map<String, Map<String, Map<Integer, Integer>>> getHistoricalTollRecords() {
		// TODO Auto-generated method stub
		return aggregateTollMap;
	}

}
