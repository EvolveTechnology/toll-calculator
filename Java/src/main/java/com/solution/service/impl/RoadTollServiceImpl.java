package com.solution.service.impl;

import cn.hutool.core.collection.CollUtil;
import cn.hutool.core.util.ObjectUtil;
import com.solution.TollCalculator;
import com.solution.pojo.Vehicle;
import com.solution.service.RoadTollService;
import com.solution.util.TimeIntervalFee;
import java.time.DayOfWeek;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.LocalTime;
import java.util.Arrays;
import java.util.Optional;

import static com.solution.util.Constants.*;
import static com.solution.util.InitData.holidayMap;
import static com.solution.util.InitData.timeFeeList;

public class RoadTollServiceImpl implements RoadTollService {


    /**
     * Determine whether the given date is a holiday
     * @param day
     * @return
     */
    @Override
    public boolean isFreeDay(LocalDate day) {
        if (day.getDayOfWeek() == DayOfWeek.SATURDAY || day.getDayOfWeek() == DayOfWeek.SUNDAY) {
            return true;
        } else {
            return holidayMap.containsKey(day);
        }
    }

    /**
     * The calculation results of tolls at a given time point are obtained
     * @param time
     * @return
     */
    @Override
    public double getFee(LocalTime time) {
        return timeFeeList.stream()
                .filter(timeFee -> isMatched(timeFee, time))
                .findAny()
                .map(TimeIntervalFee::getFee)
                .orElse(0d);
    }

    /**
     * Determine whether the given time is between StartTime and EndTime of timeFee
     */
    private boolean isMatched(TimeIntervalFee timeFee, LocalTime time) {
        return withinStartTime(timeFee, time) && withinEndTime(timeFee, time);
    }

    private boolean withinStartTime(TimeIntervalFee timeFee, LocalTime time) {
        return (timeFee.getStartTime().equals(time) || timeFee.getStartTime().isBefore(time));
    }

    private boolean withinEndTime(TimeIntervalFee timeFee, LocalTime time) {
        return (timeFee.getEndTime().equals(time) || timeFee.getEndTime().isAfter(time));
    }

    /**
     * - 1.Non-null check is performed on vehicle and time set parameters respectively.
     * - 2.Check whether the vehicle is free
     * - 3.All dates should be of same day.
     * - 4.Check IF date belongs to Free Day.
     *
     * @param vehicle
     * @param dates
     * @return
     */
    @Override
    public boolean isValid(Vehicle vehicle, LocalDateTime... dates) {

        Optional.ofNullable(vehicle).orElseThrow(()->new RuntimeException(NULL_PARAM_VEHICLE_MSG));

        if (isTollFreeVehicle(vehicle)) {
            return false;
        }

        if (ObjectUtil.isNull(dates) || CollUtil.isEmpty(Arrays.asList(dates))) {
            throw new RuntimeException(NULL_PARAM_DATES_MSG);
        }
        if (Arrays.stream(dates)
                .map(LocalDateTime::toLocalDate)
                .anyMatch(date -> !date.equals(dates[0].toLocalDate()))) {
            throw new RuntimeException(MORE_THAN_ONE_DAY_MSG);
        }
        if (isFreeDay(dates[0].toLocalDate())) {
            return false;
        }
        return true;
    }

    private boolean isTollFreeVehicle(Vehicle vehicle) {
        if (ObjectUtil.isNull(vehicle))
            return false;
        String vehicleType = vehicle.getType();
        return vehicleType.equals(TollFreeVehicles.MOTORBIKE.getType())
                || vehicleType.equals(TollFreeVehicles.TRACTOR.getType())
                || vehicleType.equals(TollFreeVehicles.EMERGENCY.getType())
                || vehicleType.equals(TollFreeVehicles.DIPLOMAT.getType())
                || vehicleType.equals(TollFreeVehicles.FOREIGN.getType())
                || vehicleType.equals(TollFreeVehicles.MILITARY.getType());
    }
}