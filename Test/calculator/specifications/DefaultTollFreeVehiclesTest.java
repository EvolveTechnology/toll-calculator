package calculator.specifications;

import calculator.Vehicles;
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

    @Test
    public void motorbike_SHOULD_be_toll_free()
    {
        DefaultTollFreeVehicles sut = new DefaultTollFreeVehicles();

        boolean actual = sut.test(Vehicles.newMotorbike());

        Assert.assertTrue(actual);

    }

    @Test
    public void car_SHOULD_not_be_toll_free()
    {
        DefaultTollFreeVehicles sut = new DefaultTollFreeVehicles();

        boolean actual = sut.test(Vehicles.newCar());

        Assert.assertFalse(actual);

    }

}
