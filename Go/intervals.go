package main

import "time"

type TimeOfDay struct {
	Hour   int
	Minute int
}

type TimetableSlot struct {
	Begin TimeOfDay
	End   TimeOfDay
	Price int
}

func (timeOfDay TimeOfDay) toTime() time.Time {
	return time.Date(0, 0, 0, timeOfDay.Hour, timeOfDay.Minute, 0, 0, time.UTC)
}

func (timeOfDay TimeOfDay) AddHours(number int) TimeOfDay {
	return TimeOfDay{timeOfDay.Hour + 1, timeOfDay.Minute}
}

func (timeOfDay TimeOfDay) IsBeforeOrEquals(time TimeOfDay) bool {
	return timeOfDay.toTime().Before(time.toTime()) || timeOfDay.toTime().Equal(time.toTime())
}

func (timeOfDay TimeOfDay) GetTollPrice(pricesPerTimeOfDay []TimetableSlot) int {
	for _, v := range pricesPerTimeOfDay {
		time := timeOfDay.toTime()
		periodStart := v.Begin.toTime()
		periodEnd := v.End.toTime()

		if (time.After(periodStart) || time.Equal(periodStart)) && (time.Before(periodEnd) || time.Equal(periodEnd)) {
			return v.Price
		}
	}

	return 0
}
