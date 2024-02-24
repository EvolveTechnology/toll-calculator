package org.example.service;

import org.example.TollCalculator;
import org.example.data.TollFeeResponse;
import org.example.data.Vehicle;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.List;

@Service
public class TollCalculatorService {

    @Autowired
    private TollCalculator tollCalculator;

    public TollFeeResponse getTollFees(Vehicle vehicle, List<LocalDateTime> dateTimeList){
       int fees =  tollCalculator.getTollFee(vehicle, dateTimeList);
       return TollFeeResponse.builder().totalFees(fees).build();
    }
}
