package tollcalc

import (
	"evolvetech/tollcalc/vehicle"
	"testing"
	"time"
)

// TestGetTollFee1 test when vehicle is nil
func TestGetTollFee1(t *testing.T) {
	defer func() {
		if r := recover(); r == nil {
			t.Errorf("The code did not panic, must be panic when vehicle is nil")
		}
	}()

	GetTollFee(nil, nil)
}

// TestGetTollFee2 test when dates is nil or length is zero
func TestGetTollFee2(t *testing.T) {
	v := vehicle.NewNoop()
	totalFee := GetTollFee(v, nil)
	if totalFee != 0 {
		t.Errorf("when date length is 0, should return totalFee %d but got %d", 0, totalFee)
		t.Fail()
	}
}

// TestGetTollFee3 test when duration of date is <= 60
// from 8.00 to 8.00 (exactly 0 minutes). And the start is in 8.00 when it has parking fee 13.
// Current totalFee is 0, and nextFee == tempFee (because only one date).
// This skip `if totalFee > 0` statement
func TestGetTollFee3(t *testing.T) {
	v := vehicle.NewNoop()

	dates := []time.Time{
		time.Date(2020, 04, 27, 8, 0, 0, 0, time.Local),
	}

	totalFee := GetTollFee(v, dates)
	if totalFee != 13 {
		t.Errorf("when between 8.00 - 8.00, should return totalFee %d but got %d", 13, totalFee)
		t.Fail()
	}
}

// TestGetTollFee4 test when duration of date is <= 60
// from 8.00 to 9.00 (exactly 60 minutes). And the start is in 8.00 when it has parking fee 13.
// Second iteration, totalFee has value 13 (from time 8.00), and nextFee < tempFee (because time 9.00 has nextFee 0)
// Parking fee in time of 9.00 is 0, so totalFee += tempFee == 13 += 0 which still return 13.
// This pass `if totalFee > 0` on second iteration.
func TestGetTollFee4(t *testing.T) {
	v := vehicle.NewNoop()

	dates := []time.Time{
		time.Date(2020, 04, 27, 8, 0, 0, 0, time.Local),
		time.Date(2020, 04, 27, 9, 0, 0, 0, time.Local),
	}

	totalFee := GetTollFee(v, dates)
	if totalFee != 13 {
		t.Errorf("when between 8.00 - 9.00, should return totalFee %d but got %d", 13, totalFee)
		t.Fail()
	}
}

// TestGetTollFee5 test when duration of date is > 60
// from 8.00 to 9.30 (exactly 60 minutes).
// And the start is in 8.00 when it has parking fee 13.
// 9.30 has parking fee 8. So, total parking fee is 21.
// This pass `duration.Minutes() <= 60` on first iteration, and skip it in second iteration.
func TestGetTollFee5(t *testing.T) {
	v := vehicle.NewNoop()

	dates := []time.Time{
		time.Date(2020, 04, 27, 8, 0, 0, 0, time.Local),
		time.Date(2020, 04, 27, 9, 30, 0, 0, time.Local),
	}

	totalFee := GetTollFee(v, dates)
	if totalFee != 21 {
		t.Errorf("when between 8.00 - 9.30, should return totalFee %d but got %d", 21, totalFee)
		t.Fail()
	}
}

// TestGetTollFee6 test when duration of date is > 60
// 07.00 has parking fee 18
// 15.30 has parking fee 18
// 16.00 has parking fee 18
// 16.30 has parking fee 18
// So, total will be 72, higher than maximum 60, and statement `totalFee > 60` will be passed on fourth iteration.
// Skipping statement `if totalFee > 0` on all iteration since between hour has duration higher than 60.
// Returning 60.
func TestGetTollFee6(t *testing.T) {
	v := vehicle.NewNoop()

	dates := []time.Time{
		time.Date(2020, 04, 27, 7, 0, 0, 0, time.Local),
		time.Date(2020, 04, 27, 15, 30, 0, 0, time.Local),
		time.Date(2020, 04, 27, 16, 00, 0, 0, time.Local),
		time.Date(2020, 04, 27, 16, 30, 0, 0, time.Local),
	}

	totalFee := GetTollFee(v, dates)
	if totalFee != 60 {
		t.Errorf("when between 7.00, 15.30, 16.00, and 16.30 should return totalFee %d but got %d", 60, totalFee)
		t.Fail()
	}
}

// TestGetTollFee7 test when duration of date is > 60
// 07.00 has parking fee 18
// 07.10 has parking fee 18
// 07.20 has parking fee 18
// 07.30 has parking fee 18
// Pass statement `if totalFee > 0` on all iteration since between hour has duration lower than 60.
// So, it will use only one highest fee at same hour, that is 18.
// Returning 18.
func TestGetTollFee7(t *testing.T) {
	v := vehicle.NewNoop()

	dates := []time.Time{
		time.Date(2020, 04, 27, 7, 0, 0, 0, time.Local),
		time.Date(2020, 04, 27, 7, 10, 0, 0, time.Local),
		time.Date(2020, 04, 27, 7, 20, 0, 0, time.Local),
		time.Date(2020, 04, 27, 7, 30, 0, 0, time.Local),
	}

	totalFee := GetTollFee(v, dates)
	if totalFee != 18 {
		t.Errorf("when between 7.00, 7.10, 7.20 and 7.30, should return totalFee %d but got %d", 18, totalFee)
		t.Fail()
	}
}
