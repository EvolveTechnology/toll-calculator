package vehicle

import (
	"testing"
)

// TestNewTractor test return type
func TestNewTractor(t *testing.T) {
	v := NewTractor()
	if _, ok := v.(Vehicle); !ok {
		t.Errorf("NewTractor must return Vehicle interface")
		t.Fail()
	}
}

// TestTractor_GetVehicleType test the string returned
func TestTractor_GetVehicleType(t *testing.T) {
	v := NewTractor()
	name := v.GetVehicleType()
	if name != "Tractor" {
		t.Errorf("must return %s", "Tractor")
		t.Fail()
	}
}

// TestTractor_IsTollFree should return the toll-free status
func TestTractor_IsTollFree(t *testing.T) {
	v := NewTractor()
	isFree := v.IsTollFree()
	if isFree != true {
		t.Errorf("%s toll-free status must return %v", "Tractor", true)
		t.Fail()
	}
}
