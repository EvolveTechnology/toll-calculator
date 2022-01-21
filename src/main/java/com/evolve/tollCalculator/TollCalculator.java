package com.evolve.tollCalculator;

import com.evolve.tollCalculator.util.TollFreeVehicles;
import com.evolve.tollCalculator.model.Vehicle;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;


import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.*;
import java.util.concurrent.TimeUnit;
import java.util.stream.Stream;


import static com.evolve.tollCalculator.util.Constants.*;
import static java.util.Map.entry;

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
        if (dates.length == 1) return getTollFee(dates[0], vehicle);

        Arrays.sort(dates);

        Map<Date, Integer> feeHashMap = new HashMap<>();
        Date intervalStart = dates[0];
        int tempFee = getTollFee(intervalStart, vehicle);
        feeHashMap.put(intervalStart, tempFee);

        for (Date date : dates) {
            int nextFee = getTollFee(date, vehicle);

            TimeUnit timeUnit = TimeUnit.MINUTES;
            long diffInMillies = date.getTime() - intervalStart.getTime();
            long minutes = timeUnit.convert(diffInMillies, TimeUnit.MILLISECONDS);

            if (minutes <= 60) {
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
        return Stream.of(TollFreeVehicles.values()).anyMatch(v -> v.name().equals(vehicle.getType().toUpperCase()));
    }

    public int getTollFee(final Date date, Vehicle vehicle) throws ParseException {
        if (isTollFreeDate(date)) return 0;
        return getFeeByDate(date);
    }

    private int getFeeByDate(Date tollDate) throws ParseException {

        for (Map.Entry<String, String> entry : s_time_fee_Map.entrySet()) {
            if (isTimeRange(entry.getKey(), tollDate)) {
                logger.info("Date time {} is under range {}", tollDate, entry.getKey());
                return Integer.valueOf(entry.getValue());
            }
        }
        return 0;
    }

    private boolean isTimeRange(String dateRange, Date tollDate) throws ParseException {

        SimpleDateFormat dateFormat = new SimpleDateFormat("HH:mm");
        Date tollTime = dateFormat.parse(dateFormat.format(tollDate));

        int index = dateRange.indexOf(DATE_DELIMITER);

        Date beginRangeTime = dateFormat.parse(dateRange.substring(0, index));
        Date endRangeTime = dateFormat.parse(dateRange.substring(index + 1));

        if ((tollTime.after(beginRangeTime) & tollTime.before(endRangeTime))
                || tollTime.equals(beginRangeTime)
                || tollTime.equals(endRangeTime)) {
            return true;
        }

        return false;
    }

    private Boolean isTollFreeDate(Date date) {
        Calendar calendar = getCalendarByDate(date);
        int month = getDateAttribute(calendar, Calendar.MONTH);
        int day = getDateAttribute(calendar, Calendar.DAY_OF_MONTH);

        int dayOfWeek = calendar.get(Calendar.DAY_OF_WEEK);
        if (dayOfWeek == Calendar.SATURDAY || dayOfWeek == Calendar.SUNDAY) return true;

        if (s_holiday_list.contains(month) || s_holiday_list.contains(month + DATE_DELIMITER + day)) {
            logger.info("{} is holiday", date);
            return true;
        }

        return false;
    }

    private Calendar getCalendarByDate(Date date) {
        Calendar calendar = GregorianCalendar.getInstance();
        calendar.setTime(date);
        return calendar;
    }


    private int getDateAttribute(Calendar calendar, int type) {
        return calendar.get(type);
    }


    private static Map<String, String> s_time_fee_Map = Map.ofEntries(
            entry("00:00-05:59", "0"),
            entry("06:00-06:29", "8"),
            entry("06:30-06:59", "13"),
            entry("07:00-07:59", "18"),
            entry("08:00-08:29", "13"),
            entry("08:30-14:59", "8"),
            entry("15:00-15:29", "13"),
            entry("15:30-16:59", "18"),
            entry("17:00-17:59", "13"),
            entry("18:00-18:29", "8"),
            entry("18:30-24:00", "0")
    );


    private static List<String> s_holiday_list =
            List.of(Calendar.JANUARY + "-1",
                    Calendar.MARCH + "-28",
                    Calendar.MARCH + "-29",
                    Calendar.APRIL + "-1",
                    Calendar.APRIL + "-30",
                    Calendar.MAY + "-1",
                    Calendar.MAY + "-8",
                    Calendar.MAY + "-9",
                    Calendar.JUNE + "-5",
                    Calendar.JUNE + "-6",
                    Calendar.JUNE + "-21",
                    Calendar.JULY + "",
                    Calendar.NOVEMBER + "-1",
                    Calendar.DECEMBER + "-24",
                    Calendar.DECEMBER + "-25",
                    Calendar.DECEMBER + "-26",
                    Calendar.DECEMBER + "-31");
}

