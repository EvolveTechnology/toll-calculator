package com.evolve_technology.calculator.properties;

import java.util.List;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.boot.context.properties.ConfigurationProperties;
import org.springframework.stereotype.Component;

import lombok.Data;

@Data
@Component
@ConfigurationProperties(prefix="tollfree")
public class TollConfiguration {
	
	@Value("${year}")
	private String year;
	private List<String> dates;
	private List<String> vehicles;
	private List<String> months;

}
