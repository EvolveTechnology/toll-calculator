package se.evolve.tollcalculator;

import static org.assertj.core.api.Assertions.assertThat;
import static se.evolve.tollcalculator.EasterHoliday.ASCENSION;
import static se.evolve.tollcalculator.EasterHoliday.BEFORE_ASCENSION;
import static se.evolve.tollcalculator.EasterHoliday.EASTER;
import static se.evolve.tollcalculator.EasterHoliday.EASTER_MONDAY;
import static se.evolve.tollcalculator.EasterHoliday.GOOD_FRIDAY;
import static se.evolve.tollcalculator.EasterHoliday.MAUNDY_THURSDAY;

import java.time.LocalDate;

import org.junit.Test;

public class EasterHolidayTest {

	@Test
	public void maundy_thursday_set_correctly() {
		LocalDate date = MAUNDY_THURSDAY.getDate(2019);
		assertThat(date).isEqualTo(LocalDate.of(2019, 4, 18));
	}

	@Test
	public void good_friday_set_correctly() {
		LocalDate date = GOOD_FRIDAY.getDate(2019);
		assertThat(date).isEqualTo(LocalDate.of(2019, 4, 19));
	}

	@Test
	public void easter_monday_set_correctly() {
		LocalDate date = EASTER_MONDAY.getDate(2019);
		assertThat(date).isEqualTo(LocalDate.of(2019, 4, 22));
	}

	@Test
	public void before_ascension_set_correctly() {
		LocalDate easter = LocalDate.of(2019, 4, 21);
		LocalDate date = BEFORE_ASCENSION.getDate(2019);
		assertThat(date).isEqualTo(easter.plusDays(38));
	}

	@Test
	public void ascension_set_correctly() {
		LocalDate easter = LocalDate.of(2019, 4, 21);
		LocalDate date = ASCENSION.getDate(2019);
		assertThat(date).isEqualTo(easter.plusDays(39));
	}

	@Test
	public void easter_2019() {
		assertThat(LocalDate.of(2019, 04, 21)).isEqualTo(EASTER.getDate(2019));
	}

	@Test
	public void easter_2020() {
		assertThat(LocalDate.of(2020, 04, 12)).isEqualTo(EASTER.getDate(2020));
	}

	@Test
	public void easter_2021() {
		assertThat(LocalDate.of(2021, 04, 4)).isEqualTo(EASTER.getDate(2021));
	}

	@Test
	public void easter_2022() {
		assertThat(LocalDate.of(2022, 04, 17)).isEqualTo(EASTER.getDate(2022));
	}

	@Test
	public void easter_2023() {
		assertThat(LocalDate.of(2023, 04, 9)).isEqualTo(EASTER.getDate(2023));
	}

	@Test
	public void easter_2024() {
		assertThat(LocalDate.of(2024, 03, 31)).isEqualTo(EASTER.getDate(2024));
	}

	@Test
	public void easter_2025() {
		assertThat(LocalDate.of(2025, 04, 20)).isEqualTo(EASTER.getDate(2025));
	}

	@Test
	public void easter_2026() {
		assertThat(LocalDate.of(2026, 04, 5)).isEqualTo(EASTER.getDate(2026));
	}

	@Test
	public void easter_2030() {
		assertThat(LocalDate.of(2030, 04, 21)).isEqualTo(EASTER.getDate(2030));
	}

	@Test
	public void easter_2039() {
		assertThat(LocalDate.of(2039, 04, 10)).isEqualTo(EASTER.getDate(2039));
	}
}
