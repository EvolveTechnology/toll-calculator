package tollcalc

import (
	"testing"
	"time"
)

// Test_getTollFeeHour1 test when vehicle object is not toll free (so `isTollFreeVehicle` return true)
// or date is on Sunday (so `isTollFreeDate` also return true)
func Test_getTollFeeHour1(t *testing.T) {
	d := dummyVehicle{
		name:       "Dummy Vehicle",
		isTollFree: true,
	}

	monday, _ := time.Parse("2006-01-02", "2020-04-27")
	if getTollFeeHour(d, monday) != 0 {
		t.Errorf("getTollFeeHour must return 0 when date is on monday, but vehicle is toll free")
		t.Fail()
		return
	}

	sunday, _ := time.Parse("2006-01-02", "2020-04-26")
	if getTollFeeHour(nil, sunday) != 0 {
		t.Errorf("getTollFeeHour must return 0 when date is on sunday, although the vehicle is not nil")
		t.Fail()
		return
	}
}

// Test_getTollFeeHour2 test parking fee in between hour 6.00 - 6.29
func Test_getTollFeeHour2(t *testing.T) {
	d := dummyVehicle{
		name:       "Dummy Vehicle",
		isTollFree: false,
	}

	monday, _ := time.Parse("2006-01-02 15:04", "2020-04-27 06:00")
	if getTollFeeHour(d, monday) != 8 {
		t.Errorf("getTollFeeHour in monday between 6.00 - 6.29 must be SEK 8")
		t.Fail()
		return
	}
}

// Test_getTollFeeHour3 test parking fee in between hour 6.30 - 6.59
func Test_getTollFeeHour3(t *testing.T) {
	d := dummyVehicle{
		name:       "Dummy Vehicle",
		isTollFree: false,
	}

	monday, _ := time.Parse("2006-01-02 15:04", "2020-04-27 06:30")
	if getTollFeeHour(d, monday) != 13 {
		t.Errorf("getTollFeeHour in monday between 6.30 - 6.59 must be SEK 13")
		t.Fail()
		return
	}
}

// Test_getTollFeeHour4 test parking fee in between hour 7.00 - 7.59
func Test_getTollFeeHour4(t *testing.T) {
	d := dummyVehicle{
		name:       "Dummy Vehicle",
		isTollFree: false,
	}

	monday, _ := time.Parse("2006-01-02 15:04", "2020-04-27 07:00")
	if getTollFeeHour(d, monday) != 18 {
		t.Errorf("getTollFeeHour in monday between 7.00 - 7.59 must be SEK 18")
		t.Fail()
		return
	}
}

// Test_getTollFeeHour5 test parking fee in between hour 8.00 - 8.29
func Test_getTollFeeHour5(t *testing.T) {
	d := dummyVehicle{
		name:       "Dummy Vehicle",
		isTollFree: false,
	}

	monday, _ := time.Parse("2006-01-02 15:04", "2020-04-27 08:00")
	if getTollFeeHour(d, monday) != 13 {
		t.Errorf("getTollFeeHour in monday between 8.00 - 8.29 must be SEK 13")
		t.Fail()
		return
	}
}

// Test_getTollFeeHour6 test parking fee in between these hours:
// 8.30 - 8.59
// 9.30 - 9.59
// 10.30 - 10.59
// 11.30 - 11.59
// 12.30 - 12.59
// 13.30 - 13.59
// 14.30 - 14.59
// This mean, these hour is free:
// 9.00 - 9.29
// 10.00 - 10.29
// 11.00 - 11.29
// 12.00 - 12.29
// 13.00 - 13.29
// 14.00 - 14.29
func Test_getTollFeeHour6(t *testing.T) {
	d := dummyVehicle{
		name:       "Dummy Vehicle",
		isTollFree: false,
	}

	monday, _ := time.Parse("2006-01-02 15:04", "2020-04-27 8:30")
	if getTollFeeHour(d, monday) != 8 {
		t.Errorf("getTollFeeHour in monday between hour 8-14 and minute between 30-59 must be SEK 8")
		t.Fail()
		return
	}
}

// Test_getTollFeeHour7 see test Test_getTollFeeHour6 why this test was written
func Test_getTollFeeHour7(t *testing.T) {
	d := dummyVehicle{
		name:       "Dummy Vehicle",
		isTollFree: false,
	}

	// Hooray! Minutes of free parking!
	mondayFree, _ := time.Parse("2006-01-02 15:04", "2020-04-27 9:00")
	if getTollFeeHour(d, mondayFree) != 0 {
		t.Errorf("getTollFeeHour in monday between hour 8-14 and minute between 0-29 must be SEK 0")
		t.Fail()
		return
	}
}

// Test_getTollFeeHour8 test parking fee in between hour 15.00 - 15.29
func Test_getTollFeeHour8(t *testing.T) {
	d := dummyVehicle{
		name:       "Dummy Vehicle",
		isTollFree: false,
	}

	monday, _ := time.Parse("2006-01-02 15:04", "2020-04-27 15:00")
	if getTollFeeHour(d, monday) != 13 {
		t.Errorf("getTollFeeHour in monday between 15.00 - 15.29 must be SEK 13")
		t.Fail()
		return
	}
}

// Test_getTollFeeHour9 test parking fee in between hour 15.30 - 16.59
func Test_getTollFeeHour9(t *testing.T) {
	d := dummyVehicle{
		name:       "Dummy Vehicle",
		isTollFree: false,
	}

	monday, _ := time.Parse("2006-01-02 15:04", "2020-04-27 15:30")
	if getTollFeeHour(d, monday) != 18 {
		t.Errorf("getTollFeeHour in monday between 15.30 - 16.59 must be SEK 18")
		t.Fail()
		return
	}
}

// Test_getTollFeeHour10 test parking fee in between hour 17.00 - 17.59
func Test_getTollFeeHour10(t *testing.T) {
	d := dummyVehicle{
		name:       "Dummy Vehicle",
		isTollFree: false,
	}

	monday, _ := time.Parse("2006-01-02 15:04", "2020-04-27 17:00")
	if getTollFeeHour(d, monday) != 13 {
		t.Errorf("getTollFeeHour in monday between 17.00 - 17.59 must be SEK 13")
		t.Fail()
		return
	}
}

// Test_getTollFeeHour11 test parking fee in between hour 18.00 - 18.29
func Test_getTollFeeHour11(t *testing.T) {
	d := dummyVehicle{
		name:       "Dummy Vehicle",
		isTollFree: false,
	}

	monday, _ := time.Parse("2006-01-02 15:04", "2020-04-27 18:00")
	if getTollFeeHour(d, monday) != 8 {
		t.Errorf("getTollFeeHour in monday between 18.00 - 18.29 must be SEK 8")
		t.Fail()
		return
	}
}

// Test_getTollFeeHour12 test parking fee in between hour 18.30 - 6.00
func Test_getTollFeeHour12(t *testing.T) {
	d := dummyVehicle{
		name:       "Dummy Vehicle",
		isTollFree: false,
	}

	monday, _ := time.Parse("2006-01-02 15:04", "2020-04-27 18:30")
	if getTollFeeHour(d, monday) != 0 {
		t.Errorf("getTollFeeHour in monday between 18.30 - 6.00 must be SEK 0")
		t.Fail()
		return
	}
}
