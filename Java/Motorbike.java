
public class Motorbike implements Vehicle {

  @Override
  public boolean isFree() {
    return getType().isFree();
  }

  @Override
  public TollVehiclesType getType() {
    return TollVehiclesType.MOTORBIKE;
  }
}
