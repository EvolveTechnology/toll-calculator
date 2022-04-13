package com.tollcalculator.pojo;

import com.tollcalculator.constants.TollCalculatorConstants;

public class Military implements Vehicle{
    @Override
    public String getType() {
        return TollCalculatorConstants.MILITARY;
    }
}
