package test_utils;

import calculator.FeeForTimeOfDaySpecification;
import calculator.Vehicle;
import util.Day;

import java.util.Date;
import java.util.function.Predicate;

public class TestCaseWithMultipleDatesBuilder extends TestCaseBuilderBase {

    public TestCaseWithMultipleDatesBuilder(String nameHeader,
                                            String nameTail,
                                            Vehicle actualVehicle,
                                            int expected)
    {
        this.nameHeader = nameHeader;
        this.nameTail = nameTail;
        this.actualVehicle = actualVehicle;
        this.expected = expected;
    }

    public static TestCaseWithMultipleDatesBuilder newWithoutHeader()
    {
        return new TestCaseWithMultipleDatesBuilder(null, null,
                                                    TestData.A_NON_FREE_VEHICLE,
                                                    -1);
    }

    public Object[] build(Date... actualDates)
    {
        return new Object[]{
                new TestCaseWithMultipleDates(name(),
                                              configuration,
                                              actualVehicle,
                                              actualDates,
                                              expected)
        };
    }

    public TestCaseWithMultipleDatesBuilder withFeeForTimeOfDaySpecification(FeeForTimeOfDaySpecification feeForTimeOfDay)
    {
        this.configuration.feeForTimeOfDay = feeForTimeOfDay;
        return this;
    }

    public TestCaseWithMultipleDatesBuilder withIsHolidaySpecification(Predicate<Day> isTollFreeDay)
    {
        this.configuration.isHoliday = isTollFreeDay;
        return this;
    }

    public TestCaseWithMultipleDatesBuilder withIsTollFreeVehicleSpecification(Predicate<Vehicle> isTollFreeVehicle)
    {
        this.configuration.isTollFreeVehicle = isTollFreeVehicle;
        return this;
    }

    public TestCaseWithMultipleDatesBuilder withMaxFeePerDay(int x)
    {
        this.configuration.maxFeePerDay = x;
        return this;
    }

    public TestCaseWithMultipleDatesBuilder withMinNumMinutesBetweenCharges(int x)
    {
        this.configuration.minNumMinutesBetweenCharges = x;
        return this;
    }

    public TestCaseWithMultipleDatesBuilder withName(String nameTail)
    {
        this.nameTail = nameTail;
        return this;
    }

    public TestCaseWithMultipleDatesBuilder withVehicle(Vehicle vehicle)
    {
        this.actualVehicle = vehicle;
        return this;
    }

    public TestCaseWithMultipleDatesBuilder withExpectedFee(int expected)
    {
        this.expected = expected;
        return this;
    }
}
