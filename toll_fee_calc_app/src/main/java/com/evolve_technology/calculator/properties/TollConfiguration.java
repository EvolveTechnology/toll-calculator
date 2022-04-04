package com.evolve_technology.calculator.properties;

import java.util.List;

import org.springframework.boot.context.properties.ConfigurationProperties;
import org.springframework.stereotype.Component;

import lombok.Data;

@Data
@Component
@ConfigurationProperties(prefix="tollfree")
public class TollConfiguration {
	
	private String year;
	private List<String> dates;
	private List<String> vehicles;
	private List<String> months;

}
