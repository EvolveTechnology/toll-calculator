package test_utils;

import calculator.Specifications;
import calculator.Vehicle;

public class TestCaseBase {
    public final String name;
    public final Specifications specifications;
    public final Vehicle actualVehicle;
    public final int expected;

    public TestCaseBase(String name,
                        Specifications specifications,
                        Vehicle actualVehicle,
                        int expected)
    {
        this.name = name;
        this.specifications = specifications;
        this.actualVehicle = actualVehicle;
        this.expected = expected;
    }

    @Override
    public String toString()
    {
        return name;
    }
}
