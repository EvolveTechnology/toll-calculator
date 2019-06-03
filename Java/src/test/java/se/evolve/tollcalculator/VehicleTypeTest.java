package se.evolve.tollcalculator;

import static org.assertj.core.api.Assertions.assertThat;
import static se.evolve.tollcalculator.VehicleType.CAR;
import static se.evolve.tollcalculator.VehicleType.DIPLOMAT;
import static se.evolve.tollcalculator.VehicleType.EMERGENCY;
import static se.evolve.tollcalculator.VehicleType.FOREIGN;
import static se.evolve.tollcalculator.VehicleType.MILITARY;
import static se.evolve.tollcalculator.VehicleType.MOTORBIKE;
import static se.evolve.tollcalculator.VehicleType.TRACTOR;

import org.junit.Test;

public class VehicleTypeTest {

	@Test
	public void toll_fee_is_correct() {
		assertThat(CAR.isTollFree()).isFalse();
		assertThat(MOTORBIKE.isTollFree()).isTrue();
		assertThat(TRACTOR.isTollFree()).isTrue();
		assertThat(EMERGENCY.isTollFree()).isTrue();
		assertThat(DIPLOMAT.isTollFree()).isTrue();
		assertThat(FOREIGN.isTollFree()).isTrue();
		assertThat(MILITARY.isTollFree()).isTrue();
	}
}
