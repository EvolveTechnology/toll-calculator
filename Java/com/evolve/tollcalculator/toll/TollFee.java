package com.evolve.tollcalculator.toll;

import java.time.LocalTime;
import java.util.ArrayList;
import java.util.List;

public class TollFee {
    private final int amount;
    private final List<TollFeeInterval> intervals;

    public TollFee(int amount, List<TollFeeInterval> intervals) {
        this.amount = amount;
        this.intervals = intervals;
    }

    public boolean isApplicable(final LocalTime tollPass) {
        return intervals.stream().anyMatch(interval -> interval.isApplicable(tollPass));
    }

    public int getAmount() {
        return this.amount;
    }

    public static class Builder {
        private final int amount;
        private List<TollFeeInterval> intervals = new ArrayList<>();

        public Builder(int amount) {
            this.amount = amount;
        }

        public Builder withInterval(TollFeeInterval interval) {
            this.intervals.add(interval);

            return this;
        }

        public TollFee build() {
            return new TollFee(amount, intervals);
        }
    }
}
