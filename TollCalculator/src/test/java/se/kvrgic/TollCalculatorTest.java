package se.kvrgic;

import java.util.Date;
import static org.junit.Assert.*;

import org.junit.Test;

public class TollCalculatorTest {

    @Test
    public void getTollFeeBasic() {
        int tollFee = (new TollCalculator()).getTollFee(new Car(), new Date(114, 2, 2));
        assertEquals("Toll fee of whatever", 0, tollFee);
    }
    
    
}
