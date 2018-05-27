package test_utils;

import calculator.Vehicle;
import util.Day;
import util.TimeOfDay;

import java.util.Date;

import static test_utils.DateTestDataBuilder.timeOf;

public class TestCase extends TestCaseBase {
    public final Date actualTime;

    public TestCase(String name, Date actualTime, Vehicle actualVehicle, int expected) {
        super(expected, actualVehicle, name);
        this.actualTime = actualTime;
    }

    public TestCase(Date actualTime, Vehicle actualVehicle, int expected) {
        this("", actualTime, actualVehicle, expected);
    }

    public TestCase(Day actualDay, TimeOfDay actualTimeOfDay, Vehicle actualVehicle, int expected) {
        this("", timeOf(actualDay, actualTimeOfDay), actualVehicle, expected);
    }

}
