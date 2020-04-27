package tollcalc

import (
	"time"
)

// isTollFreeDate check whether in `date` the toll fee is free or not.
// In Saturday and Sunday, toll fee is free (return true).
// In year 2013, in some months and dates, the toll fee is also free.
func isTollFreeDate(date time.Time) bool {
	weekDay := date.Weekday()
	day := date.Day()
	month := date.Month()
	year := date.Year()

	if weekDay == time.Saturday || weekDay == time.Sunday {
		return true
	}

	if year != 2013 {
		return false
	}

	switch {
	case month == 1 && day == 1:
		return true

	case month == 3 && (day == 28 || day == 29):
		return true

	case month == 4 && (day == 1 || day == 30):
		return true

	case month == 5 && (day == 1 || day == 8 || day == 9):
		return true

	case month == 6 && (day == 5 || day == 6 || day == 21):
		return true

	case month == 7:
		return true

	case month == 11 && day == 1:
		return true

	case month == 12 && (day == 24 || day == 25 || day == 26 || day == 31):
		return true
	}

	return false
}
