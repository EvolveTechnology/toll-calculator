package se.kvrgic;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Arrays;
import java.util.Date;
import java.util.List;
import java.util.stream.Collector;
import java.util.stream.Collectors;

import static org.junit.Assert.*;

import org.junit.Test;

public class TollCalculatorTest {

    @Test public void getTollFeeHappyCase() throws Exception {
        assertEquals("Toll fee of whatever", 18, getTollForDates("20130205 07:14"));
    }
    @Test public void getTollFee_twoPassingsClose() throws Exception {
        assertEquals("Toll fee simple", 18, getTollForDates("20130205 07:14", "20130205 07:24"));
    }
    @Test public void getTollFee_maxesOut() throws Exception {
        assertEquals("Toll fee double.", 60, getTollForDates("20130205 07:07", 
                                                             "20130205 08:08", 
                                                             "20130205 09:09", 
                                                             "20130205 14:14", 
                                                             "20130205 15:15", 
                                                             "20130205 16:16", 
                                                             "20130205 17:17"));
    }
    @Test public void getTollFee_choosesTheMoreExpensiveOne_lowToHigh() throws Exception {
        assertEquals("Toll fee", 13, getTollForDates("20130205 06:29", "20130205 06:31"));
    }
    @Test public void getTollFee_choosesTheMoreExpensiveOne_lowToHighToHigher() throws Exception {
        assertEquals("Toll fee", 18, getTollForDates("20130205 06:29", 
                                                     "20130205 06:31", 
                                                     "20130205 07:01"));
    }
    @Test public void getTollFee_choosesTheMoreExpensiveOne_highToLow() throws Exception {
        assertEquals("Toll fee", 13, getTollForDates("20130205 17:59", "20130205 18:01"));
    }
    @Test public void getTollFee_choosesTheMoreExpensiveOne_multiperiod() throws Exception {
        assertEquals("Toll fee", 13+18, getTollForDates("20130205 06:29", 
                                                        "20130205 06:31", 
                                                        "20130205 15:29", 
                                                        "20130205 15:31"));
    }
    
    
    @Test public void getTollFee_0545() throws Exception {
        assertEquals("Ingen tull", 0, getTollForDates("20130205 05:45"));;
    }
    @Test public void getTollFee_0600() throws Exception {
        assertEquals("Tull", 8, getTollForDates("20130205 06:00"));
    }
    @Test public void getTollFee_0629() throws Exception {
        assertEquals("Tull", 8, getTollForDates("20130205 06:29"));
    }
    @Test public void getTollFee_0630() throws Exception {
        assertEquals("Tull", 13, getTollForDates("20130205 06:30"));
    }
    @Test public void getTollFee_0659() throws Exception {
        assertEquals("Tull", 13, getTollForDates("20130205 06:59"));
    }
    @Test public void getTollFee_0700() throws Exception {
        assertEquals("Tull", 18, getTollForDates("20130205 07:00"));
    }
    @Test public void getTollFee_0759() throws Exception {
        assertEquals("Tull", 18, getTollForDates("20130205 07:59"));
    }
    @Test public void getTollFee_0800() throws Exception {
        assertEquals("Tull", 13, getTollForDates("20130205 08:00"));
    }
    @Test public void getTollFee_0829() throws Exception {
        assertEquals("Tull", 13, getTollForDates("20130205 08:29"));
    }
    @Test public void getTollFee_0830() throws Exception {
        assertEquals("Tull", 8, getTollForDates("20130205 08:30"));
    }
    @Test public void getTollFee_0915() throws Exception {
        assertEquals("Tull", 8, getTollForDates("20130205 09:15"));
    }
    @Test public void getTollFee_1459() throws Exception {
        assertEquals("Tull", 8, getTollForDates("20130205 14:59"));
    }
    @Test public void getTollFee_1500() throws Exception {
        assertEquals("Tull", 13, getTollForDates("20130205 15:00"));
    }
    @Test public void getTollFee_1529() throws Exception {
        assertEquals("Tull", 13, getTollForDates("20130205 15:29"));
    }
    @Test public void getTollFee_1530() throws Exception {
        assertEquals("Tull", 18, getTollForDates("20130205 15:30"));
    }
    @Test public void getTollFee_1559() throws Exception {
        assertEquals("Tull", 18, getTollForDates("20130205 15:59"));
    }
    @Test public void getTollFee_1601() throws Exception {
        assertEquals("Tull", 18, getTollForDates("20130205 16:01"));
    }
    @Test public void getTollFee_1659() throws Exception {
        assertEquals("Tull", 18, getTollForDates("20130205 16:59"));
    }
    @Test public void getTollFee_1700() throws Exception {
        assertEquals("Tull", 13, getTollForDates("20130205 17:00"));
    }
    @Test public void getTollFee_1759() throws Exception {
        assertEquals("Tull", 13, getTollForDates("20130205 17:59"));
    }
    @Test public void getTollFee_1800() throws Exception {
        assertEquals("Tull", 8, getTollForDates("20130205 18:00"));
    }
    @Test public void getTollFee_1829() throws Exception {
        assertEquals("Tull", 8, getTollForDates("20130205 18:29"));
    }
    @Test public void getTollFee_1830() throws Exception {
        assertEquals("Ingen tull", 0, getTollForDates("20130205 18:30"));
    }
    @Test public void getTollFee_2200() throws Exception {
        assertEquals("Ingen tull", 0, getTollForDates("20130205 22:00"));
    }
    

    private Date getDate(String date) {
        SimpleDateFormat dateFormatter = new SimpleDateFormat("yyyyMMdd HH:mm");
        try {
            return dateFormatter.parse(date);
        } catch (ParseException e) {
            System.err.println("Kunde inte parse:a " + date);
            return null;
        }
    }
    
    private int getTollForDates(String ... dateStrings)  throws ParseException {
        TollCalculator tollCalculator = new TollCalculator();
        List<Date> dates = Arrays.asList(dateStrings).stream().map(dateString -> getDate(dateString)).collect(Collectors.toList());
        return tollCalculator.getTollFee(new Car(), dates.toArray(new Date[dates.size()]));
    }
}
