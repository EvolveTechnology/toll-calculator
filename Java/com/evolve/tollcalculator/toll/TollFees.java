package com.evolve.tollcalculator.toll;

import java.time.LocalTime;
import java.util.List;

import static java.util.Arrays.asList;

/**
 * For the relatively small amount of fees/intervals, I thought this was an OK solution.
 * Would there be lots of fees with more complex intervals I would probably do it another way.
 */
public final class TollFees {
    private final static List<TollFee> FEES = asList(
            new TollFee.Builder(8)
                    .withInterval(
                            new TollFeeInterval.Builder()
                                    .from(LocalTime.of(6, 0))
                                    .to(LocalTime.of(6, 29))
                                    .build()
                    )
                    .withInterval(
                            new TollFeeInterval.Builder()
                                    .from(LocalTime.of(8, 30))
                                    .to(LocalTime.of(14, 59))
                                    .build()
                    )
                    .withInterval(
                            new TollFeeInterval.Builder()
                                    .from(LocalTime.of(18, 0))
                                    .to(LocalTime.of(18, 29))
                                    .build()
                    )
                    .build(),
            new TollFee.Builder(13)
                    .withInterval(
                            new TollFeeInterval.Builder()
                                    .from(LocalTime.of(6, 30))
                                    .to(LocalTime.of(6, 59))
                                    .build()
                    )
                    .withInterval(
                            new TollFeeInterval.Builder()
                                    .from(LocalTime.of(8, 0))
                                    .to(LocalTime.of(8, 29))
                                    .build()
                    )
                    .withInterval(
                            new TollFeeInterval.Builder()
                                    .from(LocalTime.of(15, 0))
                                    .to(LocalTime.of(15, 59))
                                    .build()
                    )
                    .withInterval(
                            new TollFeeInterval.Builder()
                                    .from(LocalTime.of(17, 0))
                                    .to(LocalTime.of(17, 59))
                                    .build()
                    )
                    .build(),
            new TollFee.Builder(18)
                    .withInterval(
                            new TollFeeInterval.Builder()
                                    .from(LocalTime.of(7, 0))
                                    .to(LocalTime.of(7, 59))
                                    .build()
                    )
                    .withInterval(
                            new TollFeeInterval.Builder()
                                    .from(LocalTime.of(16, 0))
                                    .to(LocalTime.of(16, 59))
                                    .build()
                    )
                    .build()
    );

    public static int get(LocalTime time) {
        return FEES.stream()
                .filter(fee -> fee.isApplicable(time))
                .map(TollFee::getAmount)
                .max(Integer::compareTo)
                .orElse(0);
    }
}
