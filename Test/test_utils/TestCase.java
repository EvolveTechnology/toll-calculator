package test_utils;

import calculator.Specifications;
import calculator.Vehicle;
import util.Day;
import util.TimeOfDay;

import java.util.Date;

import static test_utils.DateTestDataBuilder.timeOf;

public class TestCase extends TestCaseBase {
    public final Date actualTime;

    public TestCase(String name, Specifications specifications, Vehicle actualVehicle, Date actualTime, int expected) {
        super(name, specifications, actualVehicle, expected);
        this.actualTime = actualTime;
    }

    public TestCase(Specifications specifications, Vehicle actualVehicle, Day actualDay, TimeOfDay actualTimeOfDay, int expected) {
        this("",
             specifications,
             actualVehicle, timeOf(actualDay, actualTimeOfDay),
             expected);
    }

    public TestCase(Vehicle actualVehicle, Day actualDay, TimeOfDay actualTimeOfDay, int expected) {
        this("",
             Specifications.newDefault(),
             actualVehicle, timeOf(actualDay, actualTimeOfDay),
             expected);
    }

}
