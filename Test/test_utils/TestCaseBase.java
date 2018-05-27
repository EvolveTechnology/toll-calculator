package test_utils;

import calculator.Vehicle;

public class TestCaseBase {
    public final String name;
    public final Vehicle actualVehicle;
    public final int expected;

    public TestCaseBase(int expected, Vehicle actualVehicle, String name) {
        this.expected = expected;
        this.actualVehicle = actualVehicle;
        this.name = name;
    }

    @Override
    public String toString() {
        return name;
    }
}
