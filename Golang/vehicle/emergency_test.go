package vehicle

import (
	"testing"
)

// TestNewEmergency test return type
func TestNewEmergency(t *testing.T) {
	v := NewEmergency()
	if _, ok := v.(Vehicle); !ok {
		t.Errorf("NewEmergency must return Vehicle interface")
		t.Fail()
	}
}

// TestEmergency_GetVehicleType test the string returned
func TestEmergency_GetVehicleType(t *testing.T) {
	v := NewEmergency()
	name := v.GetVehicleType()
	if name != "Emergency" {
		t.Errorf("must return %s", "Emergency")
		t.Fail()
	}
}

// TestEmergency_IsTollFree should return the toll-free status
func TestEmergency_IsTollFree(t *testing.T) {
	v := NewEmergency()
	isFree := v.IsTollFree()
	if isFree != true {
		t.Errorf("%s toll-free status must return %v", "Emergency", true)
		t.Fail()
	}
}
