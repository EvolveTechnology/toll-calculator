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
from TollCalculator import TollCalculator
from datetime import datetime
from input_config import create_vehicle, unique_vehicles_from_license_plate, all_stamps_for_vehicle


def main():

    toll_stamps = [
        {
            'vehicle': 'car',
            'license_plate': 'abc123',
            'date': datetime(2021, 1, 5, 6, 15, 00, 00)
        },
        {
            'vehicle': 'car',
            'license_plate': 'abc123',
            'date': datetime(2021, 1, 5, 6, 45, 00, 00)
        },
        {
            'vehicle': 'car',
            'license_plate': 'abc123',
            'date': datetime(2021, 1, 5, 7, 55, 00, 00)
        },
        {
            'vehicle': 'car',
            'license_plate': 'abc123',
            'date': datetime(2021, 1, 5, 8, 30, 00, 00)
        },
        {
            'vehicle': 'car',
            'license_plate': 'abc123',
            'date': datetime(2021, 1, 5, 11, 45, 00, 00)
        },
        {
            'vehicle': 'car',
            'license_plate': 'abc123',
            'date': datetime(2021, 1, 5, 18, 13, 00, 00)
        },
        {
            'vehicle': 'car',
            'license_plate': 'abc723',
            'date': datetime(2021, 1, 5, 17, 45, 00, 00)
        },
        {
            'vehicle': 'car',
            'license_plate': 'abc323',
            'date': datetime(2021, 1, 5, 13, 55, 00, 00)
        },
        {
            'vehicle': 'car',
            'license_plate': 'abc423',
            'date': datetime(2021, 1, 5, 8, 30, 00, 00)
        },
        {
            'vehicle': 'car',
            'license_plate': 'abc423',
            'date': datetime(2021, 1, 5, 15, 45, 00, 00)
        },
        {
            'vehicle': 'emergency',
            'license_plate': 'abs123',
            'date': datetime(2021, 1, 5, 18, 13, 00, 00)
        },
        {
            'vehicle': 'diplomat',
            'license_plate': 'ab2723',
            'date': datetime(2021, 1, 5, 17, 45, 00, 00)
        },
        {
            'vehicle': 'motorbike',
            'license_plate': '',
            'date': datetime(2021, 1, 5, 13, 55, 00, 00)
        },
        {
            'vehicle': 'car',
            'license_plate': 'abc423',
            'date': datetime(2021, 1, 5, 8, 30, 00, 00)
        },
        {
            'vehicle': 'car',
            'license_plate': 'abc423',
            'date': datetime(2021, 1, 5, 15, 45, 00, 00)
        },
    ]

    identified_vehicles = unique_vehicles_from_license_plate(toll_stamps)

    for vehicle in identified_vehicles:
        v = create_vehicle(vehicle)
        toll_calculator = TollCalculator(v, vehicle['dates'])
        toll = toll_calculator.calculate_toll()
        
        if v.is_vehicle_tollable():
            print(f'Toll for vehicle {v.license_plate}, ({v.get_vehicle_type()}), is: {toll} SEK.')
        else:
            print(f'Vehicle is an {v.get_vehicle_type()} vehicle and is therefor not tollable.')


if __name__ == "__main__":
    main()
