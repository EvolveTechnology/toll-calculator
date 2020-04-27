package vehicle

// military struct for handling Military type.
type military struct{}

// GetVehicleType will return the "Military" type.
func (m military) GetVehicleType() string {
	return "Military"
}

// IsTollFree return true for Military type.
func (m military) IsTollFree() bool {
	return true
}

// NewMilitary return a vehicle type of Military.
func NewMilitary() Vehicle {
	return &military{}
}
