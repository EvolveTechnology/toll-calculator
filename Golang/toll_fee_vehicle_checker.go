package tollcalc

import (
	"evolvetech/tollcalc/vehicle"
)

// isTollFreeVehicle check nil values for vehicle v.
func isTollFreeVehicle(v vehicle.Vehicle) bool {
	if v == nil {
		return false
	}

	return v.IsTollFree()
}
