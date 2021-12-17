package com.solution.util;

/**
 * @description: Used to store constants.
 * @author: Richard(Duo.Wang)
 * @createDate: 2021/12/16 - 12:08
 * @version: v1.0
 */
public class Constants {
//vehicle
    public static final String CAR = "Car";
    public static final String MOTORBIKE = "Motorbike";
    public static final String DIPLOMAT="Diplomat";
    public static final String EMERGENCY="Emergency";
    public static final String MILITARY="Military";
    public static final String TRACTOR="Tractor";
    public static final String FOREIGN="Foreign";
//numbers
    public static final int MAX_FEE_FOR_ONE_DAY = 60;//SEK
    public static final int LIMIT_MINUTES_RECHARGE = 60;//MINUTES
//message
    public static final String NULL_PARAM_VEHICLE_MSG = "Vehicle parameter can not be null.";
    public static final String NULL_PARAM_DATES_MSG = "Dates parameter can not be null or empty.";
    public static final String MORE_THAN_ONE_DAY_MSG = "The time range cannot exceed one day.";
//path
    public static final String FEE_BY_TIME_LIST_YAML_FILE = "/feeByTimeList.yaml";

//free vehicles
    public enum TollFreeVehicles {
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
