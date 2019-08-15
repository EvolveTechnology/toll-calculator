import enum

from typing_extensions import Final


class Vehicle(enum.Enum):
    car = "CAR"
    motorbike = "MOTORBIKE"
    tractor = "TRACTOR"
    emergency = "EMERGENCY"
    diplomat = "DIPLOMAT"
    foreign = "FOREIGN"
    military = "MILITARY"


tolled_vehicles: Final = (Vehicle.car,)


def is_toll_free_vehicle(vehicle: Vehicle) -> bool:
    """
    Usage:
    >>> is_toll_free_vehicle(Vehicle("CAR"))
    False
    >>> is_toll_free_vehicle(Vehicle("MOTORBIKE"))
    True
    """
    return vehicle not in tolled_vehicles
