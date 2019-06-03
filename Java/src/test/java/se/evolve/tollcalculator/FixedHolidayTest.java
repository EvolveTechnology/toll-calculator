package se.evolve.tollcalculator;

import static org.assertj.core.api.Assertions.assertThat;
import static se.evolve.tollcalculator.FixedHoliday.NEW_YEARS_DAY;

import java.time.LocalDate;

import org.junit.Test;

public class FixedHolidayTest {
	
	@Test
	public void date_is_correct() {
		assertThat(NEW_YEARS_DAY.getDate(2019)).isEqualTo(LocalDate.of(2019, 1, 1));
	}
}
