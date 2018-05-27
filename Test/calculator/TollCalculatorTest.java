package calculator;

import org.testng.Assert;
import org.testng.annotations.DataProvider;
import org.testng.annotations.Test;
import test_utils.*;

import java.util.*;
import java.util.function.Function;
import java.util.stream.Collectors;

import static test_utils.DateTestDataBuilder.A_SATURDAY;
import static test_utils.DateTestDataBuilder.A_SUNDAY;
import static test_utils.TestData.*;

public class TollCalculatorTest {

    @Test(dataProvider = "weekend_cases")
    public void weekend_SHOULD_be_free_for_any_vehicle(TestCase testCase) {
        check(testCase);
    }

    @DataProvider(name = "weekend_cases")
    public Object[][] weekend_cases() {
        TestCaseBuilder caseBuilder =
                TestCaseBuilder.newWithoutHeader()
                               .withFeeForTimeOfDaySpecification(constantFeeOf(1))
                               .withIsHolidaySpecification(holidayIsConstant(false))
                               .withTime(NOON)
                               .withExpectedFee(0);

        return new Object[][]{
                caseBuilder
                        .withNameHeader("non-free vehicle")
                        .withVehicle(A_NON_FREE_VEHICLE)
                        .withDay(A_SATURDAY)
                        .named("saturday")
                ,
                caseBuilder
                        .withDay(A_SUNDAY)
                        .named("sunday")
                ,
                caseBuilder
                        .withNameHeader("free vehicle")
                        .withVehicle(A_FREE_VEHICLE)
                        .withDay(A_SATURDAY)
                        .named("saturday")
                ,
                caseBuilder
                        .withDay(A_SUNDAY)
                        .named("sunday")
                ,
        };
    }

    @Test(dataProvider = "holiday_cases")
    public void holiday_SHOULD_be_free_for_any_vehicle(TestCase testCase) {
        check(testCase);
    }

    @DataProvider(name = "holiday_cases")
    public Iterator<Object[]> holiday_cases() {
        TestCaseBuilder caseBuilder = TestCaseBuilder.newWithoutHeader()
                                                     .withIsHolidaySpecification(holidayIsConstant(true))
                                                     .withFeeForTimeOfDaySpecification(constantFeeOf(1))
                                                     .withTime(NOON)
                                                     .withExpectedFee(0);

        List<Object[]> cases = new ArrayList<>();

        caseBuilder
                .withNameHeader("non-free vehicle")
                .withVehicle(A_NON_FREE_VEHICLE);

        NON_WEEKEND_DAYS.forEach(
                dayNameAndValue -> cases.add(caseBuilder.withName(dayNameAndValue.name)
                                                        .withDay(dayNameAndValue.value)
                                                        .build2())
        );

        caseBuilder
                .withNameHeader("free vehicle")
                .withVehicle(A_FREE_VEHICLE);

        NON_WEEKEND_DAYS.forEach(
                dayNameAndValue -> cases.add(caseBuilder.withName(dayNameAndValue.name)
                                                        .withDay(dayNameAndValue.value)
                                                        .build2())
        );

        return cases.iterator();
    }

    @Test(dataProvider = "WHEN_date_is_non_free_THEN_a_free_vehicle_SHOULD_not_be_charged_cases")
    public void WHEN_date_is_non_free_THEN_a_free_vehicle_SHOULD_not_be_charged(TestCase testCase) {
        check(testCase);
    }

    @DataProvider(name = "WHEN_date_is_non_free_THEN_a_free_vehicle_SHOULD_not_be_charged_cases")
    public Iterator<Object[]> WHEN_date_is_non_free_THEN_a_free_vehicle_SHOULD_not_be_charged_cases() {
        TestCaseBuilder caseBuilder = TestCaseBuilder.newWithoutHeader()
                                                     .withIsHolidaySpecification(holidayIsConstant(false))
                                                     .withFeeForTimeOfDaySpecification(constantFeeOf(1))
                                                     .withVehicle(A_FREE_VEHICLE)
                                                     .withTime(NOON)
                                                     .withExpectedFee(0);

        return map(NON_WEEKEND_DAYS,
                   dayNameAndValue -> caseBuilder.withName(dayNameAndValue.name)
                                                 .withDay(dayNameAndValue.value)
                                                 .build2()
        );
    }

    @Test
    public void WHEN_multiple_times_THEN_fees_SHOULD_be_summed() {
        // GIVEN //

        DateTestDataBuilder dateBuilder = new DateTestDataBuilder(DAY_WITH_FEE);

        TollCalculator calculator = new TollCalculator();

        // WHEN  //

        int actual = calculator.getTollFee(A_NON_FREE_VEHICLE,
                                           dateBuilder.withTime(FEE_IS_8).build(),
                                           dateBuilder.withTime(FEE_IS_18).build());

        // THEN //

        Assert.assertEquals(actual, 8 + 18);
    }

    @Test(dataProvider = "vehicle_should_only_be_charged_once_an_hour_cases")
    public void a_vehicle_should_only_be_charged_once_an_hour(TestCaseWithMultipleDates testCase) {
        check(testCase);
    }

    @DataProvider(name = "vehicle_should_only_be_charged_once_an_hour_cases")
    public Object[][] vehicle_should_only_be_charged_once_an_hour_cases() {
        DateTestDataBuilder dateBuilder = new DateTestDataBuilder(DAY_WITH_FEE);

        TestCaseWithMultipleDatesBuilder caseBuilder = TestCaseWithMultipleDatesBuilder.newWithoutHeader()
                                                                                       .withVehicle(A_NON_FREE_VEHICLE);

        return new Object[][]{
                caseBuilder
                        .withName("WHEN first date is free THEN second date SHOULD be charged even when closer than 1 h")
                        .withExpectedFee(8)
                        .build(new Date[]{
                        dateBuilder.buildForTime(5, 59, 0),
                        dateBuilder.buildForTime(6, 0, 0),
                })
                ,
                caseBuilder
                        .withName("WHEN first date is non-free THEN second date SHOULD not be charged when closer than 1 h")
                        .withExpectedFee(13)
                        .build(new Date[]{
                        dateBuilder.buildForTime(6, 0, 0),
                        dateBuilder.buildForTime(6, 59, 0),
                })
                ,
                // bug:
//                caseBuilder
//                        .withName("WHEN 1st .. >1h .. 2nd .. <1h .. 3rd THEN only 1st and 2nd should be charged")
//                        .withExpectedFee(8 + 13)
//                        .build(new Date[]{
//                        dateBuilder.buildForTime(6, 0, 0),
//                        dateBuilder.buildForTime(8, 0, 0),
//                        dateBuilder.buildForTime(8, 1, 0),
//                })
//                ,
        };
    }


    private void check(TestCase testCase) {
        TollCalculator calculator = new TollCalculator();

        Assert.assertEquals(
                calculator.getTollFee(testCase.actualTime, testCase.actualVehicle),
                testCase.expected,
                "Method for single date: " + testCase.name);

        Assert.assertEquals(calculator.getTollFee(testCase.actualVehicle, testCase.actualTime),
                            testCase.expected,
                            "Method for multiple dates: " + testCase.name);
    }

    private void check(TestCaseWithMultipleDates testCase) {
        TollCalculator calculator = new TollCalculator();

        Assert.assertEquals(calculator.getTollFee(testCase.actualVehicle, testCase.actualTimes),
                            testCase.expected,
                            testCase.name);
    }

    private static <T, U> Iterator<U> map(Collection<T> collection, Function<T, U> mapper) {
        return collection
                .stream()
                .map(mapper)
                .collect(Collectors.toList())
                .iterator();

    }
}
