import datetime
from typing import Dict
from typing import NamedTuple

from typing_extensions import Final


class Time(NamedTuple):
    hour: int
    minute: int


class TimeRange(NamedTuple):
    start: Time
    end: Time


time_range_fees: Final[Dict[TimeRange, int]] = {
    TimeRange(Time(6, 0), Time(6, 29)): 8,
    TimeRange(Time(6, 30), Time(6, 59)): 13,
    TimeRange(Time(7, 0), Time(7, 59)): 18,
    TimeRange(Time(8, 0), Time(8, 29)): 13,
    TimeRange(Time(8, 30), Time(14, 59)): 8,  # todo: document fixed bug
    TimeRange(Time(15, 0), Time(15, 29)): 13,
    TimeRange(Time(15, 30), Time(16, 59)): 18,
    TimeRange(Time(17, 0), Time(17, 59)): 13,
    TimeRange(Time(18, 0), Time(18, 29)): 8,
}


def in_range(time_range: TimeRange, time: datetime.datetime) -> bool:
    """
    Usage:
    >>> tr = TimeRange(Time(6, 0), Time(6, 29))
    >>> in_range(tr, datetime.datetime.now().replace(hour=6, minute=1))
    True
    >>> tr = TimeRange(Time(6, 0), Time(6, 29))
    >>> in_range(tr, datetime.datetime.now().replace(hour=6, minute=30))
    False
    >>> tr = TimeRange(Time(6, 0), Time(6, 29))
    >>> in_range(tr, datetime.datetime.now().replace(hour=5, minute=59))
    False
    >>> tr = TimeRange(Time(8, 30), Time(14, 59))
    >>> in_range(tr, datetime.datetime(2019, 8, 15, 9, 12))
    True
    """
    if time.hour == time_range.start.hour and time.minute < time_range.start.minute:
        return False
    if time.hour == time_range.end.hour and time.minute > time_range.end.minute:
        return False
    return time_range.start.hour <= time.hour <= time_range.end.hour
