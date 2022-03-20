package com.evolve_technology.calculator.controller.model;

import lombok.Data;

@Data
public class TollFreeDateDTO {
	String date;
	Boolean isTollFreeDate;
	public String getDate() {
		return date;
	}
	public void setDate(String date) {
		this.date = date;
	}
	public Boolean getIsTollFreeDate() {
		return isTollFreeDate;
	}
	public void setIsTollFreeDate(Boolean isTollFreeDate) {
		this.isTollFreeDate = isTollFreeDate;
	}
	
}
