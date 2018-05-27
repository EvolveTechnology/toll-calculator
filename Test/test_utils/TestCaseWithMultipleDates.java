package test_utils;

import calculator.Specifications;
import calculator.Vehicle;

import java.util.Date;

public class TestCaseWithMultipleDates extends TestCaseBase {
    public final Date[] actualTimes;

    public TestCaseWithMultipleDates(String name,
                                     Specifications specifications,
                                     Vehicle actualVehicle,
                                     Date[] actualTimes,
                                     int expected)
    {
        super(name, specifications, actualVehicle, expected);
        this.actualTimes = actualTimes;
    }
}
