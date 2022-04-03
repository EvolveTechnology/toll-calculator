package com.evolve_technology.calculator.service;

import java.util.Map;

import com.evolve_technology.calculator.controller.model.TollFee;

public interface TollFeeService {

	public TollFee getTollFee(final String date, String vehicle);
	public Map<String,Map<String,Map<Integer,Integer>>> getHistoricalTollRecords();

}
