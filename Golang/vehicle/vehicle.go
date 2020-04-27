package vehicle

// Vehicle is a interface to define new vehicle in system.
type Vehicle interface {
	// GetVehicleType should return the name of vehicle
	GetVehicleType() string

	// Should return true when it is eligible for toll-free, otherwise return false.
	IsTollFree() bool
}

// noop struct for handling no operation of vehicle
type noop struct{}

// GetVehicleType return empty string of vehicle name.
func (n noop) GetVehicleType() string { return "" }

// IsTollFree always return false.
func (n noop) IsTollFree() bool { return false }

// NewNoop return default Vehicle object with no-operation
func NewNoop() Vehicle {
	return &noop{}
}
