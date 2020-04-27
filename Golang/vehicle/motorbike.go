package vehicle

// motorbike struct for handling Motorbike type.
type motorbike struct{}

// GetVehicleType will return the "Motorbike" type.
func (m motorbike) GetVehicleType() string {
	return "Motorbike"
}

// IsTollFree return true for Motorbike type.
func (m motorbike) IsTollFree() bool {
	return true
}

// NewMotorbike return a vehicle type of Motorbike.
func NewMotorbike() Vehicle {
	return &motorbike{}
}
