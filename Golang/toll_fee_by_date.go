package tollcalc

import (
	"evolvetech/tollcalc/vehicle"
	"time"
)

// getTollFeeHour return the toll fee of total hour in specific date of vehicle v.
// When vehicle type is not specified, it will
func getTollFeeHour(v vehicle.Vehicle, date time.Time) int {
	if isTollFreeVehicle(v) || isTollFreeDate(date) {
		return 0
	}

	hour := date.Hour()
	minute := date.Minute()

	switch {
	case hour == 6 && minute >= 0 && minute <= 29:
		return 8

	case hour == 6 && minute >= 30 && minute <= 59:
		return 13

	case hour == 7 && minute >= 0 && minute <= 59:
		return 18

	case hour == 8 && minute >= 0 && minute <= 29:
		return 13

	case hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59:
		return 8

	case hour == 15 && minute >= 0 && minute <= 29:
		return 13

	case hour == 15 && minute >= 0 || hour == 16 && minute <= 59:
		return 18

	case hour == 17 && minute >= 0 && minute <= 59:
		return 13

	case hour == 18 && minute >= 0 && minute <= 29:
		return 8
	}

	return 0
}
