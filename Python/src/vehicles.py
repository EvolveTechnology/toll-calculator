from enum import Enum

# A proper solution would have this information in a database somewhere.

class VehicleType(Enum):
    Car = 0
    Motorbike = 1
    Truck = 2
    Diplomat = 3
    Military = 4
    Foreign = 5
    Emergency = 6
    Police = 7