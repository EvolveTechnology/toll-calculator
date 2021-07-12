package main

import (
	"testing"
)

func TestGetTollPrice(t *testing.T) {
	var tests = []struct {
		name  string
		input TimeOfDay
		want  int
	}{
		{name: "5:59", input: TimeOfDay{5, 59}, want: 0},
		{name: "6:00", input: TimeOfDay{6, 0}, want: 8},
		{name: "8:29", input: TimeOfDay{8, 29}, want: 13},
		{name: "8:30", input: TimeOfDay{8, 30}, want: 8},
		{name: "11:51", input: TimeOfDay{11, 51}, want: 8},
		{name: "16:01", input: TimeOfDay{16, 1}, want: 18},
		{name: "18:29", input: TimeOfDay{18, 29}, want: 8},
		{name: "18:31", input: TimeOfDay{18, 31}, want: 0},
		{name: "19:00", input: TimeOfDay{19, 0}, want: 0},
	}

	for _, tc := range tests {
		got := tc.input.GetTollPrice(pricesPerTimeOfDay)
		if got != tc.want {
			t.Fatalf("%s: expected: %d, got: %d", tc.name, tc.want, got)
		}
	}
}
