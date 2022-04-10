package com.tollcalculator.pojo;

import com.tollcalculator.constants.TollCalculatorConstants;

public class Foreign implements Vehicle{
    @Override
    public String getType() {
        return TollCalculatorConstants.FOREIGN;
    }
}
