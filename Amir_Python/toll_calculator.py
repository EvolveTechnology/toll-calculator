from datetime import datetime, timedelta

from workalendar.europe import Sweden
from toll_constants import RUSH_HOUR_FEE_MAP, TOLL_FREE_VEHICLES, MAX_FEE


class DifferentDaysException(Exception):
    pass


class PassedTollTwiceSameTimeException(Exception):
    pass


def calculate_toll(vehicle, string_dates):
    total_fee = 0
    if is_toll_free(vehicle.get_type()):
        return total_fee

    dates = [datetime.strptime(d, "%Y-%m-%d %H:%M") for d in string_dates]
    validate_dates(dates)

    for date in dates:
        if (date > timedelta(hours=1) + vehicle.get_last_payment()):
            total_fee += get_fee(date)
            vehicle.set_last_payment(date)
        else:
            vehicle.set_last_payment(vehicle.get_last_payment())

    return min(total_fee, MAX_FEE)


def is_holiday(date):
    cal = Sweden()
    return not cal.is_working_day(date)


def is_toll_free(vehicle_type):
    return vehicle_type.upper() in TOLL_FREE_VEHICLES


def get_fee(date):
    if is_holiday(date):
        return 0

    passing_time = (date.hour, date.minute)
    for k, v in RUSH_HOUR_FEE_MAP.iteritems():
        if k[0] <= passing_time <= k[1]:
            return v
    return 0


def validate_dates(dates):
    _different_days(dates)
    _duplicate_dates(dates)


def _different_days(dates):
    max_interval =  max(dates) - min(dates)
    if max_interval > timedelta(days=1):
        raise DifferentDaysException('The vehicle passed on different days!')


def _duplicate_dates(dates):
    seen = set()
    for date in dates:
        if date in seen:
            raise PassedTollTwiceSameTimeException('{} is duplicated!'.format(date))
        seen.add(date)
