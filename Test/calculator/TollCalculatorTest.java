package calculator;

import org.testng.Assert;
import org.testng.annotations.DataProvider;
import org.testng.annotations.Test;

import java.util.Date;

import static test_data.TestData.*;

@Test
public class TollCalculatorTest {

    static class TestCase {
        final String name;
        final Date actualTime;
        final Vehicle actualVehicle;
        final int expected;

        TestCase(String name, Date actualTime, Vehicle actualVehicle, int expected) {
            this.name = name;
            this.actualTime = actualTime;
            this.actualVehicle = actualVehicle;
            this.expected = expected;
        }
    }

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


    private void check(TestCase testCase) {
        TollCalculator calculator = new TollCalculator();

        int actual = calculator.getTollFee(testCase.actualVehicle, testCase.actualTime);

        Assert.assertEquals(testCase.expected, actual, testCase.name);
    }

}
