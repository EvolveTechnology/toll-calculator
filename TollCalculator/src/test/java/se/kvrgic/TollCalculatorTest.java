package se.kvrgic;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.time.DayOfWeek;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.time.temporal.ChronoUnit;
import java.util.Arrays;
import java.util.Date;
import java.util.List;
import java.util.stream.Collector;
import java.util.stream.Collectors;
import java.util.stream.Stream;

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
    

    @Test public void getTollFee_Car() {
        assertEquals("Tull", 8, new TollCalculator().getTollFee(new Car(), getDate("20130205 06:00")));
    }
    @Test public void getTollFee_Motorbike() {
        assertEquals("Ingen tull", 0, new TollCalculator().getTollFee(new Motorbike(), getDate("20130205 06:00")));
    }
    @Test public void getTollFee_Tractor() {
        assertEquals("Ingen tull", 0, new TollCalculator().getTollFee(getVehicle("Tractor"), getDate("20130205 06:00")));
    }
    @Test public void getTollFee_Emergency() {
        assertEquals("Ingen tull", 0, new TollCalculator().getTollFee(getVehicle("Emergency"), getDate("20130205 06:00")));
    }
    @Test public void getTollFee_Diplomat() {
        assertEquals("Ingen tull", 0, new TollCalculator().getTollFee(getVehicle("Diplomat"), getDate("20130205 06:00")));
    }
    @Test public void getTollFee_Foreign() {
        assertEquals("Ingen tull", 0, new TollCalculator().getTollFee(getVehicle("Foreign"), getDate("20130205 06:00")));
    }
    @Test public void getTollFee_Military() {
        assertEquals("Ingen tull", 0, new TollCalculator().getTollFee(getVehicle("Military"), getDate("20130205 06:00")));
    }
    

    @Test public void getTollFee_RedDatesApartFromWeekends2013() {
        DateTimeFormatter dateFormatter = DateTimeFormatter.ofPattern("yyyyMMdd");
        LocalDate startdatum = LocalDate.parse("20130101", dateFormatter);
        LocalDate slutdatum  = LocalDate.parse("20131231", dateFormatter);
        
        String tollFreeDates = Stream.iterate(startdatum, datum -> datum.plusDays(1))
                                              .limit(ChronoUnit.DAYS.between(startdatum, slutdatum) + 1)
                                              .filter(datum -> datum.getYear() == 2013)
                                              .filter(datum -> !DayOfWeek.SATURDAY.equals(datum.getDayOfWeek()))
                                              .filter(datum -> !DayOfWeek.SUNDAY  .equals(datum.getDayOfWeek()))
                                              .filter(datum -> 0 == getTollForDates(dateFormatter.format(datum) + " 06:00"))
                                              .map(datum -> dateFormatter.format(datum))
                                              .collect(Collectors.joining(","));
        assertEquals("Ingen tull", 
                     "20130101,20130328,20130329,20130401,20130430,20130501,20130508,20130509,20130605,20130606,20130621,20130701,20130702,20130703,20130704,20130705,20130708,20130709,20130710,20130711,20130712,20130715,20130716,20130717,20130718,20130719,20130722,20130723,20130724,20130725,20130726,20130729,20130730,20130731,20131101,20131224,20131225,20131226,20131231", 
                     tollFreeDates);
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
    
    private int getTollForDates(String ... dateStrings) {
        TollCalculator tollCalculator = new TollCalculator();
        List<Date> dates = Arrays.asList(dateStrings).stream().map(dateString -> getDate(dateString)).collect(Collectors.toList());
        return tollCalculator.getTollFee(new Car(), dates.toArray(new Date[dates.size()]));
    }
    
    private static Vehicle getVehicle(String type) {
        return new Vehicle() {
            @Override
            public String getType() {
                return type;
            }
        };
    }
}
