from datetime import datetime

class Vehicle(object):

    def __init__(self, vehicle_type):
        self._vehicle_type = vehicle_type
        self._last_payment = datetime(1,1,1,0,0)

    def get_type(self):
        return self._vehicle_type

    def set_last_payment(self, date):
        self._last_payment = date

    def get_last_payment(self):
        return self._last_payment
