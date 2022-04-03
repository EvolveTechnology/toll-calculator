package com.evolve_technology.calculator.controller.model;

import java.util.Map;

import lombok.Data;

@Data
class TollDetailsPerDay{
	String date;
	int totalAmount;
	Map<String,String> travelRecordsHourlyCharged;
}
