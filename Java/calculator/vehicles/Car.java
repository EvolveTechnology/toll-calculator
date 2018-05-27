package calculator.vehicles;

import calculator.Vehicle;
import calculator.Vehicles;

public class Car implements Vehicle {
    @Override
    public String getType() {
        return Vehicles.CAR;
    }
}
