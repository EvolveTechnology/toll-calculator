package org.example;

import org.example.config.TimeslotFees;
import org.example.config.TollFeeConfiguration;
import org.example.data.TollFreeVehicles;
import org.example.data.Vehicle;
import org.example.exception.ParameterNotFoundException;

import java.time.DayOfWeek;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.LocalTime;
import java.time.MonthDay;
import java.time.temporal.ChronoField;
import java.time.temporal.ChronoUnit;
import java.util.Arrays;
import java.util.Date;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import java.util.stream.Collectors;

import lombok.extern.slf4j.Slf4j;


@Slf4j
public class TollCalculator {

    private final TollFeeConfiguration tollFeeConfiguration;


    public TollCalculator(TollFeeConfiguration tollFeeConfiguration) {
        this.tollFeeConfiguration = tollFeeConfiguration;
    }

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle    - the vehicle
     * @param datesInput - date and time of all passes on one day
     * @return - the total toll fee for that day
     */
    public int getTollFee(Vehicle vehicle, Date... datesInput) {
        if (vehicle == null) {
            throw new ParameterNotFoundException("vehicle parameter is null.");
        }

        if (datesInput == null) {
            throw new ParameterNotFoundException("dates parameter is null.");
        }

        if (isTollFreeVehicle(vehicle)) {
            log.info("Vehicle[{}] is free from toll tax, so returning 0 fee.", vehicle.getType());
            return 0;
        }

        List<Date> dates = Arrays.stream(datesInput).sorted().collect(Collectors.toList());

        Map<LocalDate, List<LocalDateTime>> collect = dates.stream()
                .map(DateUtil::toLocalDateTime)
                .collect(Collectors.groupingBy(LocalDateTime::toLocalDate));

        int totalFees = 0;
        for (Map.Entry<LocalDate, List<LocalDateTime>> entry : collect.entrySet()) {
            totalFees = totalFees + getTollFeesPerDay(vehicle, entry.getValue());
        }
        log.info("Final toll fees:{}", totalFees);
        return totalFees;

    }

    public int getTollFeesPerDay(Vehicle vehicle, List<LocalDateTime> localDateTimes) {

        LocalDateTime intervalStarLocal = localDateTimes.get(0);
        Date intervalStart = DateUtil.toDate(intervalStarLocal);
        int totalFee = 0;

        if (isTollFreeDate(intervalStarLocal)) return 0;

        int tempFee = getTollFee(intervalStart, vehicle);
        LocalDateTime tempDateTime = intervalStarLocal;
        for (LocalDateTime localDateTime : localDateTimes) {
            int nextFee = getTollFee(DateUtil.toDate(localDateTime), vehicle);
            long minutes = tempDateTime.until(localDateTime, ChronoUnit.MINUTES);
            if (minutes <= 60) {
                if (totalFee > 0) {
                    totalFee = totalFee - tempFee;
                }
                if (nextFee >= tempFee) {
                    tempFee = nextFee;
                }
                totalFee = totalFee + tempFee;
            } else {
                tempDateTime = localDateTime;
                totalFee = totalFee + nextFee;
            }
        }

        int maximumTollFees = tollFeeConfiguration.getMaximumTollFeesPerDay();
        if (totalFee > maximumTollFees) {
            log.info("Calculated Total Fee:{} is more than the maximum toll fees, hence returning the maximum toll fees:{}", totalFee, maximumTollFees);
            totalFee = maximumTollFees;
        }

        log.info("Total toll fees:{} for Date:{}", totalFee, intervalStarLocal.toLocalDate());

        return totalFee;
    }

    private boolean isTollFreeVehicle(Vehicle vehicle) {
        if (vehicle == null) return false;
        String vehicleType = vehicle.getType();
        boolean tollFreeVehicle = Arrays.stream(TollFreeVehicles.values()).anyMatch(tollFreeVehicles -> tollFreeVehicles.getType().equals(vehicleType));
        if (tollFreeVehicle) {
            log.info("Vehicle[{}] is free from toll tax.", vehicle.getType());
        }
        return tollFreeVehicle;
    }

    public int getTollFee(final Date date, Vehicle vehicle) {
        LocalDateTime localDateTime = DateUtil.toLocalDateTime(date);
        if (isTollFreeDate(localDateTime) || isTollFreeVehicle(vehicle)) return 0;

        LocalTime localTime = localDateTime.toLocalTime();

        Optional<TimeslotFees> any = tollFeeConfiguration.getTimeslotFees().stream().filter(
                e -> localTime.isBefore(e.getEndTime()) && localTime.isAfter(e.getStartTime())
        ).findAny();

        int tollFees = any.map(TimeslotFees::getFees).orElse(0);
        log.debug("Returning toll fees:{} for date:{}", tollFees, date);
        return tollFees;
    }

    private boolean isTollFreeDate(LocalDateTime localDateTime) {
        LocalDate localDate = localDateTime.toLocalDate();
        return isWeekend(localDate) || isHoliday(localDate);
    }

    private boolean isHoliday(final LocalDate localDate) {
        boolean holidayFlag = tollFeeConfiguration.getHolidays().contains(MonthDay.from(localDate));
        if (holidayFlag) {
            log.info("Date[{}] It is on Holiday", localDate);
        }
        return holidayFlag;
    }

    private boolean isWeekend(final LocalDate ld) {
        DayOfWeek day = DayOfWeek.of(ld.get(ChronoField.DAY_OF_WEEK));
        boolean isDateOnWeekend = day == DayOfWeek.SUNDAY || day == DayOfWeek.SATURDAY;
        if (isDateOnWeekend) {
            log.info("Date[{}] It is on Weekend", ld);
        }
        return isDateOnWeekend;
    }


}

