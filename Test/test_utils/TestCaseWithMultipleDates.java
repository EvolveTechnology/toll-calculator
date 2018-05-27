package test_utils;

import calculator.Vehicle;

import java.util.Date;

public class TestCaseWithMultipleDates extends TestCaseBase {
    public final Date[] actualTimes;

    public TestCaseWithMultipleDates(String name, Date[] actualTimes, Vehicle actualVehicle, int expected) {
        super(expected, actualVehicle, name);
        this.actualTimes = actualTimes;
    }
}
