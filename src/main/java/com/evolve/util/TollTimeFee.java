package com.evolve.util;

import java.time.LocalTime;

import lombok.Data;
import lombok.experimental.Accessors;

@Data
@Accessors(chain = true)
public class TollTimeFee {

	private LocalTime startTime;
	private LocalTime endTime;
	private double fee;
}
