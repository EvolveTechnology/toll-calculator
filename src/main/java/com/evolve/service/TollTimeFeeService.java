package com.evolve.service;

import java.time.LocalTime;

public interface TollTimeFeeService {

	/**
	 * To get Toll Fee with respect to given time period.
	 * 
	 * @param time
	 * @return Fee
	 */
	double getFee(LocalTime time);
}
