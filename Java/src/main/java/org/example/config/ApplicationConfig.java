package org.example.config;

import org.example.TollCalculator;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import java.time.MonthDay;
import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.Stream;

@Configuration
public class ApplicationConfig {


    @Value("${toll-configuration.maximumFees}")
    private int maximumFees;

    @Value("${toll-configuration.holidays}")
    private String[] holidays;


    @Value("${toll-configuration.timeslot-fees}")
    private String[] timeslotFees;

    @Bean
    TollCalculator tollCalculator(){
        TollFeeConfiguration  tollFeeConfiguration = new TollFeeConfiguration();
        tollFeeConfiguration.setMaximumTollFeesPerDay(maximumFees);
        List<MonthDay> holidayList = Stream.of(holidays).map(e -> {
            String[] d = e.split("-");
            return MonthDay.of(Integer.parseInt(d[0]), Integer.parseInt(d[1]));
        }).collect(Collectors.toList());
        tollFeeConfiguration.setHolidays(holidayList);

        List<TimeslotFees> timeslotFeeList = Stream.of(timeslotFees).map(e -> {
            String[] t = e.split("-");
            return new TimeslotFees(t[0],t[1],Integer.parseInt(t[2]));
        }).collect(Collectors.toList());
        tollFeeConfiguration.setTimeslotFees(timeslotFeeList);
        return  new TollCalculator(tollFeeConfiguration);
    }
}
