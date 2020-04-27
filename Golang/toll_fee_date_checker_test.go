package tollcalc

import (
	"testing"
	"time"
)

// Test_isTollFreeDate1 test on date saturday
func Test_isTollFreeDate1(t *testing.T) {
	date, _ := time.Parse("2006-01-02", "2020-04-25")
	resp := isTollFreeDate(date)
	if resp != true {
		t.Error("saturday must toll free")
		t.Fail()
	}
}

// Test_isTollFreeDate2 test on date sunday
func Test_isTollFreeDate2(t *testing.T) {
	date, _ := time.Parse("2006-01-02", "2020-04-26")
	resp := isTollFreeDate(date)
	if resp != true {
		t.Error("sunday must toll free")
		t.Fail()
	}
}

// Test_isTollFreeDate3 test when date is not saturday or sunday, and year is not 2013
func Test_isTollFreeDate3(t *testing.T) {
	date, _ := time.Parse("2006-01-02", "2020-04-27")
	resp := isTollFreeDate(date)
	if resp != false {
		t.Error("year other than 2013 must not toll free")
		t.Fail()
	}
}

// Test_isTollFreeDate4 test when year is 2013, and month is January and date is 1 must toll free
func Test_isTollFreeDate4(t *testing.T) {
	date, _ := time.Parse("2006-01-02", "2013-01-01")
	resp := isTollFreeDate(date)
	if resp != true {
		t.Error("01 January 2013 must toll free")
		t.Fail()
	}
}

// Test_isTollFreeDate5 test when year is 2013, and month is March and date is 28 or 29 must toll free
func Test_isTollFreeDate5(t *testing.T) {
	date, _ := time.Parse("2006-01-02", "2013-03-28")
	resp := isTollFreeDate(date)
	if resp != true {
		t.Error("28 March 2013 must toll free")
		t.Fail()
	}

	date, _ = time.Parse("2006-01-02", "2013-03-29")
	resp = isTollFreeDate(date)
	if resp != true {
		t.Error("29 March 2013 must toll free")
		t.Fail()
	}
}

// Test_isTollFreeDate6 test when year is 2013, and month is April and date is 1 or 30 must toll free
func Test_isTollFreeDate6(t *testing.T) {
	date, _ := time.Parse("2006-01-02", "2013-04-01")
	resp := isTollFreeDate(date)
	if resp != true {
		t.Error("1 April 2013 must toll free")
		t.Fail()
	}

	date, _ = time.Parse("2006-01-02", "2013-04-30")
	resp = isTollFreeDate(date)
	if resp != true {
		t.Error("30 April 2013 must toll free")
		t.Fail()
	}
}

// Test_isTollFreeDate7 test when year is 2013, and month is May and date is 1, 8 or 9 must toll free
func Test_isTollFreeDate7(t *testing.T) {
	date, _ := time.Parse("2006-01-02", "2013-05-01")
	resp := isTollFreeDate(date)
	if resp != true {
		t.Error("1 May 2013 must toll free")
		t.Fail()
	}

	date, _ = time.Parse("2006-01-02", "2013-05-08")
	resp = isTollFreeDate(date)
	if resp != true {
		t.Error("8 May 2013 must toll free")
		t.Fail()
	}

	date, _ = time.Parse("2006-01-02", "2013-05-09")
	resp = isTollFreeDate(date)
	if resp != true {
		t.Error("9 May 2013 must toll free")
		t.Fail()
	}
}

// Test_isTollFreeDate8 test when year is 2013, and month is June and date is 5, 6 or 21 must toll free
func Test_isTollFreeDate8(t *testing.T) {
	date, _ := time.Parse("2006-01-02", "2013-06-05")
	resp := isTollFreeDate(date)
	if resp != true {
		t.Error("6 June 2013 must toll free")
		t.Fail()
	}

	date, _ = time.Parse("2006-01-02", "2013-06-06")
	resp = isTollFreeDate(date)
	if resp != true {
		t.Error("6 June 2013 must toll free")
		t.Fail()
	}

	date, _ = time.Parse("2006-01-02", "2013-06-21")
	resp = isTollFreeDate(date)
	if resp != true {
		t.Error("21 June 2013 must toll free")
		t.Fail()
	}
}

// Test_isTollFreeDate9 test when year is 2013, and month is 7
func Test_isTollFreeDate9(t *testing.T) {
	date, _ := time.Parse("2006-01-02", "2013-07-01")
	resp := isTollFreeDate(date)
	if resp != true {
		t.Error("07 July 2013 must toll free")
		t.Fail()
	}
}

// Test_isTollFreeDate10 test when year is 2013, and month is November and date is 1
func Test_isTollFreeDate10(t *testing.T) {
	date, _ := time.Parse("2006-01-02", "2013-11-01")
	resp := isTollFreeDate(date)
	if resp != true {
		t.Error("11 November 2013 must toll free")
		t.Fail()
	}
}

// Test_isTollFreeDate11 test when year is 2013, and month is December and date is 24, 25, 26 or 31 must toll free
func Test_isTollFreeDate11(t *testing.T) {
	date, _ := time.Parse("2006-01-02", "2013-12-24")
	resp := isTollFreeDate(date)
	if resp != true {
		t.Error("24 December 2013 must toll free")
		t.Fail()
	}

	date, _ = time.Parse("2006-01-02", "2013-12-25")
	resp = isTollFreeDate(date)
	if resp != true {
		t.Error("25 December 2013 must toll free")
		t.Fail()
	}

	date, _ = time.Parse("2006-01-02", "2013-12-26")
	resp = isTollFreeDate(date)
	if resp != true {
		t.Error("26 December 2013 must toll free")
		t.Fail()
	}

	date, _ = time.Parse("2006-01-02", "2013-12-31")
	resp = isTollFreeDate(date)
	if resp != true {
		t.Error("31 December 2013 must toll free")
		t.Fail()
	}
}

// Test_isTollFreeDate12 test when year is 2013, and month and date is not toll free
func Test_isTollFreeDate12(t *testing.T) {
	date, _ := time.Parse("2006-01-02", "2013-12-02") // Monday
	resp := isTollFreeDate(date)
	if resp != false {
		t.Error("1 December 2013 must not toll free")
		t.Fail()
	}
}
