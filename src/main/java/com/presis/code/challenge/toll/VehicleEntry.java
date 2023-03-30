package com.presis.code.challenge.toll;

import java.time.LocalDateTime;

import com.presis.code.challenge.toll.model.Car;

public class VehicleEntry {

	public static void main(String[] args) {
		System.out.println("Toll Calculator");

		int cost = new TollCalculator().calculateTotalTollFee(
                new Car(),
                LocalDateTime.parse("2023-03-23T06:00:00"),
                LocalDateTime.parse("2023-03-23T06:45:00"),
                LocalDateTime.parse("2023-03-23T07:05:00"),
                LocalDateTime.parse("2023-03-23T07:35:00"),
                LocalDateTime.parse("2023-03-23T08:40:00"),
                LocalDateTime.parse("2023-03-23T09:10:00"),
                LocalDateTime.parse("2023-03-23T12:15:00"));
		
		System.out.println("Car Fee = " + cost);
		// 8 + 13 + 18 + 18 + 8 + 8 + 8 = 81
		
//		cost = new TollCalculator().calculateTotalTollFee(
//                new Motorbike(),
//                LocalDateTime.parse("2023-02-27T16:59:01"),
//                LocalDateTime.parse("2023-02-03T08:00:00"));
//		
//		System.out.println("Motorbike Fee = " + cost);
	}

}
