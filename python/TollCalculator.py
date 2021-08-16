from datetime import datetime, date
from TollDate import TollDate
from Vehicles import Vehicle


class TollCalculator:
    def __init__(self, vehicle: Vehicle, dates: list):
        self.vehicle = vehicle
        self.dates = dates

    def calculate_toll(self) -> int:
        # variables needed to calculate fees
        total_fee: int = 0
        temp_fee: int = 0
        day_fee: int = 0

        # First check if vehicle is tollable
        if not self.vehicle.is_vehicle_tollable():
            return total_fee

        # Sort dates so I can use for loop and removes duplicates.
        dates: list = self.organize_dates_list(self.dates)

        # Initialize start interval and current date so we can calculate on period and day.
        start_interval: TollDate = TollDate(dates[0])
        current_date = start_interval.date_time.date()

        for date in dates:
            toll_date: TollDate = TollDate(date)

            # Do nothing if it's a holiday, saturday or sunday
            if not toll_date.is_tollable_date():
                pass

            # If same day check if day fee is 60 else check if it is in an hour interval.
            elif self.same_day(current_date, toll_date.date_time.date()):

                # If same day set a temp day fee to initial day fee.
                temp_day_fee: int = day_fee
                if day_fee >= 60:
                    pass

                # If same period check if temp toll is lower then the new toll, if current fee is greater remove old fee and add new.
                elif self.same_period(start_interval.date_time, toll_date.date_time):
                    if toll_date.get_toll_fee() > temp_fee:
                        day_fee = day_fee + toll_date.get_toll_fee() - temp_fee

                        # Do a new check if day fee is over 60
                        if day_fee < 60:
                            total_fee = total_fee + toll_date.get_toll_fee() - temp_fee
                            temp_fee = toll_date.get_toll_fee()
                        else:
                            print(temp_day_fee)
                            total_fee = total_fee + (60 - temp_day_fee)

                # If not in same interval start a new interval and calculate toll fee.
                else:
                    start_interval = toll_date
                    day_fee = day_fee + toll_date.get_toll_fee()

                    # Check if day fee is over 60
                    if day_fee < 60:
                        total_fee = total_fee + toll_date.get_toll_fee()
                        temp_fee = toll_date.get_toll_fee()
                    else:
                        total_fee = total_fee + (60 - temp_day_fee)

            # If new day reset all calculations but total fee and start over.
            else:
                temp_fee = 0
                day_fee = 0
                current_date = toll_date.date_time.date()
                start_interval = toll_date

                day_fee = toll_date.get_toll_fee()
                total_fee = total_fee + toll_date.get_toll_fee()

        return total_fee

    def organize_dates_list(self, dates: list) -> list:
        return sorted(list(dict.fromkeys(dates)))

    def same_period(self, start_time: datetime, end_time: datetime) -> bool:
        minutes_diff = (end_time - start_time).total_seconds() / 60.0
        if minutes_diff < 60:
            return True
        return False

    def same_day(self, start_date: date, end_date: date) -> bool:
        if start_date == end_date:
            return True
        return False
