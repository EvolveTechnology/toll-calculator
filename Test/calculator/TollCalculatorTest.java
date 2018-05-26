package calculator;

import org.testng.Assert;
import org.testng.annotations.DataProvider;
import org.testng.annotations.Test;
import test_utils.DateTestDataBuilder;
import test_utils.TestCase;
import test_utils.TestCaseBuilder;

import static test_utils.DateTestDataBuilder.A_SATURDAY;
import static test_utils.DateTestDataBuilder.A_SUNDAY;
import static test_utils.TestData.*;

public class TollCalculatorTest {


    @Test(dataProvider = "week_end_cases")
    public void test_week_end(TestCase testCase) {
        check(testCase);
    }

    @DataProvider(name = "week_end_cases")
    public Object[][] week_end_cases() {
        TestCaseBuilder caseBuilder =
                TestCaseBuilder.newWithHeader("Week end should be free for any vehicle")
                        .withTime(FEE_IS_8)
                        .withExpectedFee(0);

        return new Object[][]{
                caseBuilder
                        .withDay(A_SATURDAY)
                        .withVehicle(A_NON_FREE_VEHICLE)
                        .named("saturday / non-free Vehicle")
                ,
                caseBuilder
                        .withVehicle(A_FREE_VEHICLE)
                        .named("saturday / free Vehicle")
                ,
                caseBuilder
                        .withDay(A_SUNDAY)
                        .withVehicle(A_NON_FREE_VEHICLE)
                        .named("sunday / non-free Vehicle")
                ,
                caseBuilder
                        .withVehicle(A_FREE_VEHICLE)
                        .named("sunday / free Vehicle")
                ,
        };
    }

    @Test(dataProvider = "holiday_cases")
    public void test_holiday(TestCase testCase) {
        check(testCase);
    }

    @DataProvider(name = "holiday_cases")
    public Object[][] holiday_cases() {
        TestCaseBuilder caseBuilder = TestCaseBuilder.newWithoutHeader()
                .withDay(HOLIDAY_DAY)
                .withTime(FEE_IS_8)
                .withExpectedFee(0);

        return new Object[][]{
                caseBuilder
                        .withVehicle(A_NON_FREE_VEHICLE)
                        .named("non-free Vehicle")
                ,
                caseBuilder
                        .withVehicle(A_FREE_VEHICLE)
                        .named("free Vehicle")
                ,
        };
    }

    @Test
    public void day_with_fee_SHOULD_be_free_WHEN_vehicle_is_free() {
        check(new TestCase(DAY_WITH_FEE, FEE_IS_8,
                A_FREE_VEHICLE,
                0));

    }

    @Test(dataProvider = "test_fees_of_date_with_fee_and_non_free_vehicle_cases")
    public void test_fees_of_date_with_fee_AND_non_free_vehicle(TestCase testCase) {
        check(testCase);
    }

    @DataProvider(name = "test_fees_of_date_with_fee_and_non_free_vehicle_cases")
    public Object[][] test_fees_of_date_with_fee_and_non_free_vehicle_cases() {
        TestCaseBuilder caseBuilder = TestCaseBuilder.newWithoutHeader()
                .withDay(DAY_WITH_FEE)
                .withVehicle(A_NON_FREE_VEHICLE);

        return new Object[][]{
                caseBuilder
                        .withTime(FEE_IS_8)
                        .withExpectedFee(8)
                        .build2(),
                caseBuilder
                        .withTime(FEE_IS_18)
                        .withExpectedFee(18)
                        .build2(),
                caseBuilder
                        .withTime(FEE_IS_0)
                        .withExpectedFee(0)
                        .build2(),
        };
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

}
