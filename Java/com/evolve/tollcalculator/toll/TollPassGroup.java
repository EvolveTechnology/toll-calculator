package com.evolve.tollcalculator.toll;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

public class TollPassGroup {
    private final List<LocalDateTime> tollPasses = new ArrayList<>();
    private final LocalDateTime from;
    private final LocalDateTime to;

    public TollPassGroup(LocalDateTime from) {
        this.from = from;
        this.to = LocalDateTime.of(from.toLocalDate(), from.toLocalTime()).plusHours(1);
    }

    public boolean isApplicable(LocalDateTime tollPass) {
        return (tollPass.equals(from) || tollPass.isAfter(from)) && (tollPass.equals(to) || tollPass.isBefore(to));
    }

    public void addTollPasses(Collection<LocalDateTime> tollPasses) {
        this.tollPasses.addAll(tollPasses);
    }

    public List<LocalDateTime> getTollPasses() {
        return this.tollPasses;
    }

    public boolean tollPassExists(LocalDateTime tollPass) {
        return tollPasses.stream().anyMatch(feeDate -> feeDate.equals(tollPass));
    }
}
