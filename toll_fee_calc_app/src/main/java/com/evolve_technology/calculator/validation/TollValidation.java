package com.evolve_technology.calculator.validation;

import java.time.LocalDateTime;
import java.util.List;

import com.evolve_technology.calculator.exception.CustomErrorException;

public class TollValidation {
	public static void validate(List<LocalDateTime> inputDates,
			String vehicle) {
		if (inputDates == null || vehicle == null || inputDates.isEmpty()
				|| vehicle.isBlank())
			throw new CustomErrorException(
					"inputDates and vehicle must not be null or empty. ");
	}

}
