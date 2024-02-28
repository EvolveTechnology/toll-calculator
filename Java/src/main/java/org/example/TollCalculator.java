package org.example;

import org.example.config.TimeslotFees;
import org.example.config.TollConfiguration;
import org.example.data.Vehicle;
import org.example.exception.APIBadRequestException;
import org.example.exception.ParameterNotFoundException;

import java.text.MessageFormat;
import java.time.DayOfWeek;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.LocalTime;
import java.time.MonthDay;
import java.time.temporal.ChronoField;
import java.time.temporal.ChronoUnit;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import java.util.stream.Collectors;

import lombok.extern.slf4j.Slf4j;
import org.example.util.VehicleValidation;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;


@Slf4j
@Component
public class TollCalculator {

    @Autowired
    private final TollConfiguration tollConfiguration;

    public TollCalculator(TollConfiguration tollConfiguration) {
        this.tollConfiguration = tollConfiguration;
        System.out.println(tollConfiguration);
    }

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle    - the vehicle
     * @param datesInput - date and time of all passes on one day
     * @return - the total toll fee for that day
     */
    public int getTollFee(Vehicle vehicle, List<LocalDateTime> datesInput) {

        if (vehicle == null) {
            throw new ParameterNotFoundException("vehicle parameter is null.");
        }

        boolean isValidVehicle = VehicleValidation.isValidVehicleType(vehicle.getType(), tollConfiguration.getVehicleTypes());
        if(Boolean.FALSE.equals(isValidVehicle)){
            String formattedMessage = MessageFormat.format("Vehicle should be a valid. It should be of one of these vehicles: {0}" , tollConfiguration.getVehicleTypes());
            throw new APIBadRequestException(formattedMessage);
        }

        if (datesInput == null) {
            throw new ParameterNotFoundException("dates parameter is null.");
        }

        List<LocalDateTime> dates = datesInput.stream().sorted().collect(Collectors.toList());

        Map<LocalDate, List<LocalDateTime>> collect = dates.stream()
                .collect(Collectors.groupingBy(LocalDateTime::toLocalDate));

        if(collect.size()>1){
            throw new APIBadRequestException("Input dates are not valid as it includes the different days.");
        }

        if (isTollFreeVehicle(vehicle)) {
            log.info("***** Vehicle[{}] is free from toll tax, so returning 0 fee.", vehicle.getType());
            return 0;
        }
        log.info("***** Calculating toll-fees for Vehicle[{}] ***** ", vehicle.getType());


        int totalFees = 0;
        for (Map.Entry<LocalDate, List<LocalDateTime>> entry : collect.entrySet()) {
            totalFees = totalFees + getTollFeesPerDay(vehicle, entry.getValue());
        }
        log.info("Vehicle[{}] Final toll fees: {}", vehicle.getType(), totalFees);
        log.info("***** End processing toll-fees for Vehicle[{}] *****", vehicle.getType());
        return totalFees;

    }

    public int getTollFeesPerDay(Vehicle vehicle, List<LocalDateTime> localDateTimes) {

        LocalDateTime firstLocalDateTime = localDateTimes.get(0);
        if (isTollFreeDate(firstLocalDateTime)) return 0;

        int tempFee = getTollFee(firstLocalDateTime, vehicle);
        LocalDateTime tempDateTime = firstLocalDateTime;
        int totalFee = tempFee;
        List<LocalDateTime> dates = localDateTimes.stream().skip(1).collect(Collectors.toList());

        for (LocalDateTime localDateTime : dates) {
            int nextFee = getTollFee(localDateTime, vehicle);
            long minutes = tempDateTime.until(localDateTime, ChronoUnit.MINUTES);
            if(isMinutesLessThanHour(minutes)) {
                if (totalFee > 0) {
                    totalFee = totalFee - tempFee;
                }
                //consider maximum fees
                tempFee =  Math.max(nextFee, tempFee);
                totalFee = totalFee + tempFee;
            } else {
                //Consider next time slot
                tempDateTime = localDateTime;
                totalFee = totalFee + nextFee;
                tempFee = nextFee;
            }

            int maximumTollFees = tollConfiguration.getMaximumTollFeesPerDay();
            if (totalFee > maximumTollFees) {
                log.info("Calculated Total Fee: {} is more than the maximum toll fees, hence returning the maximum toll fees: {}", totalFee, maximumTollFees);
                totalFee = maximumTollFees;
                break;
            }
        }

        log.info("Total toll fees: {} for date: {}", totalFee, firstLocalDateTime.toLocalDate());

        return totalFee;
    }

    private boolean isMinutesLessThanHour(long minutes) {
        return minutes <= 60;
    }

    private boolean isTollFreeVehicle(Vehicle vehicle) {
        if (vehicle == null) return false;
        String vehicleType = vehicle.getType();
        boolean tollFreeVehicle = tollConfiguration.getTollFreeVehicles().stream().anyMatch(e -> e.equals(vehicleType));
        if (tollFreeVehicle) {
            log.info("Vehicle[{}] is free from toll tax.", vehicle.getType());
        }
        return tollFreeVehicle;
    }

    public int getTollFee(final LocalDateTime localDateTime, Vehicle vehicle) {

        if (isTollFreeDate(localDateTime) || isTollFreeVehicle(vehicle)) return 0;

        LocalTime localTime = localDateTime.toLocalTime();

        Optional<TimeslotFees> any = tollConfiguration.getTimeslotFees().stream().filter(
                e -> localTime.isBefore(e.getEndTime()) && localTime.isAfter(e.getStartTime())
        ).findAny();

        int tollFees = any.map(TimeslotFees::getFees).orElse(0);
        log.info("Vehicle[{}], toll fees:{} for date:{}",vehicle.getType(), tollFees, localDateTime);
        return tollFees;
    }

    private boolean isTollFreeDate(LocalDateTime localDateTime) {
        LocalDate localDate = localDateTime.toLocalDate();
        return isWeekend(localDate) || isHoliday(localDate);
    }

    private boolean isHoliday(final LocalDate localDate) {
        boolean holidayFlag = tollConfiguration.getHolidays().contains(MonthDay.from(localDate));
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

