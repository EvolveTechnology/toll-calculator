package com.evolve_technology.calculator.service.impl;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import com.evolve_technology.calculator.service.TollFeeService;
import com.evolve_technology.calculator.util.TollUtil;
import com.evolve_technology.calculator.validation.TollValidation;

public class TollFeeServiceImpl implements TollFeeService {

	private static final Logger logger = LogManager
			.getLogger(TollFeeServiceImpl.class);

	TollUtil tollUtil;

	private Map<String, Integer> tollMap = new HashMap<>();

	public TollFeeServiceImpl(TollUtil tollUtil) {
		this.tollUtil = tollUtil;
	}

	public Integer getTollFee(List<LocalDateTime> inputDates, String vehicle) {
		TollValidation.validate(inputDates, vehicle);
		logger.info(
				"Inside getTollFee method :: inputDates = {} and vehicle = {}",
				inputDates, vehicle);
		for (LocalDateTime localDateTime : inputDates) {
			int hour = localDateTime.getHour();
			int minute = localDateTime.getMinute();
			LocalDate localDate = localDateTime.toLocalDate();
			Integer newFee = tollUtil.tollCompute(vehicle, localDate, hour,
					minute);
			String key = localDate.toString() + ":" + hour;
			if (!tollMap.containsKey(key)) {
				tollMap.put(key, newFee);
			} else {
				if (newFee > tollMap.get(key))
					tollMap.put(key, newFee);
			}
		}
		return process();
	}

	public int process() {
		logger.info("tollMap {} :: " + tollMap);
		Map<LocalDate, Integer> map = new HashMap<>();
		for (String key : tollMap.keySet()) {
			LocalDate localDate = LocalDate.parse(key.split(":")[0]);
			if (!map.containsKey(localDate)) {
				map.putIfAbsent(localDate, tollMap.get(key));
			} else {
				int sum = tollMap.get(key) + map.get(localDate);
				map.put(localDate, sum > 60 ? 60 : sum);
			}
		}
		return map.values().stream()
				.collect(Collectors.summingInt(Integer::intValue));
	}
}
