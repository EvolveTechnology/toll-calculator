package test_utils;

import calculator.Configuration;
import calculator.Vehicle;

import java.util.Date;

public class TestCase extends TestCaseBase {
    public final Date actualTime;

    public TestCase(String name,
                    Configuration configuration,
                    Vehicle actualVehicle,
                    Date actualTime,
                    int expected)
    {
        super(name, configuration, actualVehicle, expected);
        this.actualTime = actualTime;
    }
}
