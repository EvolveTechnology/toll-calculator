package vehicle

import (
	"testing"
)

// TestNewNoop test return type
func TestNewNoop(t *testing.T) {
	v := NewNoop()
	if _, ok := v.(Vehicle); !ok {
		t.Errorf("NewNoop must return Vehicle interface")
		t.Fail()
	}
}

// TestNoop_GetVehicleType test the string returned
func TestNoop_GetVehicleType(t *testing.T) {
	v := NewNoop()
	name := v.GetVehicleType()
	if name != "" {
		t.Errorf("NewNoop must return %s", "")
		t.Fail()
	}
}

// TestNoop_IsTollFree should return the toll-free status
func TestNoop_IsTollFree(t *testing.T) {
	v := NewNoop()
	isFree := v.IsTollFree()
	if isFree != false {
		t.Errorf("%s toll-free status must return %v", "NewNoop", true)
		t.Fail()
	}
}
