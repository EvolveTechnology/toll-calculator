package se.kvrgic;

public enum Vehicle {
    CAR("Car"),
    MOTORBIKE("Motorbike"),
    TRACTOR("Tractor"),
    EMERGENCY("Emergency"),
    DIPLOMAT("Diplomat"),
    FOREIGN("Foreign"),
    MILITARY("Military");
    private final String type;

    Vehicle(String type) {
        this.type = type;
    }

    public String getType() {
        return type;
    }
}
