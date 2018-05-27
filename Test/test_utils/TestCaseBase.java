package test_utils;

import calculator.Configuration;
import calculator.Vehicle;

public class TestCaseBase {
    public final String name;
    public final Configuration configuration;
    public final Vehicle actualVehicle;
    public final int expected;

    public TestCaseBase(String name,
                        Configuration configuration,
                        Vehicle actualVehicle,
                        int expected)
    {
        this.name = name;
        this.configuration = configuration.clone();
        this.actualVehicle = actualVehicle;
        this.expected = expected;
    }

    @Override
    public String toString()
    {
        return name;
    }
}
