package com.evolve_technology.calculator.service;

import java.util.List;

public interface TollFreeDatesService {
	public Boolean isTollFreeDate(String date);
	public List<String> getTollFreeDates();
}
