package test_utils;

import calculator.Configuration;
import calculator.Vehicle;

import java.util.Date;

public class TestCaseWithMultipleDates extends TestCaseBase {
    public final Date[] actualTimes;

    public TestCaseWithMultipleDates(String name,
                                     Configuration configuration,
                                     Vehicle actualVehicle,
                                     Date[] actualTimes,
                                     int expected)
    {
        super(name, configuration, actualVehicle, expected);
        this.actualTimes = actualTimes;
    }
}
