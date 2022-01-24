package com.evolve.tollCalculator;

import com.evolve.tollCalculator.model.Vehicle;
import com.evolve.tollCalculator.util.DateUtils;
import com.evolve.tollCalculator.util.TollFreeVehicles;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.text.ParseException;
import java.util.*;
import java.util.stream.Stream;

public class TollCalculator {

    private static final Logger logger = LogManager.getLogger(TollCalculator.class);

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */
    public int getTotalTollFeePerDay(Vehicle vehicle, Date... dates) throws ParseException {
        int totalFee = 0;
        if (vehicle == null || dates == null || dates.length == 0) return totalFee;
        if (isTollFreeVehicle(vehicle)) return totalFee;

        Arrays.sort(dates);
        Map<Date, Integer> feeHashMap = new HashMap<>();
        Date intervalStart = dates[0];
        DateUtils.initHolidayList(
                DateUtils.getDateAttribute(
                        DateUtils.getCalendarByDate(intervalStart), Calendar.YEAR));

        if(DateUtils.isWeekendOrHoliday(intervalStart)) return totalFee;

        int tempFee = getFeeByDate(intervalStart);
        feeHashMap.put(intervalStart, tempFee);

        for (Date date : dates) {
            int nextFee = getFeeByDate(date);

            if (DateUtils.isInOneHourPeriod(intervalStart, date)) {
                if (feeHashMap.get(intervalStart) <= nextFee) {
                    feeHashMap.put(intervalStart, nextFee);
                }
            } else {
                intervalStart = date;
                feeHashMap.put(intervalStart, nextFee);
            }
        }

        totalFee = feeHashMap.values().stream().reduce(0, Integer::sum);
        if (totalFee > 60) totalFee = 60;

        logger.info("Total fee is {}", totalFee);
        return totalFee;
    }


    private boolean isTollFreeVehicle(Vehicle vehicle) {
        if (vehicle == null) return false;
        return Stream.of(TollFreeVehicles.values()).
                anyMatch(v -> v.name().equals(vehicle.getType().toUpperCase()));
    }


    private int getFeeByDate(Date tollDate) throws ParseException {

        for (Map.Entry<String, String> entry : DateUtils.s_time_fee_Map.entrySet()) {
            if (DateUtils.isTimeRange(entry.getKey(), tollDate)) {
                logger.info("Date time {} is under range {}", tollDate, entry.getKey());
                return Integer.valueOf(entry.getValue());
            }
        }
        return 0;
    }


}

