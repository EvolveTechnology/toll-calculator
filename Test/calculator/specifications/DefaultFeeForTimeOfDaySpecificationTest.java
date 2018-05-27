package calculator.specifications;

import org.testng.Assert;
import org.testng.annotations.DataProvider;
import org.testng.annotations.Test;
import util.TimeOfDay;

import static test_utils.TestData.*;

public class DefaultFeeForTimeOfDaySpecificationTest {

    private DefaultFeeForTimeOfDaySpecification sut = new DefaultFeeForTimeOfDaySpecification();

    @Test(dataProvider = "test_fees_of_date_with_fee_and_non_free_vehicle_cases")
    public void test(TimeOfDay timeOfDay, int expectedFee) {
        Assert.assertEquals(sut.feeFor(timeOfDay.hour, timeOfDay.minute),
                            expectedFee);
    }

    @DataProvider(name = "test_fees_of_date_with_fee_and_non_free_vehicle_cases")
    public Object[][] test_fees_of_date_with_fee_and_non_free_vehicle_cases() {
        return new Object[][]{
                of(FEE_IS_8, 8),
                of(FEE_IS_18, 18),
                of(FEE_IS_0, 0),
        };
    }

    private static Object[] of(TimeOfDay timeOfDay, int expectedFee) {
        return new Object[]{timeOfDay, expectedFee};
    }
}
