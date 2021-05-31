import pendulum
import holidays

class TollConfigFacade:
    
    def __init__(self):
        pass

    def get_fee_intervals_from_remote(self, server_url: str) -> list:
        """
        In real life, this would query some central server to get the time intervals
        for the various fees, probably only once per day and then store the result in a cache or
        the like. If fees are likely to change often during a day, this function would probably
        just fetch the current fee given a timestamp. Since we don't have anything to fetch in this exercise
        we'll just return a list of hard-coded values.
        """

        intervals = [{'start_time': pendulum.time(6,0,0), 'end_time': pendulum.time(6,29), 'fee': 8},
                     {'start_time': pendulum.time(6,30,0), 'end_time': pendulum.time(6,59), 'fee': 13},
                     {'start_time': pendulum.time(7,0,0), 'end_time': pendulum.time(7,59), 'fee': 18},
                     {'start_time': pendulum.time(8,30,0), 'end_time': pendulum.time(14,59), 'fee': 8},
                     {'start_time': pendulum.time(12,0,0), 'end_time': pendulum.time(13,0), 'fee': 18},
                     {'start_time': pendulum.time(15,0,0), 'end_time': pendulum.time(15,29), 'fee': 13},
                     {'start_time': pendulum.time(15,30,0), 'end_time': pendulum.time(16,59), 'fee': 18},
                     {'start_time': pendulum.time(17,0,0), 'end_time': pendulum.time(17,59), 'fee': 13},
                     {'start_time': pendulum.time(18,0,0), 'end_time': pendulum.time(18,29), 'fee': 8}]
        return intervals

    def get_toll_free_dates(self, server_url: str, year: pendulum.datetime) -> list:
        """
        Return a list of dates for the current year that are toll free. This includes all bank holidays and weekends as
        well as all days before a bank holiday or weekend. Again, this should really be fetched from a server.
        """
        holiday_list = [date for date,name in sorted(holidays.Sweden(years=year).items())]
        # Need to add the days before each date as well.
        toll_free_dates = list()
        for d in holiday_list:
            # Convert the datetime instances to pendulum for ease of use.
            toll_free_dates.append(pendulum.datetime(d.year, d.month, d.day).subtract(days=1).date())
            toll_free_dates.append(pendulum.datetime(d.year, d.month, d.day))
        return toll_free_dates