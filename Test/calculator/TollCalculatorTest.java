package calculator;

import org.testng.Assert;
import org.testng.annotations.DataProvider;
import org.testng.annotations.Test;
import test_utils.TestCase;
import test_utils.TestCaseBuilder;
import util.Day;
import util.TimeOfDay;

import java.util.Calendar;
import java.util.Date;

import static test_utils.DateTestDataBuilder.*;
import static test_utils.TestData.aFreeVehicle;
import static test_utils.TestData.aNonFreeVehicle;

public class TollCalculatorTest {

    private static final Day
            DAY_WITH_FEE = new Day(2013, Calendar.JANUARY, 2),
            HOLIDAY_DAY = new Day(2013, Calendar.JANUARY, 1);


    private static final TimeOfDay
            FEE_IS_8 = new TimeOfDay(6, 15, 0),
            FEE_IS_18 = new TimeOfDay(7, 30, 10),
            FEE_IS_0 = new TimeOfDay(19, 0, 0);

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
                        .withVehicle(aNonFreeVehicle())
                        .named("saturday / non-free Vehicle")
                ,
                caseBuilder
                        .withVehicle(aFreeVehicle())
                        .named("saturday / free Vehicle")
                ,
                caseBuilder
                        .withDay(A_SUNDAY)
                        .withVehicle(aNonFreeVehicle())
                        .named("sunday / non-free Vehicle")
                ,
                caseBuilder
                        .withVehicle(aFreeVehicle())
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
                        .withVehicle(aNonFreeVehicle())
                        .named("non-free Vehicle")
                ,
                caseBuilder
                        .withVehicle(aFreeVehicle())
                        .named("free Vehicle")
                ,
        };
    }

    @Test
    public void day_with_fee_SHOULD_be_free_WHEN_vehicle_is_free() {
        check(new TestCase(aDateWithFee(),
                aFreeVehicle(),
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
                .withVehicle(aNonFreeVehicle());

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


    private void check(TestCase testCase) {
        TollCalculator calculator = new TollCalculator();

        int actual = calculator.getTollFee(testCase.actualVehicle, testCase.actualTime);

        Assert.assertEquals(testCase.expected, actual, testCase.name);
    }

    private static Date aDateWithFee() {
        return timeOf(DAY_WITH_FEE,
                10,
                20,
                30
        );
    }

}
