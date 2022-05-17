package com.evolve_technology.calculator.properties;

import java.io.IOException;
import java.io.InputStream;
import java.util.Arrays;
import java.util.List;
import java.util.Properties;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import lombok.Data;

@Data
public class TollConfiguration {
	private static final Logger logger = LogManager
			.getLogger(TollConfiguration.class);
	private String year;
	private List<String> dates;
	private List<String> vehicles;
	private List<String> months;

	public TollConfiguration() {
		try (InputStream input = TollConfiguration.class.getClassLoader()
				.getResourceAsStream("application.properties")) {

			Properties prop = new Properties();

			if (input == null) {
				logger.info("Sorry, unable to find application.properties");
				return;
			}

			// load a properties file from class path, inside static method
			prop.load(input);

			// get the property value and print it out
			if (prop.getProperty("dates") != null) {
				setDates(Arrays.asList(prop.getProperty("dates").split(",")));
			}

			if (prop.getProperty("months") != null) {
				setMonths(Arrays.asList(prop.getProperty("months").split(",")));
			}

			if (prop.getProperty("vehicles") != null) {
				setVehicles(
						Arrays.asList(prop.getProperty("vehicles").split(",")));
			}

			setYear(prop.getProperty("year"));

			logger.info("dates  :: {} " + getDates());
			logger.info("months  :: {} " + getMonths());
			logger.info("vehicles  :: {} " + getVehicles());
			logger.info("year  :: {} " + getYear());

		} catch (IOException ex) {
			ex.printStackTrace();
			logger.error(ex.getMessage());
		}
	}
}
