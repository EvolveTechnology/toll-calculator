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
from TollDate import TollDate
from TollCalculator import TollCalculator
from datetime import datetime


def test_Should_Create_Vehicle_With_The_Right_Type():
    car = Car('abc123')
    motorbike = Motorbike()
    tractor = Tractor()
    emergency = Emergency()
    diplomat = Diplomat()
    foreign = Foreign()
    military = Military()

    assert (
        car.get_vehicle_type() == "car"
        and motorbike.get_vehicle_type() == "motorbike"
        and tractor.get_vehicle_type() == "tractor"
        and emergency.get_vehicle_type() == "emergency"
        and diplomat.get_vehicle_type() == "diplomat"
        and foreign.get_vehicle_type() == "foreign"
        and military.get_vehicle_type() == "military"
    )


def test_All_Not_Tollable_Vehicles_Should_Not_Be_Tollable():
    motorbike = Motorbike()
    military = Military()
    foreign = Foreign()
    diplomat = Diplomat()
    emergency = Emergency()
    tractor = Tractor()

    assert (
        not motorbike.is_vehicle_tollable()
        and not military.is_vehicle_tollable()
        and not foreign.is_vehicle_tollable()
        and not diplomat.is_vehicle_tollable()
        and not emergency.is_vehicle_tollable()
        and not tractor.is_vehicle_tollable()
    )


def test_Vehicle_Type_Car_Should_Be_Tollable():
    car = Car('abc123')

    assert car.is_vehicle_tollable() == True


def test_All_Time_Stamps_Should_Generate_Right_Amount_Of_Toll():
    between_six_and_six_thirty = TollDate(datetime(2021, 8, 16, 6, 15, 00, 00))
    between_eight_thirty_and_fourteen_fiftynine = TollDate(
        datetime(2021, 8, 16, 10, 15, 00, 00)
    )
    between_eighteen_and_eighteen_twentynine = TollDate(
        datetime(2021, 8, 16, 18, 15, 00, 00)
    )
    between_six_thirty_and_six_fiftynine = TollDate(
        datetime(2021, 8, 16, 6, 45, 00, 00)
    )
    between_eight_and_eight_twentynine = TollDate(datetime(2021, 8, 16, 8, 15, 00, 00))
    between_fifteen_and_fifteen_twentynine = TollDate(
        datetime(2021, 8, 16, 15, 15, 00, 00)
    )
    between_seventeen_and_seventeen_fiftynine = TollDate(
        datetime(2021, 8, 16, 17, 35, 00, 00)
    )
    between_seven_and_seven_fiftynine = TollDate(datetime(2021, 8, 16, 7, 30, 00, 00))
    between_fifteen_thirty_and_sixteen_fiftynine = TollDate(
        datetime(2021, 8, 16, 15, 45, 00, 00)
    )
    outside_of_toll_schema = TollDate(datetime(2021, 8, 16, 3, 00, 00, 00))
    saturday = TollDate(datetime(2021, 8, 14, 3, 00, 00, 00))
    sunday = TollDate(datetime(2021, 8, 15, 3, 00, 00, 00))
    holiday_date = TollDate(datetime(2021, 6, 25, 3, 00, 00, 00))

    assert (
        between_six_and_six_thirty.get_toll_fee() == 8
        and between_eight_thirty_and_fourteen_fiftynine.get_toll_fee() == 8
        and between_eighteen_and_eighteen_twentynine.get_toll_fee() == 8
        and between_six_thirty_and_six_fiftynine.get_toll_fee() == 13
        and between_eight_and_eight_twentynine.get_toll_fee() == 13
        and between_fifteen_and_fifteen_twentynine.get_toll_fee() == 13
        and between_seventeen_and_seventeen_fiftynine.get_toll_fee() == 13
        and between_seven_and_seven_fiftynine.get_toll_fee() == 18
        and between_fifteen_thirty_and_sixteen_fiftynine.get_toll_fee() == 18
        and outside_of_toll_schema.get_toll_fee() == 0
        and saturday.get_toll_fee() == 0
        and sunday.get_toll_fee() == 0
        and holiday_date.get_toll_fee() == 0
    )


def test_Should_Pick_Highest_Toll_Fee_In_Same_Hour_Period():
    car = Car('abc123')
    dates = [
        datetime(2021, 8, 16, 6, 15, 00, 00),
        datetime(2021, 8, 16, 6, 45, 00, 00),
        datetime(2021, 8, 16, 7, 10, 00, 00),
    ]

    toll_calculator = TollCalculator(car, dates)

    assert toll_calculator.calculate_toll() == 18


def test_Should_Only_Charge_Max_Sixty_For_One_Day():
    car = Car('abc123')
    dates = [
        datetime(2021, 8, 16, 6, 15, 00, 00),
        datetime(2021, 8, 16, 6, 45, 00, 00),
        datetime(2021, 8, 16, 7, 10, 00, 00),
        datetime(2021, 8, 16, 8, 15, 00, 00),
        datetime(2021, 8, 16, 10, 45, 00, 00),
        datetime(2021, 8, 16, 16, 10, 00, 00),
        datetime(2021, 8, 16, 17, 30, 00, 00),
    ]

    toll_calculator = TollCalculator(car, dates)

    assert toll_calculator.calculate_toll() == 60
