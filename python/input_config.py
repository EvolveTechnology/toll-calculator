from Vehicles import (
    Foreign,
    Vehicle,
    Car,
    Motorbike,
    Tractor,
    Diplomat,
    Emergency,
    Military,
    Foreign,
)


def unique_vehicles_from_license_plate(stamps: list) -> list:
    unique_vehicles = []
    for vehicle in stamps:
        if not any(
            v_plate["license_plate"] == vehicle["license_plate"]
            for v_plate in unique_vehicles
        ):
            v = {
                "vehicle": vehicle["vehicle"],
                "license_plate": vehicle["license_plate"],
                "dates": all_stamps_for_vehicle(vehicle["license_plate"], stamps),
            }
            unique_vehicles.append(v)

    return unique_vehicles


def all_stamps_for_vehicle(license_plate: str, toll_stamps: list) -> list:
    dates = []
    for stamp in toll_stamps:
        if stamp["license_plate"] == license_plate:
            dates.append(stamp["date"])

    return dates


def create_vehicle(vehicle: dict) -> Vehicle:
    if vehicle["vehicle"] == "car":
        return Car(vehicle["license_plate"])
    if vehicle["vehicle"] == "motorbike":
        return Motorbike()
    if vehicle["vehicle"] == "tractor":
        return Tractor()
    if vehicle["vehicle"] == "diplomat":
        return Diplomat()
    if vehicle["vehicle"] == "emergency":
        return Emergency()
    if vehicle["vehicle"] == "military":
        return Military()
    if vehicle["vehicle"] == "foreign":
        return Foreign()
    else:
        return Exception("Vehicle type not allowed.")
