package com.evolve.services;

import java.time.LocalTime;
import java.util.HashSet;
import java.util.Set;

public class TollFeeServiceImpl implements TollFeeService {
    private static Set<TollFeePeriod> tollFeeByPeriod;

    @Override
    public int getTollFee(LocalTime time) {
        return tollFeeByPeriod.stream().filter(t -> t.containsTime(time)).findFirst().get().getFee();
    }
    static {
        tollFeeByPeriod = new HashSet<>();
        /*
          The toll fees (according to the original code with fix of several bugs).
          Period            Fee
          00:00 - 05:59     0
          06:00 - 06:29     8
          06:30 - 06:59     13
          07:00 - 07:59     18
          08:00 - 08:29     13
          08:30 - 14:59     8
          15:00 - 15:29     13
          15:30 - 16:59     18
          17:00 - 17:59     13
          18:00 - 18:29     8
          18:30 - 23:59     0
         */
        tollFeeByPeriod.add(new TollFeePeriod(LocalTime.of(0, 0), LocalTime.of(5, 59), 0));
        tollFeeByPeriod.add(new TollFeePeriod(LocalTime.of(6, 0), LocalTime.of(6, 29), 8));
        tollFeeByPeriod.add(new TollFeePeriod(LocalTime.of(6, 30), LocalTime.of(6, 59), 13));
        tollFeeByPeriod.add(new TollFeePeriod(LocalTime.of(7, 0), LocalTime.of(7, 59), 18));
        tollFeeByPeriod.add(new TollFeePeriod(LocalTime.of(8, 0), LocalTime.of(8, 29), 13));
        tollFeeByPeriod.add(new TollFeePeriod(LocalTime.of(8, 30), LocalTime.of(14, 59), 8));
        tollFeeByPeriod.add(new TollFeePeriod(LocalTime.of(15, 0), LocalTime.of(15, 29), 13));
        tollFeeByPeriod.add(new TollFeePeriod(LocalTime.of(15, 30), LocalTime.of(16, 59), 18));
        tollFeeByPeriod.add(new TollFeePeriod(LocalTime.of(17, 0), LocalTime.of(17, 59), 13));
        tollFeeByPeriod.add(new TollFeePeriod(LocalTime.of(18, 0), LocalTime.of(18, 29), 8));
        tollFeeByPeriod.add(new TollFeePeriod(LocalTime.of(18, 30), LocalTime.of(23, 59), 0));
    }
}
