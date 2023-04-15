package org.example;

import java.time.LocalTime;
import java.time.format.DateTimeFormatter;

import lombok.Getter;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;

@Slf4j
@RequiredArgsConstructor
public class TimeslotFees {

    private static final DateTimeFormatter formatter = DateTimeFormatter.ofPattern("HH:mm");

    @Getter
    LocalTime startTime;

    @Getter
    LocalTime endTime;

    @Getter
    int fees;

    public TimeslotFees(String startTime, String endTime, int fees) {
        this.startTime = LocalTime.parse(startTime, formatter);
        this.endTime = LocalTime.parse(endTime, formatter);
        this.fees = fees;
    }

}
