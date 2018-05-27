package test_utils;

import calculator.Configuration;
import calculator.Vehicle;
import util.Day;
import util.TimeOfDay;

import java.util.Date;

import static test_utils.DateTestDataBuilder.timeOf;

public class TestCase extends TestCaseBase {
    public final Date actualTime;

    public TestCase(String name, Configuration configuration, Vehicle actualVehicle, Date actualTime, int expected)
    {
        super(name, configuration, actualVehicle, expected);
        this.actualTime = actualTime;
    }

    public TestCase(Configuration configuration, Vehicle actualVehicle, Day actualDay, TimeOfDay actualTimeOfDay, int expected)
    {
        this("",
             configuration,
             actualVehicle, timeOf(actualDay, actualTimeOfDay),
             expected);
    }

    public TestCase(Vehicle actualVehicle, Day actualDay, TimeOfDay actualTimeOfDay, int expected)
    {
        this("",
             Configuration.newDefault(),
             actualVehicle, timeOf(actualDay, actualTimeOfDay),
             expected);
    }

}
