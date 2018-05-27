package test_utils;

import calculator.Specifications;
import calculator.Vehicle;

public class TestCaseBuilderBase {
    String nameHeader;
    String nameTail;

    Specifications specifications = Specifications.newDefault();

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
