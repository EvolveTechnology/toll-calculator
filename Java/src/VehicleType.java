enum VehicleType {
    MOTORBIKE("Motorbike", true),
    TRACTOR("Tractor", true),
    EMERGENCY("Emergency", true),
    DIPLOMAT("Diplomat", true),
    FOREIGN("Foreign", true),
    MILITARY("Military", true),
    CAR("Car", false);

    private final String type;
    private final boolean tollFree;

    VehicleType(String type, boolean tollFree) {
        this.type = type;
        this.tollFree = tollFree;
    }

    public String getType() {
        return type;
    }

    public boolean isTollFree() {
        return tollFree;
    }
}
