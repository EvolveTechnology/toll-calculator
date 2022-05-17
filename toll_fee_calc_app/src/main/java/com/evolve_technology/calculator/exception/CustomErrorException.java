package com.evolve_technology.calculator.exception;

import lombok.Data;

@Data
public class CustomErrorException extends RuntimeException {

	private Object data = null;

	public CustomErrorException() {
		super();
	}

	public CustomErrorException(String message) {
		super(message);
	}

	public CustomErrorException(String message, Object data) {
		this(message);
		this.data = data;
	}
}