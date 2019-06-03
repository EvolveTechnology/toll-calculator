package se.evolve.tollcalculator;

import static org.assertj.core.api.Assertions.assertThat;

import java.time.LocalDate;

import org.junit.Test;

import se.evolve.tollcalculator.AllSaintsEveCalculator;

public class AllSaintsEveCalculatorTest {

	@Test
	public void saints_eve_2019() {
		assertThat(AllSaintsEveCalculator.get(2019)).isEqualTo(LocalDate.of(2019, 11, 1));
	}

	@Test
	public void saints_eve_2020() {
		assertThat(AllSaintsEveCalculator.get(2020)).isEqualTo(LocalDate.of(2020, 10, 30));
	}

	@Test
	public void saints_eve_2021() {
		assertThat(AllSaintsEveCalculator.get(2021)).isEqualTo(LocalDate.of(2021, 11, 5));
	}

	@Test
	public void saints_eve_2022() {
		assertThat(AllSaintsEveCalculator.get(2022)).isEqualTo(LocalDate.of(2022, 11, 4));
	}

	@Test
	public void saints_eve_2023() {
		assertThat(AllSaintsEveCalculator.get(2023)).isEqualTo(LocalDate.of(2023, 11, 3));
	}

	@Test
	public void saints_eve_2024() {
		assertThat(AllSaintsEveCalculator.get(2024)).isEqualTo(LocalDate.of(2024, 11, 1));
	}

	@Test
	public void saints_eve_2025() {
		assertThat(AllSaintsEveCalculator.get(2025)).isEqualTo(LocalDate.of(2025, 10, 31));
	}

	@Test
	public void saints_eve_2026() {
		assertThat(AllSaintsEveCalculator.get(2026)).isEqualTo(LocalDate.of(2026, 10, 30));
	}
}
