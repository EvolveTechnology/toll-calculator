package com.evolve.services;

import com.evolve.data.TollPeriod;
import com.evolve.data.TollPeriodConfig;
import com.evolve.data.TollPeriodString;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.yaml.snakeyaml.Yaml;
import org.yaml.snakeyaml.constructor.Constructor;

import java.io.*;
import java.time.LocalTime;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

public class TollFeeServiceImpl implements TollFeeService {
    // the toll fees for every minute in a day
    private static Map<LocalTime, Integer> tollFees;
    private static final Logger logger = LogManager.getLogger(TollFeeServiceImpl.class);
    private static final String CONFIG_FILENAME = "toll_fee.yaml";

    @Override
    public int getTollFee(LocalTime time) {
        Integer fee = tollFees.get(LocalTime.of(time.getHour(), time.getMinute()));
        // non-exist periods are free
        return fee == null ? 0 : fee;
    }

    @Override
    public void updateTollPeriods(List<TollPeriod> tollPeriods) {
        putTollPeriods(tollPeriods);
    }

    @Override
    public void removeTollPeriods(List<TollPeriod> tollPeriods) {
        for(TollPeriod tp : tollPeriods) {
            LocalTime start = LocalTime.of(tp.getStart().getHour(), tp.getStart().getMinute());
            LocalTime end = LocalTime.of(tp.getEnd().getHour(), tp.getEnd().getMinute(), 1);
            while(start.isBefore(end)) {
                tollFees.remove(start);
                start = start.plusMinutes(1);
            }
        }
    }

    static {
        tollFees = new ConcurrentHashMap<>();
        putTollPeriods(readTollPeriodConfig());
    }

    private static void putTollPeriods(List<TollPeriod> tollPeriods) {
        for (TollPeriod tp : tollPeriods) {
            int fee = tp.getFee();
            LocalTime start = LocalTime.of(tp.getStart().getHour(), tp.getStart().getMinute());
            LocalTime end = LocalTime.of(tp.getEnd().getHour(), tp.getEnd().getMinute(), 1);
            // Put the toll-fee for every minute into the map
            while (start.isBefore(end)) {
                tollFees.put(start, fee);
                start = start.plusMinutes(1);
            }
        }
    }

    private static List<TollPeriod> readTollPeriodConfig()  {
        // read config file (YAML format)
        Yaml yaml = new Yaml(new Constructor(TollPeriodConfig.class));
        List<TollPeriod> tollPeriods = new ArrayList<>();
        try (InputStream in = new FileInputStream(new File(TollFeeServiceImpl.CONFIG_FILENAME))) {
            TollPeriodConfig tollConfig = yaml.load(in);
            for (TollPeriodString tps : tollConfig.getTollPeriods()) {
                TollPeriod tollPeriod = new TollPeriod();
                tollPeriod.setStart(LocalTime.parse(tps.getStart()));
                tollPeriod.setEnd(LocalTime.parse(tps.getEnd()));
                tollPeriod.setFee(tps.getFee());
                tollPeriods.add(tollPeriod);
            }
        } catch (IOException e) {
            logger.error(e);
        }

        return tollPeriods;
    }
}
