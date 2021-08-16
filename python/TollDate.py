from datetime import datetime, date
import holidays


class TollDate:
    def __init__(self, date_time: datetime):
        self.date_time = date_time

    def is_tollable_date(self) -> bool:
        # Adds all Swedish holidays to a list.
        se_holidays = [
            item[0] for item in holidays.Sweden(years=self.date_time.year).items()
        ]

        if self.date_time.weekday() == 5 or self.date_time.weekday() == 6:
            return False

        elif self.date_time.date() in se_holidays:
            return False

        return True

    def get_toll_fee(self) -> int:
        if not self.is_tollable_date():
            return 0

        hour: int = self.date_time.hour
        minute: int = self.date_time.minute

        if (
            (hour == 6 and minute >= 0 and minute <= 29)
            or (
                (hour >= 8 and minute >= 30 and hour < 9)
                or (hour >= 9 and hour <= 14 and minute <= 59)
            )
            or (hour == 18 and minute >= 0 and minute <= 29)
        ):
            return 8
        elif (
            (hour == 6 and minute >= 30 and minute <= 59)
            or (hour == 8 and minute >= 0 and minute <= 29)
            or (hour == 15 and minute >= 0 and minute <= 29)
            or (hour == 17 and minute >= 0 and minute <= 59)
        ):
            return 13
        elif (hour == 7 and minute >= 0 and minute <= 59) or (
            (hour == 15 and minute >= 30) or (hour == 16 and minute <= 59)
        ):
            return 18

        else:
            return 0
