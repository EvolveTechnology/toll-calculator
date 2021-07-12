package main

import "time"

func (date Date) IsTollFreeDay() bool {
	return date.isWeekend() || date.isFixedDateHoliday() || date.isSwedishMidsummer() || date.isEasterRelatedHoliday()
}

type Date struct {
	Year  int
	Month int
	Day   int
}

func (date Date) toTime() time.Time {
	return time.Date(date.Year, time.Month(date.Month), date.Day, 0, 0, 0, 0, time.UTC)
}

func (date Date) addDays(number int) Date {
	return Date{date.Year, date.Month, date.Day + number}
}

func containsDate(dates []struct {
	name string
	date Date
}, date Date) bool {
	for _, v := range dates {
		if v.date == date {
			return true
		}
	}
	return false
}

func (date Date) isWeekend() bool {
	weekday := date.toTime().Weekday()
	return weekday == time.Saturday || weekday == time.Sunday
}

func calculateEaster(year int) Date {
	a := year % 19
	b := year / 100
	c := year % 100
	d := b / 4
	e := b % 4
	f := (8*b + 13) / 25
	g := (19*a + b - d - f + 15) % 30
	h := (a + 11*g) / 319
	i := c / 4
	j := c % 4
	k := (2*e + 2*i - j - g + h + 32) % 7
	l := (g - h + k + 90) / 25
	m := (g - h + k + l + 19) % 32

	return Date{Year: year, Month: l, Day: m}
}

func (date Date) isEasterRelatedHoliday() bool {
	easter := calculateEaster(date.Year)

	holidays := []struct {
		name string
		date Date
	}{
		{"Easter Sunday", easter},
		{"Good Friday", easter.addDays(-2)},
		{"Easter Monday", easter.addDays(1)},
		{"Ascension Day", easter.addDays(39)},
		{"Pentecost Sunday", easter.addDays(50)},
	}

	return containsDate(holidays, date)
}

func (date Date) isSwedishMidsummer() bool {
	midsummerDay := date.Month == 6 && date.Day >= 20 && date.Day <= 26 && date.toTime().Weekday().String() == "Saturday"
	midsummerEve := date.Month == 6 && date.Day >= 19 && date.Day <= 25 && date.toTime().Weekday().String() == "Friday"

	return midsummerDay || midsummerEve
}

func (date Date) isFixedDateHoliday() bool {
	christmas := Date{date.Year, 12, 25}

	holidays := []struct {
		name string
		date Date
	}{
		{"Chirstmas Day", christmas},
		{"Chirstmas Eve", christmas.addDays(-1)},
		{"Second Day of Chirstmas", christmas.addDays(1)},
		{"New Year's Eve", Date{date.Year, 1, 1}},
		{"Epiphany", christmas.addDays(12)},
		{"Labour Day", Date{date.Year, 5, 1}},
		{"National Day", Date{date.Year, 6, 6}},
	}

	return containsDate(holidays, date)
}
