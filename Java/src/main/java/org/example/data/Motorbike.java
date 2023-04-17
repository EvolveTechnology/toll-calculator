package org.example.data;

public class Motorbike implements Vehicle {
    @Override
    public String getType() {
        return TollFreeVehicles.MOTORBIKE.getType();
    }
}
