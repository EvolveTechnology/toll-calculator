package com.evolve.util;

import java.util.List;

import lombok.Data;
import lombok.NoArgsConstructor;

@Data
public class TollTimeFeeList {

	private List<TollTimeFeeString> timeFeeList;
	
	@Data
	@NoArgsConstructor
	public static class TollTimeFeeString{
		private String start;
		private String end;
		private Double fee;
	}
}
