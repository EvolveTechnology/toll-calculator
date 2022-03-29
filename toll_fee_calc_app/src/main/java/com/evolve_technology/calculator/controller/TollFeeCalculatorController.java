package com.evolve_technology.calculator.controller;

import java.util.List;

import javax.validation.constraints.Pattern;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RestController;

import com.evolve_technology.calculator.controller.model.TollFeeDTO;
import com.evolve_technology.calculator.controller.model.TollFreeDateDTO;
import com.evolve_technology.calculator.controller.model.TollFreeVehicleDTO;
import com.evolve_technology.calculator.service.TollFeeService;
import com.evolve_technology.calculator.service.TollFreeDatesService;
import com.evolve_technology.calculator.service.TollFreeVehicleService;

@RestController("/v1/api")
@Validated
public class TollFeeCalculatorController {
	
	@Autowired
	TollFeeService tollFreeService;
	
	@Autowired
	TollFreeDatesService tollFreeDatesService;
	
	@Autowired
	TollFreeVehicleService tollFreeVehicleService;
	
	@GetMapping("tollfee/vehicles/{name}/dates/{date}")
	public ResponseEntity<TollFeeDTO> getTollFee(@PathVariable("name") String name,@PathVariable("date") 
	@Pattern(regexp = "^((20)[0-9][0-9])-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])T(00|0[0-9]|1[0-9]|2[0-3]):(0[0-9]|[0-5][0-9]):(0[0-9]|[0-5][0-9])$") 
	String date) {
		int amount= tollFreeService.getTollFee(date, name);
		TollFeeDTO tollFeeDTO=new TollFeeDTO();
		tollFeeDTO.setAmount(amount);
		tollFeeDTO.setDate(date);
		tollFeeDTO.setName(name);
		return new ResponseEntity<>(tollFeeDTO,HttpStatus.OK);
	}

	@GetMapping("tollfree/vehicles")
	public ResponseEntity<List<String>> getTollFreeVehicles(){
		List<String> list=tollFreeVehicleService.getTollFreeVehicles();
		return new ResponseEntity<>(list,HttpStatus.OK);
		
	}
	
	@GetMapping("tollfree/vehicles/{name}")
	public ResponseEntity<TollFreeVehicleDTO> getTollFreeVehicleStatusByName(@PathVariable("name") String name){
		Boolean value=tollFreeVehicleService.isTollFreeVehicle(name);
		TollFreeVehicleDTO tollFreeVehicleDTO=new TollFreeVehicleDTO();
		tollFreeVehicleDTO.setName(name);
		tollFreeVehicleDTO.setTollFreeVehicle(value);
		return new ResponseEntity<>(tollFreeVehicleDTO,HttpStatus.OK);
		
	}
	
	@GetMapping("tollfree/dates")
	public ResponseEntity<List<String>> getTollFreeDates(){
		List<String> list=tollFreeDatesService.getTollFreeDates();
		return new ResponseEntity<>(list,HttpStatus.OK);
	} 
	
	@GetMapping("tollfree/dates/{date}")
	public ResponseEntity<TollFreeDateDTO> getTollFreeDateStatus(@PathVariable("date") 
	@Pattern(regexp = "^((20)[0-9][0-9])-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])$") String date){
			Boolean value=tollFreeDatesService.isTollFreeDate(date);
			TollFreeDateDTO tollFreeDateDTO=new TollFreeDateDTO();
			tollFreeDateDTO.setDate(date);
			tollFreeDateDTO.setIsTollFreeDate(value);
			return new ResponseEntity<>(tollFreeDateDTO,HttpStatus.OK);
		
	} 
	
	
}
