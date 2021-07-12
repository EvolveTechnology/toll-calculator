package main

import (
	"testing"
)

func TestCalculateTollTotal(t *testing.T) {
	var tests = []struct {
		name  string
		input DateTime
		want  int
	}{
		{name: "Non holiday rush hour", input: DateTime{date: Date{Year: 2016, Month: 9, Day: 16}, timesOfDay: []TimeOfDay{{Hour: 16, Minute: 29}}}, want: 18},
		{name: "Non holiday rush hour multiple data within an hour",
			input: DateTime{
				date: Date{Year: 2016, Month: 9, Day: 16},
				timesOfDay: []TimeOfDay{
					{Hour: 16, Minute: 29},
					{Hour: 16, Minute: 29},
					{Hour: 16, Minute: 29},
					{Hour: 16, Minute: 29},
				},
			},
			want: 18},
		{name: "Non holiday rush hour multiple data throughout the day",
			input: DateTime{
				date: Date{Year: 2016, Month: 9, Day: 16},
				timesOfDay: []TimeOfDay{
					{Hour: 8, Minute: 29},
					{Hour: 13, Minute: 29},
					{Hour: 14, Minute: 29},
					{Hour: 18, Minute: 29},
				},
			},
			want: 37},
		{name: "Weekend", input: DateTime{date: Date{Year: 2016, Month: 9, Day: 18}, timesOfDay: []TimeOfDay{{Hour: 16, Minute: 29}}}, want: 0},
		{name: "Easter 2003", input: DateTime{date: Date{Year: 2003, Month: 4, Day: 20}, timesOfDay: []TimeOfDay{{Hour: 6, Minute: 59}}}, want: 0},
	}

	t.Run("Car", func(t *testing.T) {
		vehicle := Car{}
		for _, tc := range tests {

			got := CalculateTollTotal(vehicle, tc.input)
			if got != tc.want {
				t.Fatalf("%s: expected: %d, got: %d", tc.name, tc.want, got)
			}
		}
	})

	t.Run("Toll free vehicle - Non holiday rush hour multiple data throughout the day", func(t *testing.T) {
		vehicle := Tractor{}
		input := DateTime{
			date: Date{Year: 2016, Month: 9, Day: 16},
			timesOfDay: []TimeOfDay{
				{Hour: 8, Minute: 29},
				{Hour: 13, Minute: 29},
				{Hour: 14, Minute: 29},
				{Hour: 18, Minute: 29},
			},
		}
		want := 0
		got := CalculateTollTotal(vehicle, input)

		if got != want {
			t.Fatalf("Expected: %d, got: %d", want, got)
		}
	})
}
