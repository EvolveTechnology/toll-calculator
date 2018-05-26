package test_utils;

import calculator.Car;
import calculator.Motorbike;
import calculator.Vehicle;

public class TestData {

    public static Vehicle aNonFreeVehicle() {
        return new Car();
    }

    public static Vehicle aFreeVehicle() {
        return new Motorbike();
    }
}
