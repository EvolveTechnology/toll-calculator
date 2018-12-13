import emergency from "../../assets/emergency.png";
import military from "../../assets/military.png";
import car from "../../assets/car.png";
import motorbike from "../../assets/motorbike.png";
import tractor from "../../assets/tractor.png";
import truck from "../../assets/truck.png";
import diplomat from "../../assets/diplomat.png";
import foreign from "../../assets/foreign.png";
import bus from "../../assets/bus.png";
import wheel from "../../assets/wheel.png";

export default type => {
  switch (type) {
    case "Car":
      return car;
    case "Bus":
      return bus;
    case "Military":
      return military;
    case "Emergency":
      return emergency;
    case "Truck":
      return truck;
    case "Diplomat":
      return diplomat;
    case "Foreign":
      return foreign;
    case "Motorbike":
      return motorbike;
    case "Tractor":
      return tractor;
    default:
      return wheel;
  }
};
