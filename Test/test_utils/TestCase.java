package test_utils;

import calculator.Vehicle;

import java.util.Date;

public class TestCase {
    public final String name;
    public final Date actualTime;
    public final Vehicle actualVehicle;
    public final int expected;

    public TestCase(String name, Date actualTime, Vehicle actualVehicle, int expected) {
        this.name = name;
        this.actualTime = actualTime;
        this.actualVehicle = actualVehicle;
        this.expected = expected;
    }
}
