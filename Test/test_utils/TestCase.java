package test_utils;

import calculator.Vehicle;
import util.Day;
import util.TimeOfDay;

import java.util.Date;

import static test_utils.DateTestDataBuilder.timeOf;

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

    public TestCase(Date actualTime, Vehicle actualVehicle, int expected) {
        this("", actualTime, actualVehicle, expected);
    }

    public TestCase(Day actualDay, TimeOfDay actualTimeOfDay, Vehicle actualVehicle, int expected) {
        this("", timeOf(actualDay, actualTimeOfDay), actualVehicle, expected);
    }
}
