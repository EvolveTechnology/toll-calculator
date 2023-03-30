package com.presis.code.challenge.toll;

import java.time.Duration;
import java.time.LocalDateTime;
import java.util.Arrays;
import java.util.List;

import com.presis.code.challenge.toll.config.ApplicationProperties;
import com.presis.code.challenge.toll.model.TollMaster;
import com.presis.code.challenge.toll.model.Vehicle;
import com.presis.code.challenge.util.TollUtil;

public final class TollCalculator {

	static TollMaster configData;

	public TollCalculator() {
		configData = new ApplicationProperties().readMaster();
	}

	/**
	 * Calculate the total toll fee for one day
	 *
	 * @param vehicle - the vehicle
	 * @param dates   - date and time of all passes on one day
	 * @return - the total toll fee for that day
	 */
	public int calculateTotalTollFee(Vehicle vehicle, LocalDateTime... dates) {
		//  checkSameDay - date and time of all passes on one day
		if (configData != null && vehicle != null && dates != null && TollUtil.checkSameDay(dates)) {
			
			Arrays.sort(dates); // If in case not in order
			LocalDateTime intervalStart = dates[0];

			int firstTollFee = calculateOneEntry(vehicle, intervalStart);
			int total = firstTollFee;
			System.out.println("1st Travel on = " + intervalStart + ", Toll Fee = " + firstTollFee);
			List<LocalDateTime> remainingInterval = Arrays.asList(dates).subList(1, dates.length);

			for (LocalDateTime date : remainingInterval) {
				int currentTollFee = calculateOneEntry(vehicle, date);
				long minutesSincePreviousTrip = Duration.between(intervalStart, date).toMinutes();
				
				System.out.println("Travel on = " + date + ", Toll Fee = " + currentTollFee + ", From last Trip min = " + minutesSincePreviousTrip);

				if (minutesSincePreviousTrip > configData.getMaxNextTripMinute()) {
					intervalStart = date;
					firstTollFee = currentTollFee;
					total = total + currentTollFee;
				} else if (firstTollFee < currentTollFee) {
					total = total + currentTollFee - firstTollFee;
				}
				System.out.println("Total Toll Fee -> " + total);
			}
			return total > configData.getMaxFee() ? configData.getMaxFee() : total;
		}
		return 0;
	}

	private static int calculateOneEntry(Vehicle vehicle, LocalDateTime date) {
		if (TollUtil.isTollFreeDate(configData, date.toLocalDate()) || TollUtil.isTollFreeVehicle(vehicle)) {
			return 0;
		}
		return configData.fetchFeeForTimeRange(date.toLocalTime());
	}
}
