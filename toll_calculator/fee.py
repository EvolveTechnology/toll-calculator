import datetime
from typing import Iterable
from typing import List
from typing import Sequence

from .date import is_toll_free_date
from .time import in_range
from .time import time_range_fees
from .vehicle import is_toll_free_vehicle
from .vehicle import Vehicle


def time_fee(time: datetime.datetime) -> int:
    """
    Usage:
    >>> time_fee(datetime.datetime(2019, 8, 15, 15, 31))
    18
    >>> time_fee(datetime.datetime(2019, 8, 17, 15, 31))
    0
    >>> time_fee(datetime.datetime(2019, 12, 31, 15, 31))
    0
    >>> time_fee(datetime.datetime(2019, 8, 13, 8, 29))
    13
    >>> time_fee(datetime.datetime(2019, 8, 15, 9, 12))
    8
    """
    if is_toll_free_date(time):
        return 0
    for time_range, fee in time_range_fees.items():
        if in_range(time_range, time):
            return fee
    return 0


def chunk_by_hour(
    passes: Sequence[datetime.datetime]
) -> Iterable[List[datetime.datetime]]:
    """
    Usage:
    >>> now = datetime.datetime.now()
    >>> then = now + datetime.timedelta(minutes=59)
    >>> chunk, = chunk_by_hour([now, then])
    >>> chunk[0] == now and chunk[1] == then
    True
    >>> then = now + datetime.timedelta(minutes=60)
    >>> chunk1, chunk2 = chunk_by_hour([now, then])
    >>> chunk1 == [now] and chunk2 == [then]
    True
    """
    chunk: List[datetime.datetime] = []
    for time in passes:
        if not chunk or time - chunk[0] < datetime.timedelta(hours=1):
            chunk.append(time)
            continue
        yield chunk
        chunk = [time]
    yield chunk


def validate_passes(passes: Sequence[datetime.datetime]) -> None:
    """
    Usage:
    >>> a = datetime.datetime(1984, 1, 2, 12, 1)
    >>> b = a + datetime.timedelta(minutes=1)
    >>> c = a + datetime.timedelta(days=2)
    >>> validate_passes([a, b])
    >>> validate_passes([b, a])
    Traceback (most recent call last):
      ...
    ValueError: passes must be sorted
    >>> validate_passes([a, c])
    Traceback (most recent call last):
      ...
    ValueError: passes must be within the same day
    """
    previous = passes[0]
    for current in passes[1:]:
        if previous > current:
            raise ValueError("passes must be sorted")
        if (
            current - previous > datetime.timedelta(days=1)
            or current.day != previous.day
        ):
            raise ValueError("passes must be within the same day")


def get_toll_fee(vehicle: Vehicle, passes: Sequence[datetime.datetime]) -> int:
    """
    Usage:
    >>> from toll_calculator.vehicle import Vehicle
    >>> car = Vehicle.car
    >>> passes = [
    ...     datetime.datetime(2019, 8, 15, 8, 0),
    ...     datetime.datetime(2019, 8, 15, 8, 31)]
    >>> get_toll_fee(car, passes)
    13
    >>> passes = [
    ...     datetime.datetime(2019, 8, 15, 8, 0),
    ...     datetime.datetime(2019, 8, 15, 9, 12)]
    >>> get_toll_fee(car, passes)
    21
    >>> passes = [
    ...     datetime.datetime(2019, 8, 15, 6, 0),
    ...     datetime.datetime(2019, 8, 15, 7, 0),
    ...     datetime.datetime(2019, 8, 15, 8, 0),
    ...     datetime.datetime(2019, 8, 15, 9, 0),
    ...     datetime.datetime(2019, 8, 15, 10, 0),
    ...     datetime.datetime(2019, 8, 15, 11, 0)]
    >>> get_toll_fee(car, passes)
    60
    >>> passes = [
    ...     datetime.datetime(2019, 8, 15, 7, 0),
    ...     datetime.datetime(2019, 8, 15, 6, 0)]
    >>> get_toll_fee(car, passes)
    Traceback (most recent call last):
      ...
    ValueError: passes must be sorted
    """
    validate_passes(passes)
    if is_toll_free_vehicle(vehicle):
        return 0
    # assuming passes are sorted and only occurs during one date
    fee = 0
    for chunk in chunk_by_hour(passes):
        fee += max(map(time_fee, chunk))
    return min(fee, 60)
