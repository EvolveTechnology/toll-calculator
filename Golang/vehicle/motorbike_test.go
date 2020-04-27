package vehicle

import (
	"testing"
)

// TestNewMotorbike test return type
func TestNewMotorbike(t *testing.T) {
	v := NewMotorbike()
	if _, ok := v.(Vehicle); !ok {
		t.Errorf("NewMotorbike must return Vehicle interface")
		t.Fail()
	}
}

// TestMotorbike_GetVehicleType test the string returned
func TestMotorbike_GetVehicleType(t *testing.T) {
	v := NewMotorbike()
	name := v.GetVehicleType()
	if name != "Motorbike" {
		t.Errorf("must return %s", "Motorbike")
		t.Fail()
	}
}

// TestMotorbike_IsTollFree should return the toll-free status
func TestMotorbike_IsTollFree(t *testing.T) {
	v := NewMotorbike()
	isFree := v.IsTollFree()
	if isFree != true {
		t.Errorf("%s toll-free status must return %v", "Motorbike", true)
		t.Fail()
	}
}
