package com.work.exceptions;

public class MissingHolidayDataException extends Exception {
    public MissingHolidayDataException(Throwable cause) {
        super("Unable to get Holiday list for the date", cause);
    }
}
