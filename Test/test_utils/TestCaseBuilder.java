package test_utils;

import calculator.FeeForTimeOfDaySpecification;
import calculator.Vehicle;
import util.Day;
import util.TimeOfDay;

import java.util.Calendar;
import java.util.function.Predicate;

public class TestCaseBuilder extends TestCaseBuilderBase {

    DateTestDataBuilder actualTime;

    public TestCaseBuilder(String nameHeader,
                           String nameTail,
                           DateTestDataBuilder actualTime,
                           Vehicle actualVehicle,
                           int expected)
    {
        this.nameHeader = nameHeader;
        this.nameTail = nameTail;
        this.actualTime = actualTime;
        this.actualVehicle = actualVehicle;
        this.expected = expected;
    }

    public static TestCaseBuilder newWithoutHeader()
    {
        return new TestCaseBuilder(null, null,
                                   DateTestDataBuilder.ofDay(2013, Calendar.JANUARY, 1),
                                   TestData.A_NON_FREE_VEHICLE,
                                   -1);
    }

    public TestCase build()
    {
        return new TestCase(name(),
                            configuration,
                            actualVehicle, actualTime.build(),
                            expected);
    }

    public Object[] build2()
    {
        return new Object[]{build()};
    }

    public Object[] named(String name)
    {
        withName(name);
        return build2();
    }

    public TestCaseBuilder withNameHeader(String nameHeader)
    {
        this.nameHeader = nameHeader;
        return this;
    }

    public TestCaseBuilder withName(String nameTail)
    {
        this.nameTail = nameTail;
        return this;
    }

    public TestCaseBuilder withIsHolidaySpecification(Predicate<Day> isTollFreeDay)
    {
        this.configuration.isHoliday = isTollFreeDay;
        return this;
    }

    public TestCaseBuilder withFeeForTimeOfDaySpecification(FeeForTimeOfDaySpecification feeForTimeOfDay)
    {
        this.configuration.feeForTimeOfDay = feeForTimeOfDay;
        return this;
    }

    public TestCaseBuilder withIsTollFreeVehicleSpecification(Predicate<Vehicle> isTollFreeVehicle)
    {
        this.configuration.isTollFreeVehicle = isTollFreeVehicle;
        return this;
    }

    public TestCaseBuilder withMaxFeePerDay(int x)
    {
        this.configuration.maxFeePerDay = x;
        return this;
    }

    public TestCaseBuilder withMinNumMinutesBetweenCharges(int x)
    {
        this.configuration.minNumMinutesBetweenCharges = x;
        return this;
    }


    public TestCaseBuilder withDay(Day day)
    {
        this.actualTime.withDay(day);
        return this;
    }

    public TestCaseBuilder withTime(TimeOfDay timeOfDay)
    {
        this.actualTime.withTime(timeOfDay);
        return this;
    }

    public TestCaseBuilder withVehicle(Vehicle vehicle)
    {
        this.actualVehicle = vehicle;
        return this;
    }

    public TestCaseBuilder withExpectedFee(int expected)
    {
        this.expected = expected;
        return this;
    }
}
