package test_utils;

import calculator.Configuration;
import calculator.Vehicle;

public class TestCaseBuilderBase {
    String nameHeader;
    String nameTail;

    Configuration configuration = Configuration.newDefault();

    Vehicle actualVehicle;
    int expected;

    String name()
    {
        if (nameHeader == null) {
            return nameTail;
        } else if (nameTail == null) {
            return nameHeader;
        } else {
            return nameHeader + ": " + nameTail;
        }
    }
}
