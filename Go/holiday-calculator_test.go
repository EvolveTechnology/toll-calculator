package main

import (
	"reflect"
	"testing"
)

func TestCalculateEaster(t *testing.T) {
	var tests = []struct {
		name  string
		input int
		want  Date
	}{
		{name: "2001", input: 2001, want: Date{Year: 2001, Month: 4, Day: 15}},
		{name: "2022", input: 2022, want: Date{Year: 2022, Month: 4, Day: 17}},
		{name: "2007", input: 2007, want: Date{Year: 2007, Month: 4, Day: 8}},
		{name: "1989", input: 1989, want: Date{Year: 1989, Month: 3, Day: 26}},
	}

	for _, tc := range tests {
		got := calculateEaster(tc.input)
		if !reflect.DeepEqual(tc.want, got) {
			t.Fatalf("%s: expected: %v, got: %v", tc.name, tc.want, got)
		}
	}
}

func TestIsEasterRelatedHoliday(t *testing.T) {
	var tests = []struct {
		name     string
		receiver Date
		want     bool
	}{
		{name: "1692", receiver: Date{Year: 1692, Month: 4, Day: 7}, want: true},
		{name: "2044", receiver: Date{Year: 2044, Month: 4, Day: 15}, want: true},
		{name: "2003", receiver: Date{Year: 2003, Month: 4, Day: 15}, want: false},
		{name: "2018", receiver: Date{Year: 2018, Month: 3, Day: 31}, want: false},
	}

	for _, tc := range tests {
		got := tc.receiver.isEasterRelatedHoliday()
		if got != tc.want {
			t.Fatalf("%s: expected: %t, got: %t", tc.name, tc.want, got)
		}
	}
}

func TestIsFixedDateHoliday(t *testing.T) {
	var tests = []struct {
		name     string
		receiver Date
		want     bool
	}{
		{name: "1692", receiver: Date{Year: 1692, Month: 12, Day: 24}, want: true},
		{name: "2044", receiver: Date{Year: 2044, Month: 1, Day: 1}, want: true},
		{name: "2003", receiver: Date{Year: 2003, Month: 8, Day: 15}, want: false},
		{name: "2018", receiver: Date{Year: 2018, Month: 2, Day: 10}, want: false},
	}

	for _, tc := range tests {
		got := tc.receiver.isFixedDateHoliday()
		if got != tc.want {
			t.Fatalf("%s: expected: %t, got: %t", tc.name, tc.want, got)
		}
	}
}

func TestIsSwedishMidsummer(t *testing.T) {
	var tests = []struct {
		name     string
		receiver Date
		want     bool
	}{
		{name: "2002", receiver: Date{Year: 2002, Month: 6, Day: 22}, want: true},
		{name: "2006", receiver: Date{Year: 2006, Month: 6, Day: 23}, want: true},
		{name: "2013", receiver: Date{Year: 2013, Month: 6, Day: 18}, want: false},
		{name: "2018", receiver: Date{Year: 2018, Month: 6, Day: 25}, want: false},
	}

	for _, tc := range tests {
		got := tc.receiver.isSwedishMidsummer()
		if got != tc.want {
			t.Fatalf("%s: expected: %t, got: %t", tc.name, tc.want, got)
		}
	}
}
