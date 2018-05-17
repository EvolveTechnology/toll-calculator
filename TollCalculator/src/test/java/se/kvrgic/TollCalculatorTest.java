package se.kvrgic;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import static org.junit.Assert.*;

import org.junit.Test;

public class TollCalculatorTest {

    @Test
    public void getTollFeeHappyCase() throws Exception {
        int tollFee = (new TollCalculator()).getTollFee(new Car(), getDate("20130205 07:14"));
        assertEquals("Toll fee of whatever", 18, tollFee);
    }
    
    
    
    private Date getDate(String date) throws ParseException {
        SimpleDateFormat dateFormatter = new SimpleDateFormat("yyyyMMdd HH:mm");
        return dateFormatter.parse(date);
    }
}
