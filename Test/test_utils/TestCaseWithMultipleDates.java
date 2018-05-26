package test_utils;

import calculator.Vehicle;

import java.util.Date;

public class TestCaseWithMultipleDates {
    public final String name;
    public final Vehicle actualVehicle;
    public final Date[] actualTimes;
    public final int expected;

    public TestCaseWithMultipleDates(String name, Date[] actualTimes, Vehicle actualVehicle, int expected) {
        this.name = name;
        this.actualTimes = actualTimes;
        this.actualVehicle = actualVehicle;
        this.expected = expected;
    }

    @Override
    public String toString() {
        return name;
    }
}
