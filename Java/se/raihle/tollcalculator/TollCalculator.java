package se.raihle.tollcalculator;

import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.concurrent.TimeUnit;

public class TollCalculator {

	/**
	 * Calculate the total toll fee for one day
	 *
	 * @param vehicle - the vehicle
	 * @param dates   - date and time of all passes on one day
	 * @return - the total toll fee for that day
	 */
	public int getTollFee(Vehicle vehicle, Date... dates) {
		Date intervalStart = dates[0];
		int totalFee = 0;
		for (Date date : dates) {
			int nextFee = getTollFee(date, vehicle);
			int tempFee = getTollFee(intervalStart, vehicle);

			TimeUnit timeUnit = TimeUnit.MINUTES;
			long diffInMillies = date.getTime() - intervalStart.getTime();
			long minutes = timeUnit.convert(diffInMillies, TimeUnit.MILLISECONDS);

			if (minutes <= 60) {
				if (totalFee > 0) totalFee -= tempFee;
				if (nextFee >= tempFee) tempFee = nextFee;
				totalFee += tempFee;
			} else {
				totalFee += nextFee;
			}
		}
		if (totalFee > 60) totalFee = 60;
		return totalFee;
	}

	public int getTollFee(final Date date, Vehicle vehicle) {
		Calendar calendar = GregorianCalendar.getInstance();
		calendar.setTime(date);
		return vehicle.getTollAt(calendar);
	}
}

