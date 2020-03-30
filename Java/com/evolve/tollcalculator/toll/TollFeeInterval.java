package com.evolve.tollcalculator.toll;

import java.time.LocalTime;

public class TollFeeInterval {
    private final LocalTime from;
    private final LocalTime to;

    public TollFeeInterval(LocalTime from, LocalTime to) {
        this.from = from;
        this.to = to;
    }

    public boolean isApplicable(final LocalTime localTime) {
        return (localTime.isAfter(from) || localTime.equals(from)) && (localTime.isBefore(to) || localTime.equals(to));
    }

    public static class Builder {
        private LocalTime from;
        private LocalTime to;

        public Builder from(LocalTime from) {
            this.from = from;

            return this;
        }

        public Builder to(LocalTime to) {
            this.to = to;

            return this;
        }

        public TollFeeInterval build() {
            return new TollFeeInterval(from, to);
        }
    }
}
