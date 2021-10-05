package com.evolve.services;

import java.time.LocalTime;

public interface TollFeeService {
    int getTollFee(LocalTime time);
}
