package com.evolve_technology.calculator.service;

import java.time.LocalDateTime;
import java.util.List;

public interface TollFeeService {

	public Integer getTollFee(final List<LocalDateTime> list, String vehicle);
}
