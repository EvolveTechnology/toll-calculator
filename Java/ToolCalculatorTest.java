import java.time.LocalDateTime;
import java.time.Month;
import java.time.ZoneId;
import java.util.Date;

public class ToolCalculatorTest {
    public static void main(String[] args) {
        TollCalculator tollCalculator = new TollCalculator();
        Date[] dateTimeArrayWeekDayLarge = new Date[]{
          Date.from(LocalDateTime.of(2022, Month.MARCH, 14, 7, 30).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 14, 7, 52).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 14, 8, 17).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 14, 8, 25).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 14, 8, 39).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 14, 8, 48).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 14, 11, 25).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 14, 13, 45).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 14, 16, 25).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 14, 16, 40).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 14, 17, 40).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 14, 17, 48).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 14, 18, 12).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 14, 18, 27).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 14, 18, 43).atZone(ZoneId.systemDefault()).toInstant())
        };

        Date[] dateTimeArrayWeekDaySmall = new Date[]{
          Date.from(LocalDateTime.of(2022, Month.MARCH, 14, 8, 17).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 14, 8, 25).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 14, 8, 39).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 14, 8, 48).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 14, 11, 25).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 14, 17, 40).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 14, 17, 48).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 14, 18, 12).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 14, 18, 27).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 14, 18, 43).atZone(ZoneId.systemDefault()).toInstant())
        };

        Date[] dateTimeArrayWeekEnd = new Date[]{
          Date.from(LocalDateTime.of(2022, Month.MARCH, 12, 8, 17).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 12, 8, 25).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 12, 8, 39).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 12, 8, 48).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 12, 11, 25).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 12, 17, 40).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 12, 17, 48).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 12, 18, 12).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 12, 18, 27).atZone(ZoneId.systemDefault()).toInstant()),
          Date.from(LocalDateTime.of(2022, Month.MARCH, 12, 18, 43).atZone(ZoneId.systemDefault()).toInstant())
        };

        test(tollCalculator, new Car(), dateTimeArrayWeekDayLarge, 60, "Weekday Fees on Car with Cap");
        test(tollCalculator, new Car(), dateTimeArrayWeekDaySmall, 34, "Weekday Fees on Car without Cap");
        test(tollCalculator, new Car(), dateTimeArrayWeekEnd, 0, "Weekend Fees on Car");
        test(tollCalculator, new Motorbike(), dateTimeArrayWeekEnd, 0, "Weekend Fees on Motorbike");
        test(tollCalculator, new Motorbike(), dateTimeArrayWeekDaySmall, 0, "Weekday Fees on Motorbike");
    }

    private static void test(TollCalculator tollCalculator, Vehicle vehicle, Date[] datesArray, int expectedFee, String testScenario){
        int tollFee = tollCalculator.getTollFee(vehicle, datesArray);
        if(tollFee == expectedFee){
            System.out.printf("Test '%s' executed successfully.%n", testScenario);
        } else {
            System.out.printf("Test '%s' failed. Expected fee is %d SEK and actual fee returned is %d SEK.%n", testScenario, expectedFee, tollFee);
        }
    }
}
