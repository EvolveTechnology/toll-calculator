package vehicle

import (
	"testing"
)

// TestNewDiplomat test return type
func TestNewDiplomat(t *testing.T) {
	v := NewDiplomat()
	if _, ok := v.(Vehicle); !ok {
		t.Errorf("NewDiplomat must return Vehicle interface")
		t.Fail()
	}
}

// TestDiplomat_GetVehicleType test the string returned
func TestDiplomat_GetVehicleType(t *testing.T) {
	v := NewDiplomat()
	name := v.GetVehicleType()
	if name != "Diplomat" {
		t.Errorf("must return %s", "Diplomat")
		t.Fail()
	}
}

// TestDiplomat_IsTollFree should return the toll-free status
func TestDiplomat_IsTollFree(t *testing.T) {
	v := NewDiplomat()
	isFree := v.IsTollFree()
	if isFree != true {
		t.Errorf("%s toll-free status must return %v", "Diplomat", true)
		t.Fail()
	}
}
