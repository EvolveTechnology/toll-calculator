from datetime import tzinfo
import pytest
import os
import sys
import pendulum

module_dir = os.path.dirname(__file__)
sys.path.append(os.path.join(module_dir, '../src'))

from toll_handler import TollHandler
from vehicles import VehicleType

@pytest.fixture()
def toll_handler():

    free_vehicles = [VehicleType.Emergency, VehicleType.Police]
    free_dates = [pendulum.date(2021,1,1)]
    fee_intervals = [{'start_time': pendulum.time(6,0,0), 'end_time': pendulum.time(6,29), 'fee': 8},
                     {'start_time': pendulum.time(6,30,0), 'end_time': pendulum.time(6,59), 'fee': 13},
                     {'start_time': pendulum.time(7,0,0), 'end_time': pendulum.time(7,59), 'fee': 18},
                     {'start_time': pendulum.time(8,30,0), 'end_time': pendulum.time(14,59), 'fee': 8},
                     {'start_time': pendulum.time(12,0,0), 'end_time': pendulum.time(13,0), 'fee': 18},
                     {'start_time': pendulum.time(15,0,0), 'end_time': pendulum.time(15,29), 'fee': 13},
                     {'start_time': pendulum.time(15,30,0), 'end_time': pendulum.time(16,59), 'fee': 18},
                     {'start_time': pendulum.time(17,0,0), 'end_time': pendulum.time(17,59), 'fee': 13},
                     {'start_time': pendulum.time(18,0,0), 'end_time': pendulum.time(18,29), 'fee': 8}]

    th = TollHandler(free_vehicles, free_dates, fee_intervals)
    return th

def test_get_fee_from_timestamp(toll_handler):
    normal_fee = toll_handler._get_fee_from_timestamp(pendulum.datetime(2021,5,31,15,14))
    assert normal_fee == 13
    overlapping_max_fee = toll_handler._get_fee_from_timestamp(pendulum.datetime(2021,5,31,12,14))
    assert overlapping_max_fee == 18
    no_fee = toll_handler._get_fee_from_timestamp(pendulum.datetime(2021,5,31,21,34))
    assert no_fee == 0

def test_lookup_vehicle_info(toll_handler):
    """
    Since we don't do any actual looking up we'll just ignore this test case
    """
    pass

def test_calculate_fee_normal_fee(toll_handler):
    toll_handler.handled_vehicles = {'ABC 123': {'type': VehicleType.Motorbike, 'entries': []}}
    expected_result = {'ABC 123': {'type': VehicleType.Motorbike,
                                   'entries': [{'timestamp': pendulum.datetime(2021, 5, 31, 17, 23, 0, tz='UTC'), 'fee': 13}]
                                  }
                      }
    toll_handler.calculate_fee(pendulum.datetime(2021, 5, 31, 17, 23, 0), "ABC 123")
    assert toll_handler.handled_vehicles == expected_result


def test_calculate_fee_free_day(toll_handler):
    toll_handler.handled_vehicles = {'ABC 123': {'type': VehicleType.Motorbike, 'entries': []}}
    expected_result = {'ABC 123': {'type': VehicleType.Motorbike,
                                   'entries': []
                                  }
                      }
    toll_handler.calculate_fee(pendulum.datetime(2021, 1, 1, 17, 23, 0), "ABC 123")
    assert toll_handler.handled_vehicles == expected_result

def test_calculate_fee_free_vehicle(toll_handler):
    toll_handler.handled_vehicles = {'ABC 123': {'type': VehicleType.Police, 'entries': []}}
    expected_result = {'ABC 123': {'type': VehicleType.Police,
                                   'entries': []
                                  }
                      }
    toll_handler.calculate_fee(pendulum.datetime(2021, 1, 1, 17, 23, 0), "ABC 123")
    assert toll_handler.handled_vehicles == expected_result

def test_calculate_fee_update_overlapping_fees(toll_handler):
    toll_handler.handled_vehicles = {'ABC 123': {'type': VehicleType.Motorbike, 
                                     'entries': [{'timestamp': pendulum.datetime(2021, 5, 31, 11, 45, 0, tz='UTC'), 'fee': 8}]}}
    expected_result = {'ABC 123': {'type': VehicleType.Motorbike,
                                   'entries': [{'timestamp': pendulum.datetime(2021, 5, 31, 11, 45, 0, tz='UTC'), 'fee': 18}]
                                  }
                      }
    toll_handler.calculate_fee(pendulum.datetime(2021, 5, 31, 12, 23, 0), "ABC 123")
    assert toll_handler.handled_vehicles == expected_result

def test_calculate_fee_within_charge_limit(toll_handler):
    toll_handler.handled_vehicles = {'ABC 123': {'type': VehicleType.Motorbike,
                                 'entries': [{'timestamp': pendulum.datetime(2021, 5, 31, 11, 45, 0, tz='UTC'), 'fee': 8}]}}
    expected_result = {'ABC 123': {'type': VehicleType.Motorbike,
                                   'entries': [{'timestamp': pendulum.datetime(2021, 5, 31, 11, 45, 0, tz='UTC'), 'fee': 8}]
                                  }
                      }
    toll_handler.calculate_fee(pendulum.datetime(2021, 5, 31, 11, 50, 0), "ABC 123")
    assert toll_handler.handled_vehicles == expected_result

def test_get_total_fee_per_vehicle(toll_handler):
    toll_handler.handled_vehicles = {'ABC 123': {'type': VehicleType.Motorbike,
                                     'entries': [{'timestamp': pendulum.datetime(2021, 5, 31, 11, 45, 0, tz='UTC'), 'fee': 8},
                                                {'timestamp': pendulum.datetime(2021, 5, 31, 12, 45, 0, tz='UTC'), 'fee': 18}]},
                                     'CDE 321': {'type': VehicleType.Motorbike,
                                     'entries': [{'timestamp': pendulum.datetime(2021, 5, 31, 11, 45, 0, tz='UTC'), 'fee': 8},
                                                {'timestamp': pendulum.datetime(2021, 5, 31, 12, 45, 0, tz='UTC'), 'fee': 13}]}
                                    }
    expected_result = [('ABC 123', 26), ('CDE 321', 21)]
    assert toll_handler.get_total_fee_per_vehicle() == expected_result

def test_get_total_fee_per_vehicle_max_60(toll_handler):
    toll_handler.handled_vehicles = {'ABC 123': {'type': VehicleType.Motorbike,
                                     'entries': [{'timestamp': pendulum.datetime(2021, 5, 31, 7, 45, 0, tz='UTC'), 'fee': 18},
                                                {'timestamp': pendulum.datetime(2021, 5, 31, 11, 45, 0, tz='UTC'), 'fee': 8},
                                                {'timestamp': pendulum.datetime(2021, 5, 31, 12, 45, 0, tz='UTC'), 'fee': 18},
                                                {'timestamp': pendulum.datetime(2021, 5, 31, 15, 30, 0, tz='UTC'), 'fee': 18},
                                                {'timestamp': pendulum.datetime(2021, 5, 31, 16, 45, 0, tz='UTC'), 'fee': 18}]}
                                    }
    expected_result = [('ABC 123', 60)]
    assert toll_handler.get_total_fee_per_vehicle() == expected_result
