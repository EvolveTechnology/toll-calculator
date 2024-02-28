package org.example.config;

import java.time.MonthDay;
import java.util.List;

import lombok.Data;
import lombok.ToString;
import org.springframework.boot.context.properties.ConfigurationProperties;
import org.springframework.boot.context.properties.EnableConfigurationProperties;
import org.springframework.context.annotation.Configuration;
import org.springframework.format.annotation.DateTimeFormat;

@Data
@Configuration
@EnableConfigurationProperties
@ConfigurationProperties(prefix = "toll-configuration")
@ToString
public class TollConfiguration {
    private List<TimeslotFees> timeslotFees;

    @DateTimeFormat(pattern = "MM:mm")
    private List<MonthDay> holidays;

    private List<String> vehicleTypes;

    private List<String> tollFreeVehicles;

    private int maximumTollFeesPerDay;


}
