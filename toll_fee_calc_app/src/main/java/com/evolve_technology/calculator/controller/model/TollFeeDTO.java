package com.evolve_technology.calculator.controller.model;

import java.util.List;

import lombok.Data;

@Data
public class TollFeeDTO {
	String vehicle;
	List<TollDetailsPerDay> tollDetails;
}


