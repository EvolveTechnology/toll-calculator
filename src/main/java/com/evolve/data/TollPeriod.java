package com.evolve.data;

import java.time.LocalTime;

public class TollPeriod {
    private LocalTime start;
    private LocalTime end;
    private int fee;

    public TollPeriod() {
    }

    public TollPeriod(LocalTime start, LocalTime end, int fee) {
        this.start = start;
        this.end = end;
        this.fee = fee;
    }

    public LocalTime getStart() {
        return start;
    }

    public void setStart(LocalTime start) {
        this.start = start;
    }

    public LocalTime getEnd() {
        return end;
    }

    public void setEnd(LocalTime end) {
        this.end = end;
    }

    public int getFee() {
        return fee;
    }

    public void setFee(int fee) {
        this.fee = fee;
    }

    @Override
    public String toString() {
        return "TollPeriod{" +
                "start=" + start +
                ", end=" + end +
                ", fee=" + fee +
                '}';
    }
}
