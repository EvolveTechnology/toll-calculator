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

    public static TestCaseWithMultipleDatesBuilder newWithHeader(String nameHeader)
    {
        return new TestCaseWithMultipleDatesBuilder(nameHeader, null,
                                                    TestData.A_NON_FREE_VEHICLE,
                                                    -1);
    }

    public static TestCaseWithMultipleDatesBuilder newWithoutHeader()
    {
        return new TestCaseWithMultipleDatesBuilder(null, null,
                                                    TestData.A_NON_FREE_VEHICLE,
                                                    -1);
    }

    public Object[] build(Date[] actualDates)
    {
        return new Object[]{
                new TestCaseWithMultipleDates(name(),
                                              specifications,
                                              actualVehicle,
                                              actualDates,
                                              expected)
        };
    }

    public TestCaseWithMultipleDatesBuilder withFeeForTimeOfDaySpecification(FeeForTimeOfDaySpecification feeForTimeOfDay)
    {
        this.specifications.feeForTimeOfDay = feeForTimeOfDay;
        return this;
    }

    public TestCaseWithMultipleDatesBuilder withIsHolidaySpecification(Predicate<Day> isTollFreeDay)
    {
        this.specifications.isHoliday = isTollFreeDay;
        return this;
    }

    public TestCaseWithMultipleDatesBuilder withIsTollFreeVehicleSpecification(Predicate<Vehicle> isTollFreeVehicle)
    {
        this.specifications.isTollFreeVehicle = isTollFreeVehicle;
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
