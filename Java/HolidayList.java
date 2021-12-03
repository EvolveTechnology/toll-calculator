package com.evolve.tollcalculator;

public enum HolidayList {
    JANUARY("1"),
    MARCH("28,29"),
    APRIL("1,30"),
    MAY("1,8,9"),
    JUNE("5,6,21"),
    NOVEMBER("1"),
    DECEMBER("24,25,26,31");

    private final String month;

    HolidayList(String month) {
      this.month = month;
    }

    public static String getDays(String month) {
        for (HolidayList holidayList : HolidayList.values()) {
            if (holidayList.name().equalsIgnoreCase(month)) {
                return holidayList.month;
            }
        }
        return null;
    }

}
