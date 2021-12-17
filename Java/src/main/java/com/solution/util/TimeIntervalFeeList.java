package com.solution.util;

import lombok.Data;
import lombok.NoArgsConstructor;
import java.util.List;

@Data
public class TimeIntervalFeeList {

    private List<TimeFeeObj> timeFeeList;

    @Data
    @NoArgsConstructor
    public static class TimeFeeObj{
        private String start;
        private String end;
        private Double fee;
    }
}
