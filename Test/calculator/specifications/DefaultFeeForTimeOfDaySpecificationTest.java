package calculator.specifications;

import org.testng.Assert;
import org.testng.annotations.DataProvider;
import org.testng.annotations.Test;
import util.TimeOfDay;

public class DefaultFeeForTimeOfDaySpecificationTest {

    private DefaultFeeForTimeOfDaySpecification sut = new DefaultFeeForTimeOfDaySpecification();

    @Test(dataProvider = "test_cases")
    public void test(TimeOfDay timeOfDay, int expectedFee)
    {
        Assert.assertEquals(sut.feeFor(timeOfDay.hour, timeOfDay.minute),
                            expectedFee);
    }

    @DataProvider(name = "test_cases")
    public Object[][] test_cases()
    {
        TimeOfDay
                FEE_IS_8 = new TimeOfDay(6, 15, 0),
                FEE_IS_18 = new TimeOfDay(7, 30, 10),
                FEE_IS_0 = new TimeOfDay(19, 0, 0);


        return new Object[][]{
                of(FEE_IS_8, 8),
                of(FEE_IS_18, 18),
                of(FEE_IS_0, 0),
        };
    }

    private static Object[] of(TimeOfDay timeOfDay, int expectedFee)
    {
        return new Object[]{timeOfDay, expectedFee};
    }
}
