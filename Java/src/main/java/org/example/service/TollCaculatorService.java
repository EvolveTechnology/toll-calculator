package org.example.service;

import org.example.TollCalculator;
import org.example.data.TollFeeResponse;
import org.example.data.Vehicle;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.Date;
import java.util.List;

@Service
public class TollCaculatorService {

    @Autowired
    private TollCalculator tollCalculator;

    public TollFeeResponse getTollFees(Vehicle vehicle, List<Date> dateList){
        Date[] dates = dateList.toArray(new Date[dateList.size()]);
       int fees =  tollCalculator.getTollFee(vehicle, dates);
       return TollFeeResponse.builder().totalFees(fees).build();
    }
}
