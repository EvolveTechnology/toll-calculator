package vehicle

import (
	"testing"
)

// TestNewForeign test return type
func TestNewForeign(t *testing.T) {
	v := NewForeign()
	if _, ok := v.(Vehicle); !ok {
		t.Errorf("NewForeign must return Vehicle interface")
		t.Fail()
	}
}

// TestForeign_GetVehicleType test the string returned
func TestForeign_GetVehicleType(t *testing.T) {
	v := NewForeign()
	name := v.GetVehicleType()
	if name != "Foreign" {
		t.Errorf("must return %s", "Foreign")
		t.Fail()
	}
}

// TestForeign_IsTollFree should return the toll-free status
func TestForeign_IsTollFree(t *testing.T) {
	v := NewForeign()
	isFree := v.IsTollFree()
	if isFree != true {
		t.Errorf("%s toll-free status must return %v", "Foreign", true)
		t.Fail()
	}
}
