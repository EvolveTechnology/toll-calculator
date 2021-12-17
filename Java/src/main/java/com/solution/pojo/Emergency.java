package com.solution.pojo;

import static com.solution.util.Constants.EMERGENCY;

public class Emergency implements Vehicle {
    @Override
    public String getType() {
        return EMERGENCY;
    }

}
