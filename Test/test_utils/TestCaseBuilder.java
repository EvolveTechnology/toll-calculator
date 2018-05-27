package test_utils;

import calculator.Vehicle;
import util.Day;
import util.TimeOfDay;

import java.util.Calendar;

public class TestCaseBuilder extends TestCaseBuilderBase {

    DateTestDataBuilder actualTime;

    public TestCaseBuilder(String nameHeader,
                           String nameTail,
                           DateTestDataBuilder actualTime,
                           Vehicle actualVehicle,
                           int expected) {
        this.nameHeader = nameHeader;
        this.nameTail = nameTail;
        this.actualTime = actualTime;
        this.actualVehicle = actualVehicle;
        this.expected = expected;
    }

    public static TestCaseBuilder newWithHeader(String nameHeader) {
        return new TestCaseBuilder(nameHeader, null,
                                   DateTestDataBuilder.ofDay(2013, Calendar.JANUARY, 1),
                                   TestData.A_NON_FREE_VEHICLE,
                                   -1);
    }

    public static TestCaseBuilder newWithoutHeader() {
        return new TestCaseBuilder(null, null,
                                   DateTestDataBuilder.ofDay(2013, Calendar.JANUARY, 1),
                                   TestData.A_NON_FREE_VEHICLE,
                                   -1);
    }

    public TestCase build() {
        return new TestCase(name(),
                            specifications,
                            actualVehicle, actualTime.build(),
                            expected);
    }

    public Object[] build2() {
        return new Object[]{build()};
    }

    public Object[] named(String name) {
        withName(name);
        return build2();
    }

    public TestCaseBuilder withName(String nameTail) {
        this.nameTail = nameTail;
        return this;
    }

    public TestCaseBuilder withDay(int year,
                                   int month,
                                   int dayOfMonth) {
        this.actualTime.withDay(year, month, dayOfMonth);
        return this;
    }

    public TestCaseBuilder withDay(Day day) {
        this.actualTime.withDay(day);
        return this;
    }

    public TestCaseBuilder withTime(int hourOfDay,
                                    int minute,
                                    int second) {
        this.actualTime.withTime(hourOfDay, minute, second);
        return this;
    }

    public TestCaseBuilder withTime(TimeOfDay timeOfDay) {
        this.actualTime.withTime(timeOfDay);
        return this;
    }

    public TestCaseBuilder withVehicle(Vehicle vehicle) {
        this.actualVehicle = vehicle;
        return this;
    }

    public TestCaseBuilder withExpectedFee(int expected) {
        this.expected = expected;
        return this;
    }
}
