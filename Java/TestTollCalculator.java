package com.evolve.tollcalculator;

import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.junit.runners.JUnit4;
import java.text.SimpleDateFormat;
import java.time.Instant;
import java.util.Date;

@RunWith(JUnit4.class)
public class TestTollCalculator {

  TollCalculator tollCalculator = new TollCalculator();
  Car car ;

  @Test
  public void testMinimumTollFee() throws Exception {
      Assert.assertEquals(8,tollCalculator.getTollFee(car,new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").parse("2021-05-05 06:00:00"),
    		  new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").parse("2021-05-05 06:20:00")));
  }
    		  
  @Test
  public void testCasualTimeTollFee() throws Exception {
      Assert.assertEquals(18,tollCalculator.getTollFee(car,new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").parse("2021-05-05 16:00:00"),
    		  new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").parse("2021-05-05 18:31:00")));
  }   		  
    		  
  @Test
  public void testTollFreeVehicles() throws Exception {
	  Assert.assertEquals(0,tollCalculator.getTollFee(new Motorbike(), Date.from(Instant.now())));
	  Assert.assertEquals(0,tollCalculator.getTollFee(new Military(), Date.from(Instant.now())));
	  Assert.assertEquals(0,tollCalculator.getTollFee(new Tractor(), Date.from(Instant.now())));
	  Assert.assertEquals(0,tollCalculator.getTollFee(new Emergency(), Date.from(Instant.now())));
	  Assert.assertEquals(0,tollCalculator.getTollFee(new Diplomat(), Date.from(Instant.now())));
	  Assert.assertEquals(0,tollCalculator.getTollFee(new Foreign(), Date.from(Instant.now())));
  
  }
  
  @Test
  public void testWeekends() throws Exception {
      Assert.assertEquals(0,tollCalculator.getTollFee(car, new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/12/04 09:00:00")));
  }

  @Test
  public void testHolidays() throws Exception {
      Assert.assertEquals(0,tollCalculator.getTollFee(car, new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").parse("2021-01-01 10:00:00")));
  }

  @Test
  public void testMaximumPricePerDay() throws Exception {
      Assert.assertEquals(60,tollCalculator.getTollFee(car,
              new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 09:00:00"),
              new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 11:00:00"),
              new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 11:30:00"),
              new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 13:30:00"),
              new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 15:30:00"),
              new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 16:30:00"),
              new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 16:00:00"),
              new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 10:05:00"),
              new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 17:30:00")));
  }

}
