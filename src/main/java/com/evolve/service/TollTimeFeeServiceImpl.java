package com.evolve.service;

import java.io.IOException;
import java.io.InputStream;
import java.time.LocalTime;
import java.util.ArrayList;
import java.util.List;

import org.yaml.snakeyaml.Yaml;
import org.yaml.snakeyaml.constructor.Constructor;

import com.evolve.util.TollTimeFee;
import com.evolve.util.TollTimeFeeList;

public class TollTimeFeeServiceImpl implements TollTimeFeeService {
	
	private static final String TOLL_TIME_FEE_YAML_FILE = "/tollFeeTime.yaml";
	private static final List<TollTimeFee> tollTimeFeeList = new ArrayList<>();

	static {
		readTollTimeFeeYaml();
	}

	/**
	 * {@inheritDoc}
	 */
	@Override
	public double getFee(LocalTime time) {
		return tollTimeFeeList.stream()
				.filter(timeFee -> isMatched(timeFee, time))
				.findAny()
				.map(TollTimeFee::getFee)
				.orElse(0d);
	}
	
	/**
	 * StartTime <= time && endTime>= time
	 * e.g. time = 6:30:59 StartTime = 6:00:00 EndTime = 6:30:59
	 * 6:00:00 <= 6:30:59 && 6:30:59 >= 6:30:59
	 */
	private boolean isMatched(TollTimeFee timeFee, LocalTime time) {
		return withinStartTime(timeFee, time) && withinEndTime(timeFee, time); 
	}
	
	private boolean withinStartTime(TollTimeFee timeFee, LocalTime time) {
		return (timeFee.getStartTime().equals(time) || timeFee.getStartTime().isBefore(time));
	}
	
	private boolean withinEndTime(TollTimeFee timeFee, LocalTime time) {
		return (timeFee.getEndTime().equals(time) || timeFee.getEndTime().isAfter(time));
	}

	/**
	 * To read YAML file and populate TollTimeFee list.
	 * 
	 */
	private static void readTollTimeFeeYaml() {

		try (InputStream in = TollTimeFeeServiceImpl.class.getResourceAsStream(TOLL_TIME_FEE_YAML_FILE)) {
			Yaml yaml = new Yaml(new Constructor(TollTimeFeeList.class));
			TollTimeFeeList list = yaml.load(in);

			list.getTimeFeeList()
			.stream()
			.map(timeFeeString -> mapToTollTimeFee(timeFeeString))
			.forEach(tollTimeFeeList::add);
		} catch (IOException e) {
			e.printStackTrace();
			throw new RuntimeException(e.getMessage());
		}
	}
	
	private static TollTimeFee mapToTollTimeFee(TollTimeFeeList.TollTimeFeeString timeFeeString) {
		return new TollTimeFee()
				.setStartTime(LocalTime.parse(timeFeeString.getStart()))
				.setEndTime(LocalTime.parse(timeFeeString.getEnd()))
				.setFee(timeFeeString.getFee());
	}

}
