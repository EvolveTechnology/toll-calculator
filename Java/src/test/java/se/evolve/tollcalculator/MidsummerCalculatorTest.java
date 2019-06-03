package se.evolve.tollcalculator;

import static org.assertj.core.api.Assertions.assertThat;

import java.time.LocalDate;

import org.junit.Test;

import se.evolve.tollcalculator.MidsummerCalculator;

public class MidsummerCalculatorTest {
	
	@Test
	public void midsummer_2019() {
		assertThat(MidsummerCalculator.get(2019)).isEqualTo(LocalDate.of(2019, 6, 21));
	}
	
	@Test
	public void midsummer_2020() {
		assertThat(MidsummerCalculator.get(2020)).isEqualTo(LocalDate.of(2020, 6, 19));
	}
	
	@Test
	public void midsummer_2021() {
		assertThat(MidsummerCalculator.get(2021)).isEqualTo(LocalDate.of(2021, 6, 25));
	}
	
	@Test
	public void midsummer_2022() {
		assertThat(MidsummerCalculator.get(2022)).isEqualTo(LocalDate.of(2022, 6, 24));
	}
	
	@Test
	public void midsummer_2023() {
		assertThat(MidsummerCalculator.get(2023)).isEqualTo(LocalDate.of(2023, 6, 23));
	}
	
	@Test
	public void midsummer_2024() {
		assertThat(MidsummerCalculator.get(2024)).isEqualTo(LocalDate.of(2024, 6, 21));
	}
	
	@Test
	public void midsummer_2025() {
		assertThat(MidsummerCalculator.get(2025)).isEqualTo(LocalDate.of(2025, 6, 20));
	}
	
	@Test
	public void midsummer_2026() {
		assertThat(MidsummerCalculator.get(2026)).isEqualTo(LocalDate.of(2026, 6, 19));
	}
	
	@Test
	public void midsummer_2027() {
		assertThat(MidsummerCalculator.get(2027)).isEqualTo(LocalDate.of(2027, 6, 25));
	}
	
	@Test
	public void midsummer_2028() {
		assertThat(MidsummerCalculator.get(2028)).isEqualTo(LocalDate.of(2028, 6, 23));
	}
	
	@Test
	public void midsummer_2029() {
		assertThat(MidsummerCalculator.get(2029)).isEqualTo(LocalDate.of(2029, 6, 22));
	}
}
