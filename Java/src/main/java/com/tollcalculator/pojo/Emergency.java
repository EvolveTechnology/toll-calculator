package com.tollcalculator.pojo;

import com.tollcalculator.constants.TollCalculatorConstants;

public class Emergency implements Vehicle{
    @Override
    public String getType() {
        return TollCalculatorConstants.EMERGENCY;
    }
}
