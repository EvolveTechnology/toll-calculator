package calculator;

import org.testng.Assert;
import org.testng.annotations.DataProvider;
import org.testng.annotations.Test;
import test_utils.TestCase;

import java.util.Calendar;
import java.util.Date;

import static test_utils.DateTestDataBuilder.*;
import static test_utils.TestData.aFreeVehicle;
import static test_utils.TestData.aNonFreeVehicle;

public class TollCalculatorTest {

    @Test(dataProvider = "week_end_cases")
    public void test_week_end(TestCase testCase) {
        check(testCase);
    }

    @DataProvider(name = "week_end_cases")
    public Object[][] week_end_cases() {
        return new Object[][]{
                isFeeFreeWeekend("saturday / non-free Vehicle",
                        aSaturday(), aNonFreeVehicle()
                ),
                isFeeFreeWeekend("saturday / free Vehicle",
                        aSaturday(), aFreeVehicle()
                ),
                isFeeFreeWeekend("sunday / non-free Vehicle",
                        aSunday(), aNonFreeVehicle()
                ),
                isFeeFreeWeekend("sunday / free Vehicle",
                        aSunday(), aFreeVehicle()
                ),
        };
    }

    @Test(dataProvider = "holiday_cases")
    public void test_holiday(TestCase testCase) {
        check(testCase);
    }

    @DataProvider(name = "holiday_cases")
    public Object[][] holiday_cases() {
        Date holidayAndNotWeekEndDate = timeOf(2013,
                Calendar.JANUARY,
                1,
                10,
                20,
                30
        );

        return new Object[][]{
                isFeeFreeHoliday("non-free Vehicle",
                        holidayAndNotWeekEndDate, aNonFreeVehicle()
                ),
                isFeeFreeHoliday("free Vehicle",
                        holidayAndNotWeekEndDate, aFreeVehicle()
                ),
        };
    }

    @Test
    public void non_fee_free_day_SHOULD_be_free_WHEN_vehicle_is_fee_free() {
        Date nonFreeDate = timeOf(2013,
                Calendar.JANUARY,
                2,
                10,
                20,
                30
        );
        check(new TestCase("non_fee_free_day_SHOULD_be_free_WHEN_vehicle_is_fee_free",
                nonFreeDate,
                aFreeVehicle(),
                0));

    }


    private static Object[] isFeeFreeWeekend(String name,
                                             Date actualTime,
                                             Vehicle actualVehicle) {
        return new Object[]{
                new TestCase(
                        "Fee Free Weekend : " + name,
                        actualTime,
                        actualVehicle,
                        0)
        };
    }


    private static Object[] isFeeFreeHoliday(String name,
                                             Date actualTime,
                                             Vehicle actualVehicle) {
        return new Object[]{
                new TestCase(
                        "Fee Free Holiday : " + name,
                        actualTime,
                        actualVehicle,
                        0)
        };
    }


    private void check(TestCase testCase) {
        TollCalculator calculator = new TollCalculator();

        int actual = calculator.getTollFee(testCase.actualVehicle, testCase.actualTime);

        Assert.assertEquals(testCase.expected, actual, testCase.name);
    }

}
