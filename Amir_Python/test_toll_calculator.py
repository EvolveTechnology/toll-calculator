#!/usr/bin/env python 

import unittest
from datetime import datetime

import toll_calculator
from vehicle import Vehicle

class TestTollCalculator(unittest.TestCase):

    def test_get_fee(self):
        self.assertEqual(toll_calculator.get_fee(datetime(2017, 9, 16, 13, 10)), 0) # Saturday
        self.assertEqual(toll_calculator.get_fee(datetime(2017, 9, 17, 13, 10)), 0) # Sunday
        self.assertEqual(toll_calculator.get_fee(datetime(2017, 5, 25, 13, 10)), 0) # Ascension Day
        self.assertEqual(toll_calculator.get_fee(datetime(2017, 9, 15, 0, 0)), 0) # weird time of the day
        self.assertEqual(toll_calculator.get_fee(datetime(2017, 9, 15, 6, 0)), 8)
        self.assertEqual(toll_calculator.get_fee(datetime(2017, 9, 15, 6, 30)), 13)
        self.assertEqual(toll_calculator.get_fee(datetime(2017, 9, 15, 6, 59)), 13)
        self.assertEqual(toll_calculator.get_fee(datetime(2017, 9, 15, 7, 00)), 18)
        self.assertEqual(toll_calculator.get_fee(datetime(2017, 9, 15, 7, 59)), 18)
        self.assertEqual(toll_calculator.get_fee(datetime(2017, 9, 15, 8, 00)), 13)
        self.assertEqual(toll_calculator.get_fee(datetime(2017, 9, 15, 8, 29)), 13)
        self.assertEqual(toll_calculator.get_fee(datetime(2017, 9, 15, 8, 30)), 8)
        self.assertEqual(toll_calculator.get_fee(datetime(2017, 9, 15, 9, 0)), 8)
        self.assertEqual(toll_calculator.get_fee(datetime(2017, 9, 15, 12, 00)), 8)
        self.assertEqual(toll_calculator.get_fee(datetime(2017, 9, 15, 14, 59)), 8)
        self.assertEqual(toll_calculator.get_fee(datetime(2017, 9, 15, 15, 00)), 13)
        self.assertEqual(toll_calculator.get_fee(datetime(2017, 9, 15, 15, 29)), 13)
        self.assertEqual(toll_calculator.get_fee(datetime(2017, 9, 15, 15, 30)), 18)
        self.assertEqual(toll_calculator.get_fee(datetime(2017, 9, 15, 16, 59)), 18)
        self.assertEqual(toll_calculator.get_fee(datetime(2017, 9, 15, 17, 00)), 13)
        self.assertEqual(toll_calculator.get_fee(datetime(2017, 9, 15, 17, 59)), 13)
        self.assertEqual(toll_calculator.get_fee(datetime(2017, 9, 15, 18, 00)), 8)
        self.assertEqual(toll_calculator.get_fee(datetime(2017, 9, 15, 18, 29)), 8)
        self.assertEqual(toll_calculator.get_fee(datetime(2017, 9, 15, 19, 00)), 0)

    def test_toll_free_vehicle(self):
        v = Vehicle('diplomat')
        toll = toll_calculator.calculate_toll(v, ["2017-09-15 16:59"])
        self.assertEqual(toll, 0)

    def test_regular_vehicle_single_pass_sunday(self):
        v = Vehicle('private')
        toll = toll_calculator.calculate_toll(v, ["2017-09-17 07:15"])
        self.assertEqual(toll, 0)

    def test_regular_vehicle_single_pass_rush_hour(self):
        v = Vehicle('private')
        toll = toll_calculator.calculate_toll(v, ["2017-09-15 07:15"])
        self.assertEqual(toll, 18)

    def test_regular_vehicle_multiple_pass(self):
        v = Vehicle('private')
        dates = ["2017-09-15 07:15", "2017-09-15 08:30", "2017-09-15 18:15", "2017-09-15 20:15"]
        toll = toll_calculator.calculate_toll(v, dates)
        self.assertEqual(toll, 34)

    def test_regular_vehicle_multiple_pass_reached_max(self):
        v = Vehicle('private')
        dates = ["2017-09-15 07:15", "2017-09-15 08:30", "2017-09-15 15:30", "2017-09-15 16:45"]
        toll = toll_calculator.calculate_toll(v, dates)
        self.assertEqual(toll, 60)

    def test_regular_vehicle_multiple_pass_different_days(self):
        v = Vehicle('private')
        dates = ["2017-09-15 07:15", "2017-09-16 07:45"]
        with self.assertRaises(toll_calculator.DifferentDaysException):
            toll_calculator.calculate_toll(v, dates)

    def test_regular_vehicle_cant_pass_twice_same_time(self):
        v = Vehicle('private')
        dates = ["2017-09-15 07:15", "2017-09-15 07:15", "2017-09-15 07:45"]
        with self.assertRaises(toll_calculator.PassedTollTwiceSameTimeException):
            toll_calculator.calculate_toll(v, dates)

    def test_regular_vehicle_multiple_pass_within_an_hour(self):
        v = Vehicle('private')
        dates = ["2017-09-15 07:15", "2017-09-15 07:30", "2017-09-15 07:45"]
        toll = toll_calculator.calculate_toll(v, dates)
        self.assertEqual(toll, 18)

    def test_regular_vehicle_multiple_pass_within_an_hour_not_first_interval(self):
        v = Vehicle('private')
        dates = ["2017-09-15 07:15", "2017-09-15 08:30", "2017-09-15 08:45"]
        toll = toll_calculator.calculate_toll(v, dates)
        self.assertEqual(toll, 26)

    def test_regular_vehicle_multiple_pass_within_one_hour_change_rush_hour_edge_case(self):
        v = Vehicle('private')
        dates = ["2017-09-15 07:15", "2017-09-15 08:15", "2017-09-15 18:15", "2017-09-15 20:15"]
        toll = toll_calculator.calculate_toll(v, dates)
        self.assertEqual(toll, 26)


if __name__ == '__main__':
    unittest.main()
