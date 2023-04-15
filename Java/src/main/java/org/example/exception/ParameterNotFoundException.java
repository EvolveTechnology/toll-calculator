package org.example.exception;

public class ParameterNotFoundException extends RuntimeException {
    public ParameterNotFoundException(String message) {
        super(message);
    }
}
