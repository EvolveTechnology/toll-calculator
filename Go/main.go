package main

var pricesPerTimeOfDay = []TimetableSlot{
	{Begin: TimeOfDay{Hour: 6, Minute: 0}, End: TimeOfDay{Hour: 6, Minute: 29}, Price: 8},
	{Begin: TimeOfDay{Hour: 6, Minute: 30}, End: TimeOfDay{Hour: 6, Minute: 59}, Price: 13},
	{Begin: TimeOfDay{Hour: 7, Minute: 0}, End: TimeOfDay{Hour: 7, Minute: 59}, Price: 18},
	{Begin: TimeOfDay{Hour: 8, Minute: 0}, End: TimeOfDay{Hour: 8, Minute: 29}, Price: 13},
	//This is a purpsoseful deviation from the original implementation
	//Originally every first half an hour between 8am and 2pm no toll would be charged
	{Begin: TimeOfDay{Hour: 8, Minute: 30}, End: TimeOfDay{Hour: 14, Minute: 59}, Price: 8},
	{Begin: TimeOfDay{Hour: 15, Minute: 0}, End: TimeOfDay{Hour: 15, Minute: 29}, Price: 13},
	//Although originally functioning as supposed to, the interval wasn't immediately readable
	{Begin: TimeOfDay{Hour: 15, Minute: 30}, End: TimeOfDay{Hour: 16, Minute: 59}, Price: 18},
	{Begin: TimeOfDay{Hour: 17, Minute: 0}, End: TimeOfDay{Hour: 17, Minute: 59}, Price: 13},
	{Begin: TimeOfDay{Hour: 18, Minute: 0}, End: TimeOfDay{Hour: 18, Minute: 29}, Price: 8},
}

type DateTime struct {
	date       Date
	timesOfDay []TimeOfDay
}

func dropTimesWhereFeesDontApply(timesOfDay []TimeOfDay) (ret []TimeOfDay) {
	for i, time := range timesOfDay {
		if i == 0 || ret[len(ret)-1].AddHours(1).IsBeforeOrEquals(time) {
			ret = append(ret, time)
		}
	}
	return
}

func CalculateTollTotal(vehicle Vehicle, dateTime DateTime) (total int) {
	if IsTollFreeVehicle(vehicle) || dateTime.date.IsTollFreeDay() {
		return 0
	}

	filteredTimes := dropTimesWhereFeesDontApply(dateTime.timesOfDay)

	for _, timeOfDay := range filteredTimes {
		total += timeOfDay.GetTollPrice(pricesPerTimeOfDay)
	}

	return
}

func main() {}
