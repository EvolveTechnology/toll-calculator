package com.tollcalculator.pojo;

import com.tollcalculator.constants.TollCalculatorConstants;

public class Tractor implements Vehicle{

    @Override
    public String getType() {
        return TollCalculatorConstants.TRACTOR;
    }
}
