package org.example.exception;

public class APIBadRequestException extends RuntimeException {
    public APIBadRequestException(String message) {
        super(message);
    }

}
