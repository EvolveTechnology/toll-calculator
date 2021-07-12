package main

import "testing"

func TestIsTollFreeVehicle(t *testing.T) {
	t.Run("Car", func(t *testing.T) {
		vehicle := Car{}
		got := IsTollFreeVehicle(vehicle)
		if got {
			t.Errorf("IsTollFreeVehicle() = %t; want false", got)
		}
	})
	t.Run("Motorbike", func(t *testing.T) {
		vehicle := Motorbike{}
		got := IsTollFreeVehicle(vehicle)
		if !got {
			t.Errorf("IsTollFreeVehicle() = %t; want true", got)
		}
	})
	t.Run("Emergency", func(t *testing.T) {
		vehicle := EmergencyVehicle{}
		got := IsTollFreeVehicle(vehicle)
		if !got {
			t.Errorf("IsTollFreeVehicle() = %t; want true", got)
		}
	})
	t.Run("Diplomat", func(t *testing.T) {
		vehicle := DiplomatVehicle{}
		got := IsTollFreeVehicle(vehicle)
		if !got {
			t.Errorf("IsTollFreeVehicle() = %t; want true", got)
		}
	})
	t.Run("Tractor", func(t *testing.T) {
		vehicle := Tractor{}
		got := IsTollFreeVehicle(vehicle)
		if !got {
			t.Errorf("IsTollFreeVehicle() = %t; want true", got)
		}
	})
	t.Run("Foreign", func(t *testing.T) {
		vehicle := ForeignVehicle{}
		got := IsTollFreeVehicle(vehicle)
		if !got {
			t.Errorf("IsTollFreeVehicle() = %t; want true", got)
		}
	})
	t.Run("Military", func(t *testing.T) {
		vehicle := MilitaryVehicle{}
		got := IsTollFreeVehicle(vehicle)
		if !got {
			t.Errorf("IsTollFreeVehicle() = %t; want true", got)
		}
	})
}
