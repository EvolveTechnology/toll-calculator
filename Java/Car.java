
public class Car implements Vehicle {

  @Override
  public boolean isFree() {
    return getType().isFree();
  }

  @Override
  public TollVehiclesType getType() {
    return TollVehiclesType.CAR;
  }
}
