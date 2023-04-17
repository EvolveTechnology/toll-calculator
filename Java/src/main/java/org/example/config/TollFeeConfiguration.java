package org.example.config;

import java.time.MonthDay;
import java.util.List;

import lombok.Data;

@Data
public class TollFeeConfiguration {
    private List<TimeslotFees> timeslotFees;
    private List<MonthDay> holidays;
    private int maximumTollFeesPerDay;
}
