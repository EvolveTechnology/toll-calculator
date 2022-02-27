import java.util.Map;
import java.util.stream.Collectors;
import java.util.stream.Stream;

public enum TollVehiclesType {
    MOTORBIKE("Motorbike", true),
    TRACTOR("Tractor", true),
    EMERGENCY("Emergency", true),
    DIPLOMAT("Diplomat", true),
    FOREIGN("Foreign", true),
    MILITARY("Military", true),
    CAR("Car", false);

    private final String type;
    private final boolean free;

    TollVehiclesType(String type, boolean free) {
        this.type = type;
        this.free = free;
    }

    public boolean isFree() {
        return free;
    }

    public String getType() {
        return type;
    }

    private static final Map<String, TollVehiclesType> enums = Stream.of(TollVehiclesType.values())
            .collect(Collectors.toMap(TollVehiclesType::getType, v -> v));

}
