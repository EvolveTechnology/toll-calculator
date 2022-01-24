package com.evolve.tollCalculator.model;

import com.evolve.tollCalculator.util.TollFreeVehicles;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

public class FreeCarTypeFactory {
    private static final Logger logger = LogManager.getLogger(FreeCarTypeFactory.class);

    public static Vehicle initVehicleByType(String type) {
        try {
            if (TollFreeVehicles.valueOf(type.toUpperCase()) != null) {
                return new Vehicle() {
                    @Override
                    public String getType() {
                        return type;
                    }
                };
            }
        } catch (Exception e) {
            logger.info("the car type {} is not existing",type);
        }
        return new Car();
    }

}
