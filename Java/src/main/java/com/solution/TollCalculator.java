package com.solution;

import com.solution.pojo.Vehicle;
import com.solution.service.RoadTollService;
import java.time.LocalDateTime;
import java.time.LocalTime;
import java.util.Arrays;
import java.util.List;
import java.util.stream.Collectors;
import static com.solution.util.Constants.*;

/**
 * @description:  Entry class for calculating tolls
 * @author: Richard(Duo.Wang)
 * @createDate: 2021/12/16 - 11:18
 * @version: v1.0
 */
public class TollCalculator {

    private RoadTollService roadTollService;

    public TollCalculator(RoadTollService roadTollService) {
        this.roadTollService = roadTollService;
    }


    /**
     *
     * Calculate the total fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total fee for the day
     *
     */
    public double getTollFee(Vehicle vehicle, LocalDateTime... dates) {

        if (!roadTollService.isValid(vehicle, dates)) {
            return 0;
        }
        // filtered and sorted time
        List<LocalTime> validTimeList = Arrays.stream(dates)
                .map(LocalDateTime::toLocalTime)
                .filter(time -> roadTollService.getFee(time) > 0)
                .sorted()
                .collect(Collectors.toList());

        if (validTimeList.isEmpty()) {
            return 0;
        }

        LocalTime lastHourTime = validTimeList.get(0);
        double tempFee = roadTollService.getFee(lastHourTime);
        double totalFee = 0;
        for (LocalTime nextTime : validTimeList) {
            double nextFee = roadTollService.getFee(nextTime);

            if (lastHourTime.plusMinutes(LIMIT_MINUTES_RECHARGE).isAfter(nextTime)) {
                if (totalFee > 0) {
                    totalFee -= tempFee;
                }
                if (nextFee >= tempFee) {
                    tempFee = nextFee;
                }
                totalFee += tempFee;
            } else {
                totalFee += nextFee;
                lastHourTime = nextTime;
                tempFee = nextFee;

                /**
                 * The maximum fee for one day is 60 SEK
                 */
                if (totalFee >= MAX_FEE_FOR_ONE_DAY) {
                    return MAX_FEE_FOR_ONE_DAY;
                }
            }
        }
        return Math.min(totalFee, MAX_FEE_FOR_ONE_DAY);
    }
}
