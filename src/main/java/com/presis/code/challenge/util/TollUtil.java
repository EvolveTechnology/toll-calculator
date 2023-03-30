package com.presis.code.challenge.util;

import java.time.DayOfWeek;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.YearMonth;
import java.util.EnumSet;
import java.util.Set;

import com.presis.code.challenge.toll.model.TollMaster;
import com.presis.code.challenge.toll.model.Vehicle;
import com.presis.code.challenge.toll.model.VehicleType;

public final class TollUtil {
	
	private static final Set<VehicleType> TOLL_FREE_VEHICLES = EnumSet.of(VehicleType.MOTORBIKE, VehicleType.TRACTOR, VehicleType.EMERGENCY, 
			VehicleType.DIPLOMAT, VehicleType.FOREIGN, VehicleType.MILITARY);

	public static boolean checkSameDay(LocalDateTime...dates) {
		if(dates.length == 1)  return true;
		
		LocalDate day = dates[0].toLocalDate();
        for (LocalDateTime date : dates) {
            if (!day.isEqual(date.toLocalDate())) {
            	return false;
            }
        }
        return true;
	}
	
	public static boolean isTollFreeVehicle(Vehicle vehicle) {
        return TOLL_FREE_VEHICLES.contains(vehicle.getType());
    }

	public static boolean isTollFreeDate(TollMaster configData, LocalDate date) {
		return isWeekend(date) || isWhitelistedFree(configData, date);
	}

	private static boolean isWeekend(LocalDate date) {
		return date.getDayOfWeek() == DayOfWeek.SATURDAY || date.getDayOfWeek() == DayOfWeek.SUNDAY;
	}
	
	private static boolean isWhitelistedFree(TollMaster configData, LocalDate date) {
		return configData.getWhiteListFreeDates().contains(date) || configData.getWhiteListFreeYearMonths().contains(YearMonth.from(date));
	}
}
