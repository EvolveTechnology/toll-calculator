package com.evolve_technology.calculator.exception;


import org.springframework.http.HttpStatus;

import lombok.Data;

@Data
public class CustomErrorException extends RuntimeException {
	private HttpStatus status = null;

	private Object data = null;

	public CustomErrorException() {
		super();
	}

	public CustomErrorException(String message) {
		super(message);
	}

	public CustomErrorException(HttpStatus status, String message) {
		this(message);
		this.status = status;
	}

	public CustomErrorException(HttpStatus status, String message, Object data) {
		this(status, message);
		this.data = data;
	}
}