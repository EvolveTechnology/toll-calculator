package calculator.specifications;

import calculator.FeeForTimeOfDaySpecification;

/**
 * Gives the fee for a given time of day.
 */
public class DefaultFeeForTimeOfDaySpecification implements FeeForTimeOfDaySpecification {
    /**
     * @param hour   in [0,23]
     * @param minute in [0,59]
     * @return >= 0
     */
    public int feeFor(int hour, int minute)
    {
        if (hour == 6 && minute >= 0 && minute <= 29) return 8;
        else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
        else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
        else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
        else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
        else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
        else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
        else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
        else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
        else return 0;
    }
}
