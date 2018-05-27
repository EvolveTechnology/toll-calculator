package calculator.specifications;

import org.testng.Assert;
import org.testng.annotations.Test;

public class DefaultTollFreeVehiclesTest {
    @Test
    public void WHEN_vehicle_is_null_THEN_it_is_not_free()
    {
        DefaultTollFreeVehicles sut = new DefaultTollFreeVehicles();

        boolean actual = sut.test(null);

        Assert.assertFalse(actual);

    }

}
