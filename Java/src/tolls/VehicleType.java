package tolls;

enum VehicleType {
    MOTORBIKE(true),
    TRACTOR(true),
    EMERGENCY(true),
    DIPLOMAT(true),
    FOREIGN(true),
    MILITARY(true),
    CAR(false);

    private final boolean tollFree;

    VehicleType(boolean tollFree) {
        this.tollFree = tollFree;
    }

    public boolean isTollFree() {
        return tollFree;
    }
}
