package com.evolve.data;

import java.util.ArrayList;
import java.util.List;

public class TollPeriodConfig {
    private List<TollPeriodString> tollPeriods;

    public TollPeriodConfig() {
        tollPeriods = new ArrayList<>();
    }

    public List<TollPeriodString> getTollPeriods() {
        return tollPeriods;
    }

    public void setTollPeriods(List<TollPeriodString> tollPeriods) {
        this.tollPeriods = tollPeriods;
    }

    @Override
    public String toString() {
        return "TollPeriodConfig{" +
                "tollPeriods=" + tollPeriods +
                '}';
    }
}
