package com.evolve.services;

import com.evolve.data.TollPeriod;

import java.time.LocalTime;
import java.util.List;

public interface TollFeeService {
    /**
     * Get the toll fee for the specified time
     * @param time  the specified time
     * @return  the toll fee
     */
    int getTollFee(LocalTime time);

    /**
     * Add/update the given toll fee periods.
     * @param tollPeriods   the toll fee periods
     */
    void updateTollPeriods(List<TollPeriod> tollPeriods);

    /**
     * Remove the toll fee periods
     * @param tollPeriods   the toll fee periods.
     */
    void removeTollPeriods(List<TollPeriod> tollPeriods);
}
