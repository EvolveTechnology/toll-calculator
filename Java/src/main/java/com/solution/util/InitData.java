package com.solution.util;

import org.yaml.snakeyaml.Yaml;
import org.yaml.snakeyaml.constructor.Constructor;
import java.io.IOException;
import java.io.InputStream;
import java.time.LocalDate;
import java.time.LocalTime;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import static com.solution.util.Constants.FEE_BY_TIME_LIST_YAML_FILE;

/**
 * @description:  Initialize the data:
 *  1. Init the holiday data for a year into Map,
 *  2. The fee for each time of day are read from the YAML config file and stored into List
 * @author: Richard(Duo.Wang)
 * @createDate: 2021/12/16 - 13:24
 * @version: v1.0
 */
public class InitData {

    public static final Map<LocalDate, Boolean> holidayMap = new HashMap<>();//store holiday data
    public static final List<TimeIntervalFee> timeFeeList = new ArrayList<>();//store the charging standard data of each period for one day

    static {
        initHolidayMap();
        initTimeFeeYaml();
    }

    public static void initHolidayMap() {

        int year = LocalDate.now().getYear();

        // Jan.1
        holidayMap.put(LocalDate.of(year, 1, 1), true);

        // Mar.28,29
        holidayMap.put(LocalDate.of(year, 3, 28), true);
        holidayMap.put(LocalDate.of(year, 3, 29), true);

        // Apr.1,30
        holidayMap.put(LocalDate.of(year, 4, 1), true);
        holidayMap.put(LocalDate.of(year, 4, 30), true);

        // May.1,8,9
        holidayMap.put(LocalDate.of(year, 5, 1), true);
        holidayMap.put(LocalDate.of(year, 5, 8), true);
        holidayMap.put(LocalDate.of(year, 5, 9), true);

        // Jun.5,6,21
        holidayMap.put(LocalDate.of(year, 6, 5), true);
        holidayMap.put(LocalDate.of(year, 6, 6), true);
        holidayMap.put(LocalDate.of(year, 6, 21), true);

        // Jul.
        for (int i = 1; i <= 31; i++) {
            holidayMap.put(LocalDate.of(year, 7, i), true);
        }

        // Nov.1
        holidayMap.put(LocalDate.of(year, 11, 1), true);

        // Dec.24,25,26,31
        holidayMap.put(LocalDate.of(year, 12, 24), true);
        holidayMap.put(LocalDate.of(year, 12, 25), true);
        holidayMap.put(LocalDate.of(year, 12, 26), true);
        holidayMap.put(LocalDate.of(year, 12, 31), true);
    }

    /**
     * Load YAML file and init TimeFee list.
     */
    public static void initTimeFeeYaml() {

        try (InputStream in = InitData.class.getResourceAsStream(FEE_BY_TIME_LIST_YAML_FILE)) {
            Yaml yaml = new Yaml(new Constructor(TimeIntervalFeeList.class));
            TimeIntervalFeeList list = yaml.load(in);

            list.getTimeFeeList()
                    .stream()
                    .map(timeFeeObj -> timeFeeToDto(timeFeeObj))
                    .forEach(timeFeeList::add);
        } catch (IOException e) {
            e.printStackTrace();
            throw new RuntimeException(e.getMessage());
        }
    }

    public static TimeIntervalFee timeFeeToDto(TimeIntervalFeeList.TimeFeeObj timeFeeObj) {
        return new TimeIntervalFee()
                .setStartTime(LocalTime.parse(timeFeeObj.getStart()))
                .setEndTime(LocalTime.parse(timeFeeObj.getEnd()))
                .setFee(timeFeeObj.getFee());
    }
}
