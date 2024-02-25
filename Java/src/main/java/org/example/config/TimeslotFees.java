package org.example.config;

import lombok.*;
import org.springframework.context.annotation.Configuration;

import java.time.LocalTime;
import java.time.format.DateTimeFormatter;

import lombok.extern.slf4j.Slf4j;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.stereotype.Component;

@Slf4j
@RequiredArgsConstructor
@Configuration
@Component
@Data
public class TimeslotFees {

    private static final DateTimeFormatter formatter = DateTimeFormatter.ofPattern("HH:mm");

    @DateTimeFormat(pattern = "HH:mm")
    private LocalTime startTime;

    @DateTimeFormat(pattern = "HH:mm")
    private  LocalTime endTime;

    private int fees;

    public TimeslotFees( String startTime, String endTime, int fees) {
        this.startTime = LocalTime.parse(startTime, formatter);
        this.endTime = LocalTime.parse(endTime, formatter);
        this.fees = fees;
    }

}
