package tollcalc

import (
	"testing"
)

// dummyVehicle is Vehicle with dummy data
type dummyVehicle struct {
	name       string
	isTollFree bool
}

// GetVehicleType return name of vehicle
func (d dummyVehicle) GetVehicleType() string {
	return d.name
}

// IsTollFree return the status of free-eligibility of toll
func (d dummyVehicle) IsTollFree() bool {
	return d.isTollFree
}

// Test_isTollFreeVehicle1 test when vehicle instance is nil, should return false
func Test_isTollFreeVehicle1(t *testing.T) {
	if isTollFreeVehicle(nil) != false {
		t.Error("must return false when vehicle is nil")
		t.Fail()
	}
}

// Test_isTollFreeVehicle2 test using dummy vehicle
func Test_isTollFreeVehicle2(t *testing.T) {
	d := &dummyVehicle{
		name:       "Dummy Vehicle",
		isTollFree: true,
	}

	if got := isTollFreeVehicle(d); got != d.isTollFree {
		t.Errorf("want %v got %v", d.isTollFree, got)
		t.Fail()
	}
}
