package org.example.exception;


import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;

@ControllerAdvice
public class ApiExceptionHandler {
    @ExceptionHandler(ParameterNotFoundException.class)
    public ResponseEntity<ErrorDetails> resourceNotFoundException(ParameterNotFoundException ex) {
        ErrorDetails errorDetails = new ErrorDetails( ex.getMessage());
        return new ResponseEntity<>(errorDetails, HttpStatus.NOT_FOUND);
    }

    @ExceptionHandler(Exception.class)
    public ResponseEntity<ErrorDetails> globleExcpetionHandler(Exception ex) {
        ErrorDetails errorDetails = new ErrorDetails( ex.getMessage());
        return new ResponseEntity<>(errorDetails, HttpStatus.INTERNAL_SERVER_ERROR);
    }
}