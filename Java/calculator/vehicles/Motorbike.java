package calculator.vehicles;

import calculator.Vehicle;
import calculator.Vehicles;

public class Motorbike implements Vehicle {
    @Override
    public String getType() {
        return Vehicles.MOTORBIKE;
    }
}
