package com.evolve.tollCalculator.util;


import static com.evolve.tollCalculator.util.Constants.*;

public enum TollFreeVehicles {
        MOTORBIKE(VEHICLE_TYPE_MOTORBIKE),
        TRACTOR(VEHICLE_TYPE_TRACTOR),
        EMERGENCY(VEHICLE_TYPE_EMERGENCY),
        DIPLOMAT(VEHICLE_TYPE_DIPLOMAT),
        FOREIGN(VEHICLE_TYPE_FOREIGN),
        MILITARY(VEHICLE_TYPE_MILITARY);

        private final String type;

        TollFreeVehicles(String type) {
            this.type = type;
        }

        public String getType() {
            return type;
        }
}

