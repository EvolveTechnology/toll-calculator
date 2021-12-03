package com.evolve.tollcalculator;

import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;


public class TollCalculatorMain {

	public static void main(String[] args) throws ParseException {
		System.out.println("Movie name of the GIF is Hackers ;) ");
		
		TollCalculator calculator = new TollCalculator();
        DateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
        Vehicle test = new Car();
        String dateString1 = "2013-06-26 07:00:00";
        String dateString2 = "2013-06-26 15:31:00";
        String dateString3 = "2013-06-26 15:33:00";
        String dateString4 = "2013-06-26 16:34:00";
        Date dateObject1 = sdf.parse(dateString1);
        Date dateObject2 = sdf.parse(dateString2);
        Date dateObject3 = sdf.parse(dateString3);
        Date dateObject4 = sdf.parse(dateString4);
        int value = calculator.getTollFee(test,dateObject1,dateObject2,dateObject3,dateObject4);
        System.out.println("Maximum TollFee Test: " +value);

        test = new Car();
        dateString1 = "2013-06-26 06:00:00";
        dateString2 = "2013-06-26 06:20:00";
        dateObject1 = sdf.parse(dateString1);
        dateObject2 = sdf.parse(dateString2);
        value = calculator.getTollFee(test,dateObject1,dateObject2);
        System.out.println("Minimum TollFee Test: " +value);

        test = new Motorbike();
        dateString1 = "2013-06-26 07:00:00";
        dateString2 = "2013-06-26 15:31:00";
        dateObject1 = sdf.parse(dateString1);
        dateObject2 = sdf.parse(dateString2);
        value = calculator.getTollFee(test,dateObject1,dateObject2);
        System.out.println("TollFree Test: " +value);

        test = new Car();
        dateString1 = "2013-06-21 07:00:00";
        dateString2 = "2013-06-21 15:31:00";
        dateObject1 = sdf.parse(dateString1);
        dateObject2 = sdf.parse(dateString2);
        value = calculator.getTollFee(test,dateObject1,dateObject2);
        System.out.println("Holiday Toll Test: " +value);

        test = new Car();
        dateString1 = "2013-06-15 07:00:00";
        dateString2 = "2013-06-15 15:31:00";
        dateObject1 = sdf.parse(dateString1);
        dateObject2 = sdf.parse(dateString2);
        value = calculator.getTollFee(test,dateObject1,dateObject2);
        System.out.println("Weekend TollFee Test: " +value);

        test = new Car();
        dateString1 = "2013-06-26 16:00:00";
        dateString2 = "2013-06-26 18:31:00";
        dateObject1 = sdf.parse(dateString1);
        dateObject2 = sdf.parse(dateString2);
        value = calculator.getTollFee(test,dateObject1,dateObject2);
        System.out.println("Casual Hours Test: " +value);

		
	}
}
