package vehicle

// foreign struct for handling Foreign vehicle type.
type foreign struct{}

// GetVehicleType will return the "Foreign" type.
func (f foreign) GetVehicleType() string {
	return "Foreign"
}

// IsTollFree return true for Foreign type.
func (f foreign) IsTollFree() bool {
	return true
}

// NewForeign return a vehicle type of Foreign.
func NewForeign() Vehicle {
	return &foreign{}
}
