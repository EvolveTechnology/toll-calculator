package com.evolve.tollCalculator;

import com.evolve.tollCalculator.model.Car;
import com.evolve.tollCalculator.model.FreeCarTypeFactory;
import com.evolve.tollCalculator.model.TollFreeVehicles;
import com.evolve.tollCalculator.util.Constants;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvSource;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;

import static org.junit.jupiter.api.Assertions.*;

class TollCalculatorTest {

    private static final Logger logger = LogManager.getLogger(TollCalculatorTest.class);
    private TollCalculator calculator = new TollCalculator();


    @Test
    void getTotalTollFee() {

        SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd HH:mm");
        try {
            //Multiple dates but one hour period
            assertEquals(18,calculator.getTotalTollFeePerDay(new Car(),
                    formatter.parse("2013-11-18 07:45"),
                    formatter.parse("2013-11-18 08:10"),
                    formatter.parse("2013-11-18 08:30")));

            //Multiple dates and multiple hour period.
            assertEquals(44,calculator.getTotalTollFeePerDay(new Car(),
                    formatter.parse("2013-11-18 07:45"),
                    formatter.parse("2013-11-18 08:10"),
                    formatter.parse("2013-11-18 08:30"),
                    formatter.parse("2013-11-18 15:00"),
                    formatter.parse("2013-11-18 17:59"),
                    formatter.parse("2013-11-18 18:58")
                    ));

            //Dates is empty
            assertEquals(0,calculator.getTotalTollFeePerDay(new Car()));

            //One date
            assertEquals(8,calculator.getTotalTollFeePerDay(new Car(),
                    formatter.parse("2013-11-18 08:30")));

            //Free car
            assertEquals(0,calculator.getTotalTollFeePerDay(
                    FreeCarTypeFactory.initVehicleByType(Constants.VEHICLE_TYPE_MILITARY),
                    formatter.parse("2013-11-18 08:30")));

        } catch (ParseException e) {
            e.printStackTrace();
        }
    }

    @ParameterizedTest
    @CsvSource({
            //for hours
            "2013-01-16 00:00,0",
            "2013-01-16 00:12,0",
            "2013-01-16 05:59,0",
            "2013-01-16 06:12,8",
            "2013-01-16 06:45,13",
            "2013-01-16 07:00,18",
            "2013-01-16 08:21,13",
            "2013-01-16 08:45,8",
            "2013-01-16 15:12,13",
            "2013-01-16 15:45,18",
            "2013-01-16 17:37,13",
            "2013-01-16 18:12,8",
            "2013-01-16 18:45,0",
            //for holiday
            "2013-01-01 17:37,0",
            "2013-04-30 17:37,0",
            "2013-12-01 17:37,0",
            "2013-12-25 17:37,0",
    })
    void testGetTollFee(String dateString, int fee) {

        SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd HH:mm");

        try {
            Date date = formatter.parse(dateString);
            logger.info("Date {} expected fee is {}", date, fee);
            assertEquals(calculator.getTollFee(date, new Car()), fee);

        } catch (ParseException e) {
            e.printStackTrace();
        }


    }
}