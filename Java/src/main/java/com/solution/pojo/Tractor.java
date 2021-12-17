package com.solution.pojo;

import static com.solution.util.Constants.TRACTOR;

public class Tractor implements Vehicle {
    @Override
    public String getType() {
        return TRACTOR;
    }

}
