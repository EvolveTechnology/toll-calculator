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

import com.evolve_technology.calculator.controller.model.TollFreeDateDTO;
import com.evolve_technology.calculator.service.TollFreeDatesService;


@RestController
@Validated
public class TollFreeDatesController {

	@Autowired
	TollFreeDatesService tollFreeDatesService;
	
	@GetMapping("/tollfree/dates")
	public ResponseEntity<List<String>> getTollFreeDates(){
		List<String> list=tollFreeDatesService.getTollFreeDates();
		return new ResponseEntity<>(list,HttpStatus.OK);
	} 
	
	@GetMapping("/tollfree/dates/{date}")
	public ResponseEntity<TollFreeDateDTO> getTollFreeDateStatus(@PathVariable("date") 
	@Pattern(regexp = "^((20)[0-9][0-9])-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])$") String date){
			Boolean value=tollFreeDatesService.isTollFreeDate(date);
			TollFreeDateDTO tollFreeDateDTO=new TollFreeDateDTO();
			tollFreeDateDTO.setDate(date);
			tollFreeDateDTO.setIsTollFreeDate(value);
			return new ResponseEntity<>(tollFreeDateDTO,HttpStatus.OK);
		
	} 
}
