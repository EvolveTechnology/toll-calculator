package org.example.util;

import javax.validation.constraints.NotEmpty;
import javax.validation.constraints.NotNull;
import java.util.List;

public class VehicleValidation {

    public static boolean isValidVehicleType(@NotNull String vehicle, @NotEmpty List<String> vehicleTypes){
        return vehicleTypes.contains(vehicle);
    }

}
