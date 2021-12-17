package com.solution.pojo;

import static com.solution.util.Constants.CAR;

public class Car implements Vehicle {
    @Override
    public String getType() {
        return CAR;
    }
}
