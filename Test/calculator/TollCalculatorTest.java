package calculator;

import org.testng.Assert;
import org.testng.annotations.DataProvider;
import org.testng.annotations.Test;
import test_utils.*;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;
import java.util.List;
import java.util.function.Function;
import java.util.stream.Collectors;

import static test_utils.TestData.*;

public class TollCalculatorTest {

    @Test(dataProvider = "weekend_cases")
    public void weekend_SHOULD_be_free_for_any_vehicle(TestCase testCase)
    {
        check(testCase);
    }

    @DataProvider(name = "weekend_cases")
    public Object[][] weekend_cases()
    {
        Vehicle
                toll_free_vehicle = Vehicles.of("toll-fre"),
                non_toll_free_vehicle = Vehicles.of("non-toll-fre");

        TestCaseBuilder caseBuilder =
                TestCaseBuilder.newWithoutHeader()
                               .withFeeForTimeOfDaySpecification(constantFeeOf(1))
                               .withIsHolidaySpecification(holidayIsConstant(false))
                               .withIsTollFreeVehicleSpecification(v -> v.getType().equals(toll_free_vehicle.getType()))
                               .withTime(NOON)
                               .withExpectedFee(0);

        return new Object[][]{
                caseBuilder
                        .withNameHeader("non-free vehicle")
                        .withVehicle(non_toll_free_vehicle)
                        .withDay(SATURDAY)
                        .named("saturday")
                ,
                caseBuilder
                        .withDay(SUNDAY)
                        .named("sunday")
                ,
                caseBuilder
                        .withNameHeader("free vehicle")
                        .withVehicle(toll_free_vehicle)
                        .withDay(SATURDAY)
                        .named("saturday")
                ,
                caseBuilder
                        .withDay(SUNDAY)
                        .named("sunday")
                ,
        };
    }

    @Test(dataProvider = "holiday_cases")
    public void holiday_SHOULD_be_free_for_any_vehicle(TestCase testCase)
    {
        check(testCase);
    }

    @DataProvider(name = "holiday_cases")
    public Iterator<Object[]> holiday_cases()
    {
        Vehicle
                toll_free_vehicle = Vehicles.of("toll-fre"),
                non_toll_free_vehicle = Vehicles.of("non-toll-fre");

        TestCaseBuilder caseBuilder = TestCaseBuilder.newWithoutHeader()
                                                     .withIsHolidaySpecification(holidayIsConstant(true))
                                                     .withFeeForTimeOfDaySpecification(constantFeeOf(1))
                                                     .withIsTollFreeVehicleSpecification(v -> v.getType().equals(toll_free_vehicle.getType()))
                                                     .withTime(NOON)
                                                     .withExpectedFee(0);

        List<Object[]> cases = new ArrayList<>();

        caseBuilder
                .withNameHeader("non-free vehicle")
                .withVehicle(non_toll_free_vehicle);

        NON_WEEKEND_DAYS.forEach(
                dayNameAndValue -> cases.add(caseBuilder.withName(dayNameAndValue.name)
                                                        .withDay(dayNameAndValue.value)
                                                        .build2())
        );

        caseBuilder
                .withNameHeader("free vehicle")
                .withVehicle(toll_free_vehicle);

        NON_WEEKEND_DAYS.forEach(
                dayNameAndValue -> cases.add(caseBuilder.withName(dayNameAndValue.name)
                                                        .withDay(dayNameAndValue.value)
                                                        .build2())
        );

        return cases.iterator();
    }

    @Test(dataProvider = "WHEN_date_is_non_free_THEN_a_free_vehicle_SHOULD_not_be_charged_cases")
    public void WHEN_date_is_non_free_THEN_a_free_vehicle_SHOULD_not_be_charged(TestCase testCase)
    {
        check(testCase);
    }

    @DataProvider(name = "WHEN_date_is_non_free_THEN_a_free_vehicle_SHOULD_not_be_charged_cases")
    public Iterator<Object[]> WHEN_date_is_non_free_THEN_a_free_vehicle_SHOULD_not_be_charged_cases()
    {
        TestCaseBuilder caseBuilder = TestCaseBuilder.newWithoutHeader()
                                                     .withIsHolidaySpecification(holidayIsConstant(false))
                                                     .withFeeForTimeOfDaySpecification(constantFeeOf(1))
                                                     .withIsTollFreeVehicleSpecification(TestData.vehicleIsTollFreeIsConstant(true))
                                                     .withVehicle(RANDOM_VEHICLE)
                                                     .withTime(NOON)
                                                     .withExpectedFee(0);

        return map(NON_WEEKEND_DAYS,
                   dayNameAndValue -> caseBuilder.withName(dayNameAndValue.name)
                                                 .withDay(dayNameAndValue.value)
                                                 .build2()
        );
    }

    @Test
    public void WHEN_multiple_dates_THEN_fees_SHOULD_be_sum_of_charges_for_every_date()
    {
        DateTestDataBuilder dateBuilder = new DateTestDataBuilder(MONDAY);

        check(TestCaseWithMultipleDatesBuilder.newWithoutHeader()
                                              .withIsTollFreeVehicleSpecification(TestData.vehicleIsTollFreeIsConstant(false))
                                              .withMaxFeePerDay(Integer.MAX_VALUE)
                                              .withMinNumMinutesBetweenCharges(60)
                                              .withFeeForTimeOfDaySpecification(feeIsSameAsMinute())
                                              .withExpectedFee(20 + 30)
                                              .buildTestCase(dateBuilder.withTime(10, 20, 0).build(),
                                                             dateBuilder.withTime(11, 30, 0).build()));
    }

    @Test(dataProvider = "only_charge_for_first_date_within_charge_interval_cases")
    public void a_vehicle_SHOULD_only_be_charged_for_first_date_within_charge_interval(TestCaseWithMultipleDates testCase)
    {
        check(testCase);
    }

    @DataProvider
    public Object[][] only_charge_for_first_date_within_charge_interval_cases()
    {
        DateTestDataBuilder dateBuilder = new DateTestDataBuilder(MONDAY);

        TestCaseWithMultipleDatesBuilder caseBuilder = TestCaseWithMultipleDatesBuilder.newWithoutHeader()
                                                                                       .withIsHolidaySpecification(holidayIsConstant(false))
                                                                                       .withIsTollFreeVehicleSpecification(vehicleIsTollFreeIsConstant(false))
                                                                                       .withMaxFeePerDay(Integer.MAX_VALUE)
                                                                                       .withMinNumMinutesBetweenCharges(30)
                                                                                       .withFeeForTimeOfDaySpecification(feeIsSameAsMinute());

        return new Object[][]{
                caseBuilder
                        .withName("WHEN 2nd date is withing same interval THEN only 1st date SHOULD be charged")
                        .withExpectedFee(2)
                        .build(dateBuilder.buildForTime(10, 2),
                               dateBuilder.buildForTime(10, 10))
                ,
                caseBuilder
                        .withName("WHEN 2nd date is withing same interval THEN only 1st date SHOULD be charged (different hours)")
                        .withExpectedFee(50)
                        .build(dateBuilder.buildForTime(10, 50),
                               dateBuilder.buildForTime(11, 10))
                ,
                caseBuilder
                        .withName("WHEN 2nd, 3rd date is withing same interval THEN only 1st date SHOULD be charged")
                        .withExpectedFee(2)
                        .build(dateBuilder.buildForTime(10, 2),
                               dateBuilder.buildForTime(10, 10),
                               dateBuilder.buildForTime(10, 20))
                ,
                caseBuilder
                        .withName("WHEN 2nd date is withing same interval but 3rd is not THEN 1st & 3rd dates SHOULD be charged")
                        .withExpectedFee(30 + 5)
                        .build(dateBuilder.buildForTime(10, 30),
                               dateBuilder.buildForTime(10, 40),
                               dateBuilder.buildForTime(11, 5))
                ,
                caseBuilder
                        .withName("WHEN 2nd & 4th date is withing same interval but 3rd is not THEN 1st & 3rd dates SHOULD be charged")
                        .withExpectedFee(32 + 5)
                        .build(dateBuilder.buildForTime(10, 32),
                               dateBuilder.buildForTime(10, 40),
                               dateBuilder.buildForTime(11, 5),
                               dateBuilder.buildForTime(11, 30))
                ,
                caseBuilder
                        .withName("WHEN 2nd & 4th date is withing same interval but 3rd is not THEN 1st & 3rd dates SHOULD be charged (with limit)")
                        .withMaxFeePerDay(33)
                        .withExpectedFee(Integer.min(33, 32 + 5))
                        .build(dateBuilder.buildForTime(10, 32),
                               dateBuilder.buildForTime(10, 40),
                               dateBuilder.buildForTime(11, 5),
                               dateBuilder.buildForTime(11, 30))
        };
    }

    @Test(dataProvider = "dates_with_zero_charge_SHOULD_not_count_as_point_in_charge_interval_cases")
    public void dates_with_zero_charge_SHOULD_not_count_as_point_in_charge_interval(TestCaseWithMultipleDates testCase)
    {
        check(testCase);
    }

    @DataProvider
    public Object[][] dates_with_zero_charge_SHOULD_not_count_as_point_in_charge_interval_cases()
    {
        DateTestDataBuilder dateBuilder = new DateTestDataBuilder(MONDAY);

        TestCaseWithMultipleDatesBuilder caseBuilder = TestCaseWithMultipleDatesBuilder.newWithoutHeader()
                                                                                       .withIsHolidaySpecification(holidayIsConstant(false))
                                                                                       .withIsTollFreeVehicleSpecification(vehicleIsTollFreeIsConstant(false))
                                                                                       .withMaxFeePerDay(Integer.MAX_VALUE)
                                                                                       .withMinNumMinutesBetweenCharges(60)
                                                                                       .withFeeForTimeOfDaySpecification(feeIsSameAsMinuteWhenMinuteIsEvenElseZero());

        return new Object[][]{
                caseBuilder
                        .withName("initial zero charge SHOULD be ignored")
                        .withExpectedFee(10)
                        .build(dateBuilder.buildForTime(10, 1),
                               dateBuilder.buildForTime(10, 10))
                ,
                caseBuilder
                        .withName("initial zero chargeS SHOULD be ignored")
                        .withExpectedFee(10)
                        .build(dateBuilder.buildForTime(10, 1),
                               dateBuilder.buildForTime(10, 51),
                               dateBuilder.buildForTime(11, 10))
                ,
                caseBuilder
                        .withName("initial zero charge SHOULD be ignored and following interval should count from 1st non-zero point")
                        .withExpectedFee(50 + 52)
                        .build(dateBuilder.buildForTime(10, 1),
                               dateBuilder.buildForTime(10, 50),
                               dateBuilder.buildForTime(11, 10),
                               dateBuilder.buildForTime(11, 52))
                ,
                caseBuilder
                        .withName("initial zero charge SHOULD be ignored and following interval should count from 1st non-zero point")
                        .withExpectedFee(10 + 20)
                        .build(dateBuilder.buildForTime(10, 31),
                               dateBuilder.buildForTime(11, 10),
                               dateBuilder.buildForTime(11, 51),
                               dateBuilder.buildForTime(12, 20))
        };
    }

    @Test(dataProvider = "charge_for_multiple_dates_SHOULD_be_sum_of_charge_for_individual_dates_cases")
    public void charge_for_multiple_dates_SHOULD_be_sum_of_charge_for_individual_dates(TestCaseWithMultipleDates testCase)
    {
        check(testCase);
    }

    @DataProvider
    public Object[][] charge_for_multiple_dates_SHOULD_be_sum_of_charge_for_individual_dates_cases()
    {
        TestCaseWithMultipleDatesBuilder caseBuilder =
                TestCaseWithMultipleDatesBuilder.newWithoutHeader()
                                                .withIsHolidaySpecification(holidayIsConstant(false))
                                                .withIsTollFreeVehicleSpecification(vehicleIsTollFreeIsConstant(false))
                                                .withVehicle(RANDOM_VEHICLE)
                                                .withMaxFeePerDay(Integer.MAX_VALUE)
                                                .withMinNumMinutesBetweenCharges(1)
                                                .withFeeForTimeOfDaySpecification(feeIsSameAsMinute());

        DateTestDataBuilder dateBuilder =
                new DateTestDataBuilder(MONDAY)
                        .withTime(NOON);

        return new Object[][]{
                caseBuilder
                        .withName("Two times")
                        .withExpectedFee(5)
                        .build(dateBuilder.withMinute(1).build(),
                               dateBuilder.withMinute(4).build())
                ,
                caseBuilder
                        .withName("Three times, separate by hours")
                        .withExpectedFee(55)
                        .build(dateBuilder.withMinute(5).build(),
                               dateBuilder.withNextHour()
                                          .withMinute(20).build(),
                               dateBuilder.withMinute(30).build())
                ,
        };
    }

    @Test(dataProvider = "WHEN_sum_of_individual_charges_exceed_maximum_charge_THEN_result_SHOULD_be_maximum_change_cases")
    public void WHEN_sum_of_individual_charges_exceed_maximum_charge_THEN_result_SHOULD_be_maximum_change(TestCaseWithMultipleDates testCase)
    {
        check(testCase);
    }

    @DataProvider
    public Object[][] WHEN_sum_of_individual_charges_exceed_maximum_charge_THEN_result_SHOULD_be_maximum_change_cases()
    {
        TestCaseWithMultipleDatesBuilder caseBuilder =
                TestCaseWithMultipleDatesBuilder.newWithoutHeader()
                                                .withIsHolidaySpecification(holidayIsConstant(false))
                                                .withIsTollFreeVehicleSpecification(vehicleIsTollFreeIsConstant(false))
                                                .withVehicle(RANDOM_VEHICLE)
                                                .withMinNumMinutesBetweenCharges(1)
                                                .withFeeForTimeOfDaySpecification(feeIsSameAsMinute())
                                                .withMaxFeePerDay(20);

        DateTestDataBuilder dateBuilder =
                new DateTestDataBuilder(MONDAY)
                        .withTime(NOON);

        return new Object[][]{
                caseBuilder
                        .withName("Sum that is higher than maximum")
                        .withExpectedFee(20)
                        .build(dateBuilder.withMinute(5).build(),
                               dateBuilder.withNextHour()
                                          .withMinute(20).build(),
                               dateBuilder.withMinute(30).build())
                ,
                caseBuilder
                        .withName("Single charge that is higher than maximum")
                        .withFeeForTimeOfDaySpecification(constantFeeOf(1000))
                        .withExpectedFee(20)
                        .build(dateBuilder.build())
                ,
        };
    }

    private void check(TestCase testCase)
    {
        TollCalculator calculator = new TollCalculator(testCase.configuration);

        Assert.assertEquals(
                calculator.getTollFee(testCase.actualTime, testCase.actualVehicle),
                testCase.expected,
                "Method for single date: " + testCase.name);

        Assert.assertEquals(calculator.getTollFee(testCase.actualVehicle, testCase.actualTime),
                            testCase.expected,
                            "Method for multiple dates: " + testCase.name);
    }

    private void check(TestCaseWithMultipleDates testCase)
    {
        TollCalculator calculator = new TollCalculator(testCase.configuration);

        Assert.assertEquals(calculator.getTollFee(testCase.actualVehicle, testCase.actualTimes),
                            testCase.expected,
                            testCase.name);
    }

    private static <T, U> Iterator<U> map(Collection<T> collection, Function<T, U> mapper)
    {
        return collection
                .stream()
                .map(mapper)
                .collect(Collectors.toList())
                .iterator();

    }
}
