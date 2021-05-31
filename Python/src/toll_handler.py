from datetime import time
import pendulum
import random
from toll_config_facade import TollConfigFacade
from vehicles import VehicleType

class TollHandler:

    def __init__(self,
                 toll_free_vehicles: list,
                 toll_free_dates: list,
                 fee_intervals: list,
                 charge_limit: int = 3600,
                 ):
        self.toll_free_vehicles = toll_free_vehicles
        self.toll_free_dates = toll_free_dates
        self.fee_intervals = fee_intervals
        self.charge_limit = charge_limit
        self.handled_vehicles = dict() # In real life this would be a redis cache or something.

    def _get_fee_from_timestamp(self, timestamp: pendulum.datetime) -> int:
        fees = list()
        for t in self.fee_intervals:
            if timestamp.time() > t['start_time'] and timestamp.time() < t['end_time']:
                fees.append(t['fee'])
        if fees:
            return max(fees)
        else:
            return 0

    def _lookup_vehicle_info(self, reg_nr: str):
        """
        Randomize a vehicle, since I don't intend to create a vehicle database for this.
        """
        return {
            'type': random.choice(list(VehicleType)),
            'entries': [],
            'reg_nr': reg_nr
        }

    def calculate_fee(self, entry_time: pendulum.datetime, vehicle_reg_nr: str) -> bool:
        """
        Determine the fee, if any, for a given vehicle at a given point in time. I imagine a toll camera
        would only pick up the plate nr of a vehicle, so we first do a lookup to find out the vehicle information
        and then decide what to do about the fee.
        """
        # Get the vehicle from our key-value store (which should REALLY be a redis cache or the like)
        # If we don't have it yet, do a lookup and store it.
        try:
            vehicle = self.handled_vehicles[vehicle_reg_nr]
        except KeyError:
            vehicle = self._lookup_vehicle_info(vehicle_reg_nr)
        # First check if we even should collect a fee
        if entry_time.date() in self.toll_free_dates or vehicle["type"] in self.toll_free_vehicles:
            return False
        # Get the fee that might need to be paid
        fee = self._get_fee_from_timestamp(entry_time)
        if not fee:
            return False
        # Next we get the latest (if any) entry time and fee for our vehicle, to determine if we need
        # to bump the fee up
        try:
            last_entry = vehicle['entries'][-1]
            if (entry_time - last_entry['timestamp']).seconds < self.charge_limit and fee > last_entry['fee']:
                vehicle['entries'][-1]['fee'] = fee
            elif (entry_time - last_entry['timestamp']).seconds < self.charge_limit:
                pass
            else:
                vehicle['entries'].append({'timestamp': entry_time, 'fee': fee})
        except IndexError:
            vehicle['entries'].append({'timestamp': entry_time, 'fee': fee})

        # Finally we add the vehicle to our handled vehicle store
        self.handled_vehicles[vehicle_reg_nr] = vehicle
        return True

    def get_total_fee_per_vehicle(self) -> list:
        result = list()
        for v, k in self.handled_vehicles.items():
            total_fee = sum([x['fee'] for x in k['entries']])
            # Maximum fee per day is 60.
            result.append((v, min(60, total_fee)))
        return result