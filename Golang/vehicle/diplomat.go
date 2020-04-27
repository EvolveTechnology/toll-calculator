package vehicle

// diplomat struct for handling Diplomat type.
type diplomat struct{}

// GetVehicleType will return the "Diplomat" type.
func (d diplomat) GetVehicleType() string {
	return "Diplomat"
}

// IsTollFree return true for Diplomat type.
func (d diplomat) IsTollFree() bool {
	return true
}

// NewDiplomat return a vehicle type of Diplomat.
func NewDiplomat() Vehicle {
	return &diplomat{}
}
