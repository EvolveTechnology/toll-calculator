package org.example.controller;

import org.example.DateUtil;
import org.example.data.Car;
import org.example.data.Motorbike;
import org.example.data.OtherVehicle;
import org.example.data.TollFeeRequest;
import org.example.data.TollFeeResponse;
import org.example.data.Vehicle;
import org.example.service.TollCaculatorService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.Date;
import java.util.List;
import java.util.stream.Collectors;


import lombok.extern.slf4j.Slf4j;

@RestController
@Slf4j
@RequestMapping("/toll/calculator")
public class TollCalculatorController {

    @Autowired
    private TollCaculatorService tollCaculatorService;

    @PostMapping
    public ResponseEntity<TollFeeResponse> calculateTollFees(@RequestBody TollFeeRequest tollFeeRequest){

       String vehicleStr = tollFeeRequest.getVehicle();
       Vehicle vehicle = null;
       if (vehicleStr.equals("Car")) {
           vehicle = new Car();
       } else if(vehicleStr.equals("Motorbike")) {
           vehicle = new Motorbike();
       } else {
           vehicle = new OtherVehicle(vehicleStr);
       }
       List<Date> dateList = tollFeeRequest.getDates().stream().map(DateUtil::toDate).collect(Collectors.toList());
       return ResponseEntity.ok(tollCaculatorService.getTollFees(vehicle, dateList));
    }
}
