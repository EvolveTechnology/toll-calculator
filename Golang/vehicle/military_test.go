package vehicle

import (
	"testing"
)

// TestNewMilitary test return type
func TestNewMilitary(t *testing.T) {
	v := NewMilitary()
	if _, ok := v.(Vehicle); !ok {
		t.Errorf("NewMilitary must return Vehicle interface")
		t.Fail()
	}
}

// TestMilitary_GetVehicleType test the string returned
func TestMilitary_GetVehicleType(t *testing.T) {
	v := NewMilitary()
	name := v.GetVehicleType()
	if name != "Military" {
		t.Errorf("must return %s", "Military")
		t.Fail()
	}
}

// TestMilitary_IsTollFree should return the toll-free status
func TestMilitary_IsTollFree(t *testing.T) {
	v := NewMilitary()
	isFree := v.IsTollFree()
	if isFree != true {
		t.Errorf("%s toll-free status must return %v", "Military", true)
		t.Fail()
	}
}
