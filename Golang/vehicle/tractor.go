package vehicle

// tractor struct for handling Tractor type.
type tractor struct{}

// GetVehicleType will return the "Tractor" type.
func (t tractor) GetVehicleType() string {
	return "Tractor"
}

// IsTollFree return true for Tractor type.
func (t tractor) IsTollFree() bool {
	return true
}

// NewTractor return a vehicle type of Tractor.
func NewTractor() Vehicle {
	return &tractor{}
}
