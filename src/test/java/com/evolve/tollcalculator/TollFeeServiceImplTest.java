package com.evolve.tollcalculator;

import com.evolve.data.TollPeriod;
import com.evolve.services.TollFeeService;
import com.evolve.services.TollFeeServiceImpl;
import org.junit.Assert;
import org.junit.FixMethodOrder;
import org.junit.Test;

import java.time.LocalTime;

@FixMethodOrder()
public class TollFeeServiceImplTest {
    private final TollFeeService tollFeeService;

    public TollFeeServiceImplTest() {
        tollFeeService = new TollFeeServiceImpl();
    }

    @Test
    public void testTollFeePeriod() {
        LocalTime time = LocalTime.of(5, 30); // 05:30
        Assert.assertEquals(time.toString(), 0, tollFeeService.getTollFee(time));
        time = time.plusMinutes(30); // 06:00
        Assert.assertEquals(time.toString(), 8, tollFeeService.getTollFee(time));
        time = time.plusMinutes(30); // 06:30
        Assert.assertEquals(time.toString(), 13, tollFeeService.getTollFee(time));
        time = time.plusMinutes(30); // 07:00
        Assert.assertEquals(time.toString(), 18, tollFeeService.getTollFee(time));
        time = time.plusMinutes(60); // 08:00
        Assert.assertEquals(time.toString(), 13, tollFeeService.getTollFee(time));
        time = time.plusMinutes(30); // 08:30
        Assert.assertEquals(time.toString(), 8, tollFeeService.getTollFee(time));
        time = time.plusMinutes(60*6+30); // 15:00
        Assert.assertEquals(time.toString(), 13, tollFeeService.getTollFee(time));
        time = time.plusMinutes(30); // 15:30
        Assert.assertEquals(time.toString(), 18, tollFeeService.getTollFee(time));
        time = time.plusMinutes(90); // 17:00
        Assert.assertEquals(time.toString(), 13, tollFeeService.getTollFee(time));
        time = time.plusMinutes(60); // 18:00
        Assert.assertEquals(time.toString(), 8, tollFeeService.getTollFee(time));
        time = time.plusMinutes(60); // 19:00
        Assert.assertEquals(time.toString(), 0, tollFeeService.getTollFee(time));
    }

    @Test
    public void testModifyTollPeriod() {
        final int TOLL_FEE = 11;
        LocalTime start = LocalTime.of(6, 15);
        LocalTime end = LocalTime.of(6, 29);
        TollPeriod period = new TollPeriod(start, end, TOLL_FEE);
        tollFeeService.updateTollPeriods(period);
        Assert.assertEquals(start.toString(), TOLL_FEE, tollFeeService.getTollFee(start));
        Assert.assertEquals(end.toString(), TOLL_FEE, tollFeeService.getTollFee(end));
    }

    @Test
    public void testRemoveTollPeriod() {
        LocalTime start = LocalTime.of(6, 15);
        LocalTime end = LocalTime.of(6, 29);
        TollPeriod period = new TollPeriod(start, end, 11);
        tollFeeService.removeTollPeriods(period);
        // non-exist periods are toll-free
        Assert.assertEquals(start.toString(), 0, tollFeeService.getTollFee(start));
        Assert.assertEquals(end.toString(), 0, tollFeeService.getTollFee(end));
    }
}
