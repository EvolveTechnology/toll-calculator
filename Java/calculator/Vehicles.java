package calculator;

/**
 * Definition of vehicle types, and factory methods for creating vehicles.
 */
public final class Vehicles {

    public static final String
            CAR = "Car",
            MOTORBIKE = "Motorbike",
            TRACTOR = "Tractor",
            EMERGENCY = "Emergency",
            DIPLOMAT = "Diplomat",
            FOREIGN = "Foreign",
            MILITARY = "Military";

    public static Vehicle of(String type)
    {
        return new Vehicle(type);
    }

    public static Vehicle newCar()
    {
        return of(CAR);
    }

    public static Vehicle newMotorbike()
    {
        return of(MOTORBIKE);
    }
}
