package com.tollcalculator.pojo;

import com.tollcalculator.constants.TollCalculatorConstants;

public class Diplomat implements Vehicle{
    @Override
    public String getType() {
        return TollCalculatorConstants.DIPLOMAT;
    }
}
