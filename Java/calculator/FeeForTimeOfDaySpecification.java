package calculator;

/**
 * Gives the fee for a given time of day.
 */
public interface FeeForTimeOfDaySpecification {
    /**
     * @param hour   in [0,23]
     * @param minute in [0,59]
     * @return >= 0
     */
    int feeFor(int hour, int minute);
}
