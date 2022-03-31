package com.evolve_technology.calculator.controller;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RestController;

import com.evolve_technology.calculator.controller.model.TollFreeVehicleDTO;
import com.evolve_technology.calculator.service.TollFreeVehicleService;


@RestController
@Validated
public class TollFreeVehiclesController {

	@Autowired
	TollFreeVehicleService tollFreeVehicleService;
	
	@GetMapping("/tollfree/vehicles")
	public ResponseEntity<List<String>> getTollFreeVehicles(){
		List<String> list=tollFreeVehicleService.getTollFreeVehicles();
		return new ResponseEntity<>(list,HttpStatus.OK);
		
	}
	
	@GetMapping("/tollfree/vehicles/{name}")
	public ResponseEntity<TollFreeVehicleDTO> getTollFreeVehicleStatusByName(@PathVariable("name") String name){
		Boolean value=tollFreeVehicleService.isTollFreeVehicle(name);
		TollFreeVehicleDTO tollFreeVehicleDTO=new TollFreeVehicleDTO();
		tollFreeVehicleDTO.setName(name);
		tollFreeVehicleDTO.setTollFreeVehicle(value);
		return new ResponseEntity<>(tollFreeVehicleDTO,HttpStatus.OK);
		
	}
}
