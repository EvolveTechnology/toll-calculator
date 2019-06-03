package se.evolve.tollcalculator;

import static org.assertj.core.api.Assertions.assertThat;
import static se.evolve.tollcalculator.TollCalculator.HIGH;
import static se.evolve.tollcalculator.TollCalculator.LOW;
import static se.evolve.tollcalculator.TollCalculator.MEDIUM;
import static se.evolve.tollcalculator.VehicleType.CAR;
import static se.evolve.tollcalculator.VehicleType.EMERGENCY;

import java.time.LocalDateTime;
import java.util.List;

import org.junit.Test;

import com.google.common.collect.Lists;

public class TollCalculatorTest {
	private TollCalculator calculator = new TollCalculator();

	@Test
	public void test_no_dates_returns_zero() {
		assertThat(calculator.calculateTollFeeForDay(CAR, Lists.newArrayList())).isZero();
	}

	@Test
	public void null_list_gets_no_fees() {
		assertThat(calculator.calculateTollFeeForDay(CAR, null)).isZero();
	}

	@Test
	public void toll_free_gets_zero_fee() {
		assertThat(calculator.calculateTollFeeForDay(EMERGENCY, createSingleTime(15, 45))).isZero();
	}

	@Test
	public void first_period_is_8() {
		assertThat(calculator.calculateTollFeeForDay(CAR, createSingleTime(6, 0))).isEqualTo(LOW);
		assertThat(calculator.calculateTollFeeForDay(CAR, createSingleTime(6, 29))).isEqualTo(LOW);
	}

	@Test
	public void second_period_is_13() {
		assertThat(calculator.calculateTollFeeForDay(CAR, createSingleTime(6, 30))).isEqualTo(MEDIUM);
		assertThat(calculator.calculateTollFeeForDay(CAR, createSingleTime(6, 59))).isEqualTo(MEDIUM);
	}

	@Test
	public void third_period_is_18() {
		assertThat(calculator.calculateTollFeeForDay(CAR, createSingleTime(7, 0))).isEqualTo(HIGH);
		assertThat(calculator.calculateTollFeeForDay(CAR, createSingleTime(7, 59))).isEqualTo(HIGH);
	}

	@Test
	public void fourth_period_is_13() {
		assertThat(calculator.calculateTollFeeForDay(CAR, createSingleTime(8, 0))).isEqualTo(MEDIUM);
		assertThat(calculator.calculateTollFeeForDay(CAR, createSingleTime(8, 29))).isEqualTo(MEDIUM);
	}

	@Test
	public void fifth_period_is_8() {
		assertThat(calculator.calculateTollFeeForDay(CAR, createSingleTime(8, 30))).isEqualTo(LOW);
		assertThat(calculator.calculateTollFeeForDay(CAR, createSingleTime(14, 59))).isEqualTo(LOW);
	}

	@Test
	public void sixth_period_is_13() {
		assertThat(calculator.calculateTollFeeForDay(CAR, createSingleTime(15, 0))).isEqualTo(MEDIUM);
		assertThat(calculator.calculateTollFeeForDay(CAR, createSingleTime(15, 29))).isEqualTo(MEDIUM);
	}

	@Test
	public void seventh_period_is_18() {
		assertThat(calculator.calculateTollFeeForDay(CAR, createSingleTime(15, 30))).isEqualTo(HIGH);
		assertThat(calculator.calculateTollFeeForDay(CAR, createSingleTime(16, 59))).isEqualTo(HIGH);
	}

	@Test
	public void eight_period_is_13() {
		assertThat(calculator.calculateTollFeeForDay(CAR, createSingleTime(17, 0))).isEqualTo(MEDIUM);
		assertThat(calculator.calculateTollFeeForDay(CAR, createSingleTime(17, 59))).isEqualTo(MEDIUM);
	}

	@Test
	public void ninth_period_is_8() {
		assertThat(calculator.calculateTollFeeForDay(CAR, createSingleTime(18, 0))).isEqualTo(LOW);
		assertThat(calculator.calculateTollFeeForDay(CAR, createSingleTime(18, 29))).isEqualTo(LOW);
	}

	@Test
	public void daily_maximum_is_not_exceeded() {
		List<LocalDateTime> times = Lists.newArrayList(createTime(6, 0), createTime(7, 0), createTime(8, 0),
				createTime(9, 0), createTime(10, 0), createTime(15, 0), createTime(16, 0));
		assertThat(calculator.calculateTollFeeForDay(CAR, times)).isEqualTo(TollCalculator.DAILY_MAX);
	}

	@Test
	public void highest_fee_of_two() {
		List<LocalDateTime> times = Lists.newArrayList(createTime(8, 15), createTime(8, 45));
		assertThat(calculator.calculateTollFeeForDay(CAR, times)).isEqualTo(MEDIUM);
	}

	@Test
	public void highest_of_tree() {
		List<LocalDateTime> times = Lists.newArrayList(createTime(7, 50), createTime(8, 15), createTime(8, 49));
		assertThat(calculator.calculateTollFeeForDay(CAR, times)).isEqualTo(HIGH);
	}

	@Test
	public void highest_of_several_in_afternoon() {
		List<LocalDateTime> times = Lists.newArrayList(createTime(15, 20), createTime(15, 40), createTime(16, 15));
		assertThat(calculator.calculateTollFeeForDay(CAR, times)).isEqualTo(HIGH);
	}

	@Test
	public void several_passes_per_day() {
		List<LocalDateTime> times = Lists.newArrayList(createTime(7, 15), createTime(8, 14), createTime(8, 15),
				createTime(12, 30), createTime(13, 30));
		assertThat(calculator.calculateTollFeeForDay(CAR, times)).isEqualTo(HIGH + MEDIUM + LOW + LOW);
	}

	@Test
	public void two_close_edge_case() {
		List<LocalDateTime> times = Lists.newArrayList(createTime(6, 59), createTime(7, 0));
		assertThat(calculator.calculateTollFeeForDay(CAR, times)).isEqualTo(HIGH);
	}

	@Test
	public void two_separate_edge_case() {
		List<LocalDateTime> times = Lists.newArrayList(createTime(15, 00), createTime(16, 00));
		assertThat(calculator.calculateTollFeeForDay(CAR, times)).isEqualTo(MEDIUM + HIGH);
	}

	@Test
	public void two_passes_two_fees_seconds_difference() {
		List<LocalDateTime> times = Lists.newArrayList(createTimeWithSec(12, 0, 5), createTimeWithSec(13, 0, 5));
		assertThat(calculator.calculateTollFeeForDay(CAR, times)).isEqualTo(LOW + LOW);
	}

	@Test
	public void two_passes_one_fee_seconds_difference() {
		List<LocalDateTime> times = Lists.newArrayList(createTimeWithSec(14, 30, 57), createTimeWithSec(15, 30, 56));
		assertThat(calculator.calculateTollFeeForDay(CAR, times)).isEqualTo(HIGH);
	}

	@Test
	public void before_6_is_free() {
		assertThat(calculator.calculateTollFeeForDay(CAR, createSingleTime(0, 0))).isEqualTo(0);
	}

	@Test
	public void after_1829_is_free() {
		assertThat(calculator.calculateTollFeeForDay(CAR, createSingleTime(18, 30))).isEqualTo(0);
	}

	@Test
	public void holiday_is_toll_free() {
		assertThat(calculator.calculateTollFeeForDay(CAR, Lists.newArrayList(LocalDateTime.of(2019, 6, 6, 8, 0))))
				.isEqualTo(0);
	}

	@Test
	public void test_that_all_day_is_covered_by_intervals() {
		for (int h = 0; h <= 23; h++) {
			for (int m = 0; m <= 59; m++) {
				LocalDateTime time = LocalDateTime.of(2019, 06, 03, h, m);
				calculator.calculateTollFeeForDay(CAR, Lists.newArrayList(time));
			}
		}
	}

	private List<LocalDateTime> createSingleTime(int hour, int minute) {
		return Lists.newArrayList(createTime(hour, minute));
	}

	private LocalDateTime createTime(int hour, int minute) {
		return LocalDateTime.of(2019, 05, 28, hour, minute);
	}

	private LocalDateTime createTimeWithSec(int hour, int minute, int second) {
		return LocalDateTime.of(2019, 05, 28, hour, minute, second);
	}

}
