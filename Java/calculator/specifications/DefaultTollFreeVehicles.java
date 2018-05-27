package calculator.specifications;

import calculator.Vehicle;
import calculator.Vehicles;

import java.util.Arrays;
import java.util.HashSet;
import java.util.Set;
import java.util.function.Predicate;

public class DefaultTollFreeVehicles implements Predicate<Vehicle> {

    private static final Set<String> TOLL_FREE_TYPES =
            new HashSet<>(Arrays.asList(
                    Vehicles.MOTORBIKE,
                    Vehicles.TRACTOR,
                    Vehicles.EMERGENCY,
                    Vehicles.DIPLOMAT,
                    Vehicles.FOREIGN,
                    Vehicles.MILITARY
            ));

    @Override
    public boolean test(Vehicle vehicle)
    {
        return vehicle != null && TOLL_FREE_TYPES.contains(vehicle.getType());
    }
}
