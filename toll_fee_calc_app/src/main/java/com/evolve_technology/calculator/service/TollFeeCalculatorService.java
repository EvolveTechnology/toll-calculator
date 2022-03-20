package com.evolve_technology.calculator.service;

import java.util.List;

public interface TollFeeCalculatorService {

	public List<String> getTollFreeVehicles();
	public boolean isTollFreeVehicle(String vehicle);
	public int getTollFee(final String date, String vehicle);
	public Boolean isTollFreeDate(String date);
	public List<String> getTollFreeDates();
}
