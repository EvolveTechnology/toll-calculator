package vehicle

// emergency struct for handling Emergency vehicle type.
type emergency struct{}

// GetVehicleType will return the "Emergency" type.
func (e emergency) GetVehicleType() string {
	return "Emergency"
}

// IsTollFree return true for Emergency type.
func (e emergency) IsTollFree() bool {
	return true
}

// NewEmergency return a vehicle type of Emergency.
func NewEmergency() Vehicle {
	return &emergency{}
}
