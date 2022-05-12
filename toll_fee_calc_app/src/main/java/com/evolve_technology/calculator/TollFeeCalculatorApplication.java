package com.evolve_technology.calculator;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.context.annotation.Configuration;

import com.evolve_technology.calculator.properties.TollConfiguration;


public class TollFeeCalculatorApplication {
	@Autowired
	TollConfiguration tollConfiguration;
	
	public static void main(String[] args) {
		// TODO Auto-generated method stub
	}

}
