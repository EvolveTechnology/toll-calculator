package com.evolve;

import java.time.LocalDateTime;
import java.time.LocalTime;
import java.util.Arrays;
import java.util.List;
import java.util.stream.Collectors;

import com.evolve.model.Vehicle;
import com.evolve.service.FreeDayService;
import com.evolve.service.TollTimeFeeService;

public class TollCalculator {

	public static final int MAX_ALLOWED_ONE_DAY_FEE = 60;

	public static final String NULL_VEHICLE_MESSAGE = "Vehicle can not be null";

	public static final String NULL_DATES_MESSAGE = "dates can not be null or empty";

	public static final String DIFFERENT_DATES_MESSAGE = "date should be passed for same day";
	
	private static final int TOLL_FEE_DEDUCTION_TIME_DIFFERENCE_MINUTES = 60;

	private FreeDayService freeDayService;

	private TollTimeFeeService tollTimeFeeService;

	public TollCalculator() {
	}

	public TollCalculator(FreeDayService freeDayService, TollTimeFeeService tollTimeFeeService) {
		this.freeDayService = freeDayService;
		this.tollTimeFeeService = tollTimeFeeService;
	}

	/**
	 * - Apply null check for both Vehicle and Date 
	 * - IF toll free vehicle then return. no further check/validation
	 * 
	 * - All dates should be of same day.
	 * - Check IF date belongs to Free Day.
	 * 
	 * @param vehicle
	 * @param dates
	 * @return
	 */
	private boolean isValid(Vehicle vehicle, LocalDateTime... dates) {
		if (null == vehicle) {
			throw new RuntimeException(NULL_VEHICLE_MESSAGE);
		}
		if (isTollFreeVehicle(vehicle)) {
			return false;
		}

		if (null == dates || dates.length == 0) {
			throw new RuntimeException(NULL_DATES_MESSAGE);
		}
		if (Arrays.stream(dates)
				.map(LocalDateTime::toLocalDate)
				.anyMatch(date -> !date.equals(dates[0].toLocalDate()))) {
			throw new RuntimeException(DIFFERENT_DATES_MESSAGE);
		}
		if (freeDayService.isFreeDay(dates[0].toLocalDate())) {
			return false;
		}
		return true;
	}

	/**
	 * 
	 * Calculate the total toll fee for one day
	 *
	 * @param vehicle - the vehicle
	 * @param dates   - date and time of all passes on one day
	 * @return - the total toll fee for that day
	 * 
	 *         - //dates should match today's date (need to discuss[toll can not be
	 *         deducted for previous OR future date]) - drop toll free entries.
	 * 
	 */
	public double getTollFee(Vehicle vehicle, LocalDateTime... dates) {

		if (!isValid(vehicle, dates)) {
			return 0;
		}
		// filtered and sorted time
		List<LocalTime> validTimeList = Arrays.stream(dates)
				.map(LocalDateTime::toLocalTime)
				.filter(time -> tollTimeFeeService.getFee(time) > 0)
				.sorted()
				.collect(Collectors.toList());

		if (validTimeList.isEmpty()) {
			return 0;
		}

		LocalTime lastHourTime = validTimeList.get(0);
		double tempFee = tollTimeFeeService.getFee(lastHourTime);
		double totalFee = 0;
		for (LocalTime nextTime : validTimeList) {
			double nextFee = tollTimeFeeService.getFee(nextTime);

			if (lastHourTime.plusMinutes(TOLL_FEE_DEDUCTION_TIME_DIFFERENCE_MINUTES).isAfter(nextTime)) {
				if (totalFee > 0) {
					totalFee -= tempFee;
				}
				if (nextFee >= tempFee) {
					tempFee = nextFee;
				}
				totalFee += tempFee;
			} else {
				totalFee += nextFee;
				lastHourTime = nextTime;
				tempFee = nextFee;

				/**
				 * extra check to stop iteration if totalFee equal or above
				 * MAX_ALLOWED_ONE_DAY_FEE
				 */
				if (totalFee >= MAX_ALLOWED_ONE_DAY_FEE) {
					return MAX_ALLOWED_ONE_DAY_FEE;
				}
			}
		}
		return Math.min(totalFee, MAX_ALLOWED_ONE_DAY_FEE);
	}

	private boolean isTollFreeVehicle(Vehicle vehicle) {
		if (vehicle == null)
			return false;
		String vehicleType = vehicle.getType();
		return vehicleType.equals(TollFreeVehicles.MOTORBIKE.getType())
				|| vehicleType.equals(TollFreeVehicles.TRACTOR.getType())
				|| vehicleType.equals(TollFreeVehicles.EMERGENCY.getType())
				|| vehicleType.equals(TollFreeVehicles.DIPLOMAT.getType())
				|| vehicleType.equals(TollFreeVehicles.FOREIGN.getType())
				|| vehicleType.equals(TollFreeVehicles.MILITARY.getType());
	}

	private enum TollFreeVehicles {
		MOTORBIKE("Motorbike"), 
		TRACTOR("Tractor"), 
		EMERGENCY("Emergency"), 
		DIPLOMAT("Diplomat"), 
		FOREIGN("Foreign"),
		MILITARY("Military");

		private final String type;

		TollFreeVehicles(String type) {
			this.type = type;
		}

		public String getType() {
			return type;
		}
	}
}
