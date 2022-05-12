package com.evolve_technology.calculator.properties;

import java.util.List;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.context.annotation.PropertySource;
import org.springframework.context.support.PropertySourcesPlaceholderConfigurer;
import org.springframework.stereotype.Component;

import lombok.Data;

@Data
@Component
@Configuration
public class TollConfiguration {
	
	@Value("${year}")
	private String year;
	@Value("#{'${dates}'.split(',')}")
	private List<String> dates;
	@Value("#{'${vehicles}'.split(',')}")
	private List<String> vehicles;
	@Value("#{'${months}'.split(',')}")
	private List<String> months;
	
	@Bean
	public static PropertySourcesPlaceholderConfigurer propertyConfigInDev() {
		return new PropertySourcesPlaceholderConfigurer();
	}

}
