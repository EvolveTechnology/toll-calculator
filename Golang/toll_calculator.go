package tollcalc

import (
	"evolvetech/tollcalc/vehicle"
	"fmt"
	"time"
)

// ErrVehicleIsNil error type when vehicle type is nil
var ErrVehicleIsNil = fmt.Errorf("vehicle is nil, must pass argument which implements vehicle.Vehicle interface")

// GetTollFee will calculate the total toll fee for one day of a Vehicle v,
// within range of date and time of all passes on one day.
func GetTollFee(v vehicle.Vehicle, dates []time.Time) (totalFee int) {
	totalFee = 0

	if v == nil {
		panic(ErrVehicleIsNil)
	}

	if len(dates) <= 0 {
		return
	}

	dateStart := dates[0]
	tempFee := getTollFeeHour(v, dateStart)

	for _, date := range dates {

		nextFee := getTollFeeHour(v, date)
		duration := date.Sub(dateStart)

		if duration.Minutes() <= 60 {
			if totalFee > 0 {
				totalFee -= tempFee
			}

			if nextFee >= tempFee {
				tempFee = nextFee
			}

			totalFee += tempFee
			continue
		}

		totalFee += nextFee
	}

	if totalFee > 60 {
		totalFee = 60
		return
	}

	return
}
