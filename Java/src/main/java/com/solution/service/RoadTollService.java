package com.solution.service;

import com.solution.pojo.Vehicle;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.LocalTime;


/**
 * @description: methods relating to calculation of road tolls.
 * @author: Richard(Duo.Wang)
 * @createDate: 2021/12/16 - 14:10
 * @version: v1.0
 */

public interface RoadTollService {
    boolean isFreeDay(LocalDate day);
    double getFee(LocalTime time);
    boolean isValid(Vehicle vehicle, LocalDateTime... dates);
}
