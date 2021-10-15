package com.evolve.data;

public class TollPeriodString {
    private String start;
    private String end;
    private int fee;

    public TollPeriodString() {
    }

    public TollPeriodString(String start, String end, int fee) {
        this.start = start;
        this.end = end;
        this.fee = fee;
    }

    public String getStart() {
        return start;
    }

    public void setStart(String start) {
        this.start = start;
    }

    public String getEnd() {
        return end;
    }

    public void setEnd(String end) {
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
        return "TollPeriodString{" +
                "start='" + start + '\'' +
                ", end='" + end + '\'' +
                ", fee=" + fee +
                '}';
    }
}
