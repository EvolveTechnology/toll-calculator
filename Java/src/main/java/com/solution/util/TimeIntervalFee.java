package com.solution.util;

import lombok.Data;
import lombok.experimental.Accessors;
import java.time.LocalTime;

@Data
@Accessors(chain = true)
public class TimeIntervalFee {

    private LocalTime startTime;
    private LocalTime endTime;
    private double fee;
}
