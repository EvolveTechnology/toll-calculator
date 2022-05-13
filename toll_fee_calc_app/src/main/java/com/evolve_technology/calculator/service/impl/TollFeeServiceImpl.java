package com.evolve_technology.calculator.service.impl;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import com.evolve_technology.calculator.exception.CustomErrorException;
import com.evolve_technology.calculator.service.TollFeeService;
import com.evolve_technology.calculator.util.TollUtil;

public class TollFeeServiceImpl implements TollFeeService {

	private static final Logger logger = LogManager.getLogger(TollFeeServiceImpl.class);

	TollUtil tollUtil;

	private  Map<LocalDate,Map<Integer,Integer>> tollMap=new HashMap<>();
	
	public TollFeeServiceImpl() {
		this.tollUtil=new TollUtil();
	}
	
	public Integer getTollFee(List<LocalDateTime> inputDates,String vehicle) {
		if(inputDates==null || vehicle==null || inputDates.isEmpty() || vehicle.isBlank())
			throw new CustomErrorException( "inputDates and vehicle must not be null or empty. ");
		logger.info("Inside getTollFee method :: inputDates = {} and vehicle = {}", inputDates, vehicle);
		for(LocalDateTime localDateTime : inputDates) {
			int hour = localDateTime.getHour();
			int minute = localDateTime.getMinute();
			LocalDate localDate = localDateTime.toLocalDate();
			Integer newFee = tollUtil.tollCompute(vehicle, localDate, hour, minute);
			if(!tollMap.containsKey(localDate)) {
				Map<Integer,Integer> innerMap=tollMap.computeIfAbsent(localDate, k->{
					logger.info("Vehicle {} has crossed the toll first time on date {} ", vehicle ,localDate);
					Map<Integer, Integer> map = new HashMap<>();
					map.put(hour, newFee);
					return map;
				});
				logger.info("key = {} , value = {} ", localDate, innerMap);
			}else {
				Map<Integer, Integer> innerMapExisting = tollMap.computeIfPresent(localDate,
						(k, outerMap) -> {
								logger.info("Vehicle {} has crossed the toll on day {}", vehicle, localDate);
								Integer existingFee = outerMap.get(hour);
								if (existingFee == null) {
									logger.info("Vehicle {} has crossed the toll on day {} but not at hour {}", vehicle,
											localDate, hour);
									outerMap.put(hour, newFee);
								}
								if (existingFee != null && existingFee < newFee) {
									logger.info(
											"Vehicle {} has crossed the toll on day {} at hour {} with oldFee {} and new Fee {}",
											vehicle, localDate, hour, existingFee, newFee);
									outerMap.put(hour, newFee);
								}
							
							return outerMap;
						});
				logger.info("updated value = {} ", innerMapExisting);
			}
		}
		return process();
	}
	
	public int process() {
		logger.info("tollMap {} :: "+tollMap);
		return tollMap.entrySet().stream().collect(Collectors.toMap(e->e.getKey(),
				e->{
					int sum=e.getValue().values().stream().collect(Collectors.summingInt(Integer::intValue));
					return sum>60 ? 60 : sum;
				})).values().stream().collect(Collectors.summingInt(Integer::intValue));
	}
}
