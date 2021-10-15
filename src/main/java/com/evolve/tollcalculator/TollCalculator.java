package com.evolve.tollcalculator;

import java.time.LocalTime;
import java.time.LocalDate;
import java.time.ZoneId;
import java.time.DayOfWeek;
import java.util.*;

import java.util.stream.Collectors;

import com.evolve.services.HolidayService;
import com.evolve.services.TollFeeService;
import com.evolve.vehicles.Vehicle;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

public class TollCalculator {
    private HolidayService holidayService;
    private TollFeeService tollFeeService;
    private static final Logger logger = LogManager.getLogger(TollCalculator.class);
    public static final int MAX_FEE_IN_ONE_DAY = 60;

    public TollCalculator(HolidayService holidayService, TollFeeService tollFeeService) {
        this.holidayService = holidayService;
        this.tollFeeService = tollFeeService;
    }

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */
    public int getTollFee(Vehicle vehicle, Date... dates) {
        if (vehicle == null)
            throw new IllegalArgumentException("Vehicle can't be null.");
        if (dates == null || dates.length == 0)
            return 0;
        if (Arrays.stream(dates).anyMatch(Objects::isNull)) {
            logger.error("The input dates contain null: " + Arrays.toString(dates));
            throw new IllegalArgumentException("The input dates has null entry.");
        }

        // check if all dates are of the same day
        List<LocalDate> temp = Arrays.stream(dates).map(d -> d.toInstant().atZone(ZoneId.systemDefault()).toLocalDate()).collect(Collectors.toList());
        LocalDate entry1 = temp.get(0);
        if (temp.stream().anyMatch(d -> !d.equals(entry1))) {
            logger.error("The input dates are not of the same date: " + Arrays.toString(dates));
            throw new IllegalArgumentException("All dates shall be on the same date.");
        }
        if (isTollFreeVehicle(vehicle) || isTollFreeDate(entry1))
            return 0;

        /*
         * Drop the toll-free entries.
         * Consider the following case:
         * Tim      fee     comment
         * 05:10    0       start of 1st interval if toll-free entries are not dropped
         * 06:05    8       start of 1st interval if toll-free entries are dropped
         * 06:20    8       start of 2nd interval if toll-free entries are not dropped
         *
         * If the toll-free entries are not dropped, the total fees are 8 + 8 = 16.
         * Otherwise it is 8.
         * Thus we should drop toll-free entries so that free is free.
         */
        List<LocalTime> localTimes = Arrays.stream(dates).map(d -> d.toInstant().atZone(ZoneId.systemDefault())
                .toLocalTime()).filter(d -> getTollFee(d) > 0).collect(Collectors.toList());
        if (localTimes.isEmpty())
            return 0;

        // end of one-hour interval
        LocalTime intervalEnd = localTimes.get(0).plusHours(1);
        int totalFee = 0;
        int maxFee = 0;
        for (LocalTime time : localTimes) {
            int tollFee = getTollFee(time);
            if (time.isAfter(intervalEnd)) {
                // passed one-hour period, add highest toll in this hour to total fee.
                totalFee += maxFee;
                intervalEnd = time.plusHours(1);
                maxFee = tollFee;
            } else {
                if (maxFee < tollFee)
                    maxFee = tollFee;
            }
        }
        // add the last entry to total. Note: this entry is not counted within the loop.
        totalFee += maxFee;

        return (Math.min(totalFee, MAX_FEE_IN_ONE_DAY));
    }

    /**
     * Get the toll fee at a specified time.
     *
     * @param time  the specified time
     * @return  the toll fee should be charged at the specified time
     */
    private int getTollFee(LocalTime time) {
        // discard the seconds part
        LocalTime t = LocalTime.of(time.getHour(), time.getMinute());
        return tollFeeService.getTollFee(t);
    }

    /**
     * Check if the given date is a toll-free date.
     * @param date  the specified date
     * @return  true if it is toll-free, false otherwise
     */
    private Boolean isTollFreeDate(LocalDate date) {
        DayOfWeek dayOfWeek = date.getDayOfWeek();
        if (dayOfWeek == DayOfWeek.SATURDAY || dayOfWeek == DayOfWeek.SUNDAY)
            return true;
        return holidayService.isHoliday(date);
    }

    /**
     * Check if a vehicle is toll-free.
     * @param vehicle   the vehicle
     * @return  true if the vehicle is toll-free, false otherwise
     */
    private boolean isTollFreeVehicle(Vehicle vehicle) {
        return vehicle.isTollFree();
    }
}
