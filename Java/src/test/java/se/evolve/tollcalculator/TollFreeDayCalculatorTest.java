package se.evolve.tollcalculator;

import static org.assertj.core.api.Assertions.assertThat;

import java.time.LocalDate;

import org.junit.Test;

public class TollFreeDayCalculatorTest {

	@Test
	public void saturday_is_free() {
		assertThat(TollFreeDayCalculator.isTollFreeDay(LocalDate.of(2019, 6, 1))).isTrue();
	}

	@Test
	public void sunday_is_free() {
		assertThat(TollFreeDayCalculator.isTollFreeDay(LocalDate.of(2019, 6, 2))).isTrue();
	}

	@Test
	public void july_weekday_is_free() {
		assertThat(TollFreeDayCalculator.isTollFreeDay(LocalDate.of(2019, 7, 17))).isTrue();
	}

	@Test
	public void new_years_day_is_free() {
		assertThat(TollFreeDayCalculator.isTollFreeDay(LocalDate.of(2019, 1, 1))).isTrue();
	}

	@Test
	public void before_epiphany_is_free() {
		assertThat(TollFreeDayCalculator.isTollFreeDay(LocalDate.of(2021, 1, 5))).isTrue();
	}

	@Test
	public void epiphany_is_free() {
		assertThat(TollFreeDayCalculator.isTollFreeDay(LocalDate.of(2020, 1, 6))).isTrue();
	}

	@Test
	public void maundy_thursday_is_free() {
		assertThat(TollFreeDayCalculator.isTollFreeDay(LocalDate.of(2019, 4, 18))).isTrue();
	}

	@Test
	public void good_friday_is_free() {
		assertThat(TollFreeDayCalculator.isTollFreeDay(LocalDate.of(2019, 4, 19))).isTrue();
	}

	@Test
	public void easter_monday_is_free() {
		assertThat(TollFreeDayCalculator.isTollFreeDay(LocalDate.of(2019, 4, 22))).isTrue();
	}

	@Test
	public void april_30th_is_free() {
		assertThat(TollFreeDayCalculator.isTollFreeDay(LocalDate.of(2019, 4, 30))).isTrue();
	}

	@Test
	public void may_1st_is_free() {
		assertThat(TollFreeDayCalculator.isTollFreeDay(LocalDate.of(2019, 5, 1))).isTrue();
	}

	@Test
	public void before_ascension_is_free() {
		assertThat(TollFreeDayCalculator.isTollFreeDay(LocalDate.of(2019, 5, 29))).isTrue();
	}

	@Test
	public void ascension_is_free() {
		assertThat(TollFreeDayCalculator.isTollFreeDay(LocalDate.of(2019, 5, 30))).isTrue();
	}

	@Test
	public void before_national_day_is_free() {
		assertThat(TollFreeDayCalculator.isTollFreeDay(LocalDate.of(2019, 6, 5))).isTrue();
	}

	@Test
	public void national_day_is_free() {
		assertThat(TollFreeDayCalculator.isTollFreeDay(LocalDate.of(2019, 6, 6))).isTrue();
	}

	@Test
	public void midsummer_is_free() {
		assertThat(TollFreeDayCalculator.isTollFreeDay(LocalDate.of(2019, 6, 21))).isTrue();
	}

	@Test
	public void all_saints_eve_is_free() {
		assertThat(TollFreeDayCalculator.isTollFreeDay(LocalDate.of(2019, 11, 1))).isTrue();
	}

	@Test
	public void christmas_eve_is_free() {
		assertThat(TollFreeDayCalculator.isTollFreeDay(LocalDate.of(2019, 12, 24))).isTrue();
	}

	@Test
	public void christmas_day_is_free() {
		assertThat(TollFreeDayCalculator.isTollFreeDay(LocalDate.of(2019, 12, 25))).isTrue();
	}

	@Test
	public void boxing_day_is_free() {
		assertThat(TollFreeDayCalculator.isTollFreeDay(LocalDate.of(2019, 12, 26))).isTrue();
	}

	@Test
	public void new_years_eve_is_free() {
		assertThat(TollFreeDayCalculator.isTollFreeDay(LocalDate.of(2019, 12, 31))).isTrue();
	}

	@Test
	public void normal_day_is_not_free() {
		assertThat(TollFreeDayCalculator.isTollFreeDay(LocalDate.of(2019, 6, 19))).isFalse();
	}

}
