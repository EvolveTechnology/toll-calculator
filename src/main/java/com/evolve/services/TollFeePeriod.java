package com.evolve.services;

import java.time.LocalTime;

public class TollFeePeriod {
    private final LocalTime start;
    private final LocalTime end;
    private final int fee;

    public TollFeePeriod(LocalTime start, LocalTime end, int fee) {
        this.start = start;
        this.end = end;
        this.fee = fee;
    }

    public int getFee() {
        return fee;
    }

    /**
     * Check if the given time is within the toll period.
     * @param time  the specified time
     * @return  true if the specified time is within the period, false otherwise.
     */
    public boolean containsTime(LocalTime time) {
        return (start.isBefore(time) || start.equals(time)) && (end.isAfter(time) || end.equals(time));
    }

    @Override
    public String toString() {
        return "TollFeePeriod{" +
                "start=" + start +
                ", end=" + end +
                ", fee=" + fee +
                '}';
    }
}
