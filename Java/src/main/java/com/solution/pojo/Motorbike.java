package com.solution.pojo;

import static com.solution.util.Constants.MOTORBIKE;

public class Motorbike implements Vehicle {
    @Override
    public String getType() {
        return MOTORBIKE;
    }
}