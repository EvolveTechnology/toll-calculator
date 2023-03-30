package com.presis.code.challenge.toll.model;

import java.time.LocalDate;
import java.time.LocalTime;
import java.time.YearMonth;
import java.time.format.DateTimeFormatter;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

public class TollMaster {
	public int maxFee;
	public int maxNextTripMinute;
	
	public List<FreeDate> freeDates;
	public List<FreeDate> freeYearMonths;
	public List<TimeRange> timeRanges;
	
	public int getMaxFee() {
		return maxFee;
	}
	public void setMaxFee(int maxFee) {
		this.maxFee = maxFee;
	}
	public int getMaxNextTripMinute() {
		return maxNextTripMinute;
	}
	
	public void setMaxNextTripMinute(int maxNextTripMinute) {
		this.maxNextTripMinute = maxNextTripMinute;
	}
	
	public Set<LocalDate> getWhiteListFreeDates() {
		Set<LocalDate> dates = new HashSet<>();
		for (FreeDate freeDate : this.freeDates) {
			dates.add(LocalDate.of(freeDate.getYear(), freeDate.getMonth(), freeDate.getDay()));
		}
		return dates;
	}
	
	public Set<YearMonth> getWhiteListFreeYearMonths() {
		Set<YearMonth> yearMonths = new HashSet<>();
		for (FreeDate freeDate : this.freeYearMonths) {
			yearMonths.add(YearMonth.of(freeDate.getYear(), freeDate.getMonth()));
		}
		return yearMonths;
	}
	public int fetchFeeForTimeRange(LocalTime localTime) {
		DateTimeFormatter hmFormatter = DateTimeFormatter.ofPattern("HH:mm");
		
		for(TimeRange range : this.timeRanges) {
			if((localTime.isAfter(LocalTime.parse(range.getFrom(), hmFormatter)) || localTime.equals(LocalTime.parse(range.getFrom(), hmFormatter))) && 
					(localTime.isBefore(LocalTime.parse(range.getTo(), hmFormatter)) || localTime.equals(LocalTime.parse(range.getTo(), hmFormatter)))) {
				return range.getFee();
			}
		}
		return 0;
	}
}
