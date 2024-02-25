package org.example.controller;

import org.example.data.DefaultVehicle;
import org.example.data.TollFeeRequest;
import org.example.data.TollFeeResponse;
import org.example.data.Vehicle;
import org.example.service.TollCalculatorService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;


import lombok.extern.slf4j.Slf4j;

@RestController
@Slf4j
@RequestMapping("/toll/calculator")
public class TollCalculatorController {

    @Autowired
    private TollCalculatorService tollCalculatorService;

    @PostMapping
    public ResponseEntity<TollFeeResponse> calculateTollFees(@RequestBody TollFeeRequest tollFeeRequest){

       String vehicleStr = tollFeeRequest.getVehicle();

       Vehicle vehicle = new DefaultVehicle(vehicleStr);

       return ResponseEntity.ok(tollCalculatorService.getTollFees(vehicle, tollFeeRequest.getDates()));
    }
}
