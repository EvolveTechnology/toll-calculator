package com.arngrimur.evolve;

import java.time.DayOfWeek;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.LocalTime;
import java.util.*;

public class TollCalculator {

	private static final int NO_COST = 0;
	private static final int RUSH_HOUR_FEE = 18;
	private static final int MID_FEE = 13;
	private static final int LOW_FEE = 8;
	private static int MAX_FEE_PER_DAY=60;
	/**
	 * Calculate the total toll fee for one day. 
	 *
	 * @param vehicle
	 *            - the vehicle
	 * @param dates
	 *            - date and time of all passes on one day
	 * @return - the total toll fee for that day
	 */
	public int getTollFee(Vehicle vehicle, List<LocalDateTime> dates) {
		//We are paranoid and do not trust our own input!
		if (vehicle == null)
			return 0;
		if(dates == null || dates.size() == 0){
			return 0;
		}
		
		if (isTollFreeVehicle(vehicle)) {
			return 0;
		}
		Map<LocalDate, List<LocalTime>> timesOverDates = filterTimesOverDates(dates);
		int totalFee = calculateTotalFee(vehicle, timesOverDates);
		return totalFee;
	}

	private boolean isTollFreeVehicle(Vehicle vehicle) {
		switch (vehicle) {
		case CAR:
			return false;
		default:
			return true;
		}
	}

	private Map<LocalDate, List<LocalTime>> filterTimesOverDates(List<LocalDateTime> dates) {
		Map<LocalDate, List<LocalTime>> dateAndTimes = new HashMap<LocalDate, List<LocalTime>>();

		for (LocalDateTime date : dates) {
			if (!isTollFreeDate(date.toLocalDate())) {
				List<LocalTime> times = dateAndTimes.get(date.toLocalDate());
				if (times != null) {
					times.add(date.toLocalTime());
				} else {
					times = new ArrayList<LocalTime>();
					times.add(date.toLocalTime());
					dateAndTimes.put(date.toLocalDate(), times);
				}
			}
		}
		return dateAndTimes;
	}

	private int calculateTotalFee(Vehicle vehicle, Map<LocalDate, List<LocalTime>> timesOverDates) {
		int totalFee=0;
		Map<Integer, Integer> accumulatedFees;
		for (List<LocalTime> times : timesOverDates.values()) {
			accumulatedFees = new HashMap<Integer, Integer>();
			for (LocalTime localTime : times) {
				int fee = getTollFee(localTime, vehicle);
				Integer otherFee = accumulatedFees.get(localTime.getHour());
				if(otherFee != null ){
					fee = otherFee  > fee ? otherFee:fee;
				}
				accumulatedFees.put(localTime.getHour(), fee);
			}
			totalFee += calculateDailyFee(accumulatedFees);
		}
		return totalFee;
	}

	private int calculateDailyFee(Map<Integer, Integer> accumulatedFees) {
		int dayFee=0;
		for (Integer fee : accumulatedFees.values()) {
			dayFee+=fee;
		}
		dayFee = dayFee > MAX_FEE_PER_DAY ? MAX_FEE_PER_DAY:dayFee;
		return dayFee;
	}	

	private int getTollFee(final LocalTime date, Vehicle vehicle) {
		
		switch (date.getHour()) {
		case 6:
			if (inFirstHalfHour(date)) {
				return LOW_FEE;
			} else {
				return MID_FEE;
			}
		case 7:
			return RUSH_HOUR_FEE;
		case 8:
			if (inFirstHalfHour(date)) {
				return MID_FEE;
			} else {
				return LOW_FEE;
			}
		case 9:
		case 10:
		case 11:
		case 12:
		case 13:
		case 14:
			return LOW_FEE;
		case 15:
			if (inFirstHalfHour(date)) {
				return MID_FEE;
			} else {
				return RUSH_HOUR_FEE;
			}
		case 16:
			return RUSH_HOUR_FEE;
		case 17:
			return MID_FEE;
		case 18:
			if (inFirstHalfHour(date)) {
				return LOW_FEE;
			} else {
				return NO_COST;
			}
		default:
			return NO_COST;
		}
	}

	private boolean inFirstHalfHour(final LocalTime date) {
		return date.getMinute() < 30;
	}

	private Boolean isTollFreeDate(LocalDate date) {
		
		if(isWeekend(date)){
			return true;
		};
		
		return new HolidayChecker().isPublicHoliday(date);

	}

	private boolean isWeekend(LocalDate date) {
		return date.getDayOfWeek().equals(DayOfWeek.SATURDAY) || date.getDayOfWeek().equals(DayOfWeek.SUNDAY); 
	}
	
	
}
