package com.solution.pojo;

import static com.solution.util.Constants.MILITARY;

public class Military implements Vehicle {
    @Override
    public String getType() {
        return MILITARY;
    }

}
