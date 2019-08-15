import datetime

from typing_extensions import Final

toll_free_weekdays: Final = (6, 7)
toll_free_months: Final = (7,)
toll_free_dates: Final = (
    (1, 1),
    (3, 28),
    (3, 29),
    (4, 1),
    (4, 30),
    (5, 1),
    (5, 8),
    (5, 9),
    (6, 5),
    (6, 6),
    (6, 21),
    (11, 1),
    (12, 24),
    (12, 25),
    (12, 26),
    (12, 31),
)


def is_toll_free_date(date: datetime.date) -> bool:
    """
    Usage:
    >>> is_toll_free_date(datetime.date(2011, 6, 6))
    True
    >>> is_toll_free_date(datetime.date(2011, 6, 7))
    False
    >>> is_toll_free_date(datetime.date(2019, 7, 16))
    True
    """
    return (
        date.isoweekday() in toll_free_weekdays
        or date.month in toll_free_months
        or (date.month, date.day) in toll_free_dates
    )
