package org.example;


public class Motorbike implements Vehicle {
    @Override
    public String getType() {
        return TollFreeVehicles.MOTORBIKE.getType();
    }
}
