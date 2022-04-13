package com.tollcalculator.service;

import com.tollcalculator.pojo.Car;
import com.tollcalculator.pojo.Diplomat;
import com.tollcalculator.pojo.Vehicle;
import org.junit.Assert;
import org.junit.Test;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;

import static org.junit.Assert.assertEquals;

public class TollCalculatorTest {

    private static final SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm");

    @Test
    public void testToolCalculatorService(){
        TollCalculator tollCalculator=new TollCalculator();
        Vehicle vehicle=new Car();
        Date time1 = getDateTime("2021-03-19 06:45");
        int toll = tollCalculator.getTollFee(vehicle, time1);
        assertEquals(13, toll);

        Date time2 = getDateTime("2021-03-19 07:50");
        toll = tollCalculator.getTollFee(vehicle, time1, time2);
        assertEquals(31, toll);

    }

    @Test
    public void testVehicleFreeToolCalculatorService(){
        TollCalculator tollCalculator=new TollCalculator();
        Vehicle vehicle=new Diplomat();

        Date time1 = getDateTime("2021-03-19 06:45"); //13
        int toll = tollCalculator.getTollFee(vehicle, time1);

        assertEquals(0, toll);

    }

    @Test
    public void testWeekEndToolCalculatorService(){
        TollCalculator tollCalculator=new TollCalculator();
        Vehicle vehicle=new Car();
        Date time1 = getDateTime("2021-04-10 06:45"); //13
        int toll = tollCalculator.getTollFee(vehicle, time1);

        assertEquals(0, toll);
    }

    @Test(expected = RuntimeException.class)
    public void testExceptionFlow(){
        TollCalculator tollCalculator=new TollCalculator();
        Vehicle vehicle=null;
        tollCalculator.getTollFee(vehicle,null);
    }

    @Test
    public void testSameVehicleMultipleEntriesInASameHour(){
        TollCalculator tollCalculator=new TollCalculator();
        Vehicle vehicle=new Car();

        Date time1 = getDateTime("2021-04-08 06:25");
        Date time2 = getDateTime("2021-04-08 06:55");

        int toll = tollCalculator.getTollFee(vehicle, time1,time2);

        assertEquals(13, toll);
    }

    private Date getDateTime(String dateTime) {
        try {
            return sdf.parse(dateTime);
        } catch (ParseException e) {
            e.printStackTrace();
        }
        return null;
    }
}
