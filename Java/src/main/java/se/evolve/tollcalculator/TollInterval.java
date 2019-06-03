package se.evolve.tollcalculator;

import java.time.LocalTime;

public class TollInterval {
	private LocalTime startTime;
	private LocalTime endTime;
	private int fee;

	public TollInterval(LocalTime startTime, LocalTime endTime, int fee) {
		this.startTime = startTime;
		this.endTime = endTime;
		this.fee = fee;
	}
	
	public LocalTime getStartTime() {
		return startTime;
	}
	
	public LocalTime getEndTime() {
		return endTime;
	}
	
	public int getFee() {
		return fee;
	}
	
	public boolean containsTime(LocalTime time) {
		return (time.isAfter(startTime) && time.isBefore(endTime)) || time.equals(startTime) || time.equals(endTime);
	}

}
