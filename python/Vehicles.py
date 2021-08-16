from abc import ABC, abstractmethod


class Vehicle(ABC):
    @abstractmethod
    def is_vehicle_tollable(self) -> bool:
        pass

    @abstractmethod
    def get_vehicle_type(self) -> str:
        pass


class Car(Vehicle):
    def __init__(self, license_plate):
        self.license_plate = license_plate

    def is_vehicle_tollable(self) -> bool:
        return True

    def get_vehicle_type(self) -> str:
        return "car"


class Motorbike(Vehicle):
    def is_vehicle_tollable(self) -> bool:
        return False

    def get_vehicle_type(self) -> str:
        return "motorbike"


class Tractor(Vehicle):
    def is_vehicle_tollable(self) -> bool:
        return False

    def get_vehicle_type(self) -> str:
        return "tractor"


class Emergency(Vehicle):
    def is_vehicle_tollable(self) -> bool:
        return False

    def get_vehicle_type(self) -> str:
        return "emergency"


class Diplomat(Vehicle):
    def is_vehicle_tollable(self) -> bool:
        return False

    def get_vehicle_type(self) -> str:
        return "diplomat"


class Foreign(Vehicle):
    def is_vehicle_tollable(self) -> bool:
        return False

    def get_vehicle_type(self) -> str:
        return "foreign"


class Military(Vehicle):
    def is_vehicle_tollable(self) -> bool:
        return False

    def get_vehicle_type(self) -> str:
        return "military"
