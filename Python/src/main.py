from toll_config_facade import TollConfigFacade
from toll_handler import TollHandler
from vehicles import VehicleType
import pendulum

if __name__ == '__main__':
    tcf = TollConfigFacade()
    toll_fee_intervals = tcf.get_fee_intervals_from_remote(server_url="Gorkz Server")
    toll_free_dates = tcf.get_toll_free_dates(server_url="Morkz Server", year=2021)

    toll_handler = TollHandler([VehicleType.Emergency, VehicleType.Police],
                              toll_free_dates,
                              toll_fee_intervals,
                              )
    reg_nrs = ["ABC 123", "123 ABC", "ABC 123", "BCF 321", "POP 555"]

    for reg_nr in reg_nrs:
        toll_handler.calculate_fee(pendulum.datetime(2021,5,30,17,23), reg_nr)

    print(toll_handler.get_total_fee_per_vehicle())