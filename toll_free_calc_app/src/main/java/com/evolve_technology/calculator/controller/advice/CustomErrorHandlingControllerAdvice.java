package com.evolve_technology.calculator.controller.advice;

import java.io.PrintWriter;
import java.io.StringWriter;

import javax.validation.ConstraintViolationException;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.RestControllerAdvice;

import com.evolve_technology.calculator.exception.CustomErrorException;
import com.evolve_technology.calculator.exception.ErrorResponse;

@RestControllerAdvice
public class CustomErrorHandlingControllerAdvice {

	@ExceptionHandler(CustomErrorException.class)
    public ResponseEntity<ErrorResponse> handleCustomErrorExceptions(
            Exception e
    ) {
        CustomErrorException customErrorException = (CustomErrorException) e;

        HttpStatus status = customErrorException.getStatus();

        // converting the stack trace to String
        StringWriter stringWriter = new StringWriter();
        PrintWriter printWriter = new PrintWriter(stringWriter);
        customErrorException.printStackTrace(printWriter);
        String stackTrace = stringWriter.toString();

        return new ResponseEntity<>(
                new ErrorResponse(
                        status,
                        customErrorException.getMessage(),
                        stackTrace,
                        customErrorException.getData()
                ),
                status
        );
    }
	
	 @ExceptionHandler(ConstraintViolationException.class)
	    public ResponseEntity<ErrorResponse> handleCustomParameterConstraintExceptions(
	        Exception e
	    ) {
	        HttpStatus status = HttpStatus.BAD_REQUEST; // 400
	        StringWriter stringWriter = new StringWriter();
	        PrintWriter printWriter = new PrintWriter(stringWriter);
	        e.printStackTrace(printWriter);
	        String stackTrace = stringWriter.toString();
	        return new ResponseEntity<>(
	        		 new ErrorResponse(
	                         status,
	                         e.getMessage(),
	                         stackTrace
	                 ),
	                 status
	        );
	    }
}
