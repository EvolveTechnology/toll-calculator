use chrono::{Date, Duration, Local, NaiveTime, TimeZone};

use crate::{holiday::is_holiday, toll_fee_period::TollFeePeriod};

const TOLL_FREE_VEHICLES: [&str; 6] = [
    "Motorbike",
    "Tractor",
    "Emergency",
    "Diplomat",
    "Foreign",
    "Military",
];

fn toll_fee_periods() -> Vec<TollFeePeriod> {
    vec![
        TollFeePeriod::new(
            NaiveTime::from_hms(6, 0, 0),
            NaiveTime::from_hms(6, 30, 0),
            8,
        ),
        TollFeePeriod::new(
            NaiveTime::from_hms(6, 30, 0),
            NaiveTime::from_hms(7, 0, 0),
            13,
        ),
        TollFeePeriod::new(
            NaiveTime::from_hms(7, 0, 0),
            NaiveTime::from_hms(8, 0, 0),
            18,
        ),
        TollFeePeriod::new(
            NaiveTime::from_hms(8, 0, 0),
            NaiveTime::from_hms(8, 30, 0),
            13,
        ),
        TollFeePeriod::new(
            NaiveTime::from_hms(8, 30, 0),
            NaiveTime::from_hms(15, 0, 0),
            8,
        ),
        TollFeePeriod::new(
            NaiveTime::from_hms(15, 0, 0),
            NaiveTime::from_hms(15, 30, 0),
            13,
        ),
        TollFeePeriod::new(
            NaiveTime::from_hms(15, 30, 0),
            NaiveTime::from_hms(17, 0, 0),
            18,
        ),
        TollFeePeriod::new(
            NaiveTime::from_hms(17, 0, 0),
            NaiveTime::from_hms(18, 0, 0),
            13,
        ),
        TollFeePeriod::new(
            NaiveTime::from_hms(18, 0, 0),
            NaiveTime::from_hms(18, 30, 0),
            8,
        ),
    ]
}

fn is_toll_free_vehicle(vehicle: &str) -> bool {
    TOLL_FREE_VEHICLES.contains(&vehicle)
}

fn get_single_fee(periods: &Vec<TollFeePeriod>, date: Date<Local>, time: NaiveTime) -> i32 {
    if is_holiday(date) {
        return 0;
    }

    let iter = periods
        .iter()
        .filter(|x| x.within(time))
        .collect::<Vec<_>>();

    let period = iter.first();

    match period {
        Some(i) => i.get_fee(),
        None => 0,
    }
}

pub fn get_total_fee(vehicle: &str, date_times: &Vec<&str>) -> i32 {
    if is_toll_free_vehicle(vehicle) {
        return 0;
    }

    let mut interval_start = Local
        .datetime_from_str(date_times[0], "%Y-%m-%d %H:%M:%S")
        .unwrap()
        .time();
    let mut total_fee: i32 = 0;

    for date_time in date_times {
        let periods = toll_fee_periods();
        let dt = Local
            .datetime_from_str(date_time, "%Y-%m-%d %H:%M:%S")
            .unwrap();

        let date = dt.date();
        let time = dt.time();

        let next_fee = get_single_fee(&periods, date, time);
        let mut temp_fee = get_single_fee(&periods, date, interval_start);

        if time - interval_start < Duration::hours(1) {
            if total_fee > 0 {
                total_fee -= temp_fee;
            }

            if next_fee >= temp_fee {
                temp_fee = next_fee;
            }

            total_fee += temp_fee;
        } else {
            total_fee += next_fee;
            interval_start = time;
        }
    }

    total_fee.min(60)
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_is_toll_free_vehicle() {
        assert_eq!(is_toll_free_vehicle("Diplomat"), true);
        assert_eq!(is_toll_free_vehicle("Car"), false);
        assert_eq!(is_toll_free_vehicle("UFO"), false);
    }

    #[test]
    fn test_get_single_fee() {
        let periods = toll_fee_periods();

        // Weekend
        let dt = Local
            .datetime_from_str("2021-10-09 7:00:00", "%Y-%m-%d %H:%M:%S")
            .unwrap();
        assert_eq!(get_single_fee(&periods, dt.date(), dt.time()), 0);

        // Holiday
        let dt = Local
            .datetime_from_str("2021-12-24 7:00:00", "%Y-%m-%d %H:%M:%S")
            .unwrap();
        assert_eq!(get_single_fee(&periods, dt.date(), dt.time()), 0);

        // Other
        let dt = Local
            .datetime_from_str("2021-10-11 8:00:00", "%Y-%m-%d %H:%M:%S")
            .unwrap();
        assert_eq!(get_single_fee(&periods, dt.date(), dt.time()), 13);
        let dt = Local
            .datetime_from_str("2021-10-11 7:59:59", "%Y-%m-%d %H:%M:%S")
            .unwrap();
        assert_eq!(get_single_fee(&periods, dt.date(), dt.time()), 18);
    }

    #[test]
    fn test_get_total_fee() {
        // Toll free vehicle
        let date_times = vec!["2021-10-11 7:00:00", "2021-10-11 8:00:00"];
        assert_eq!(get_total_fee("Diplomat", &date_times), 0);

        // Within an hour
        let date_times = vec!["2021-10-11 7:30:00", "2021-10-11 8:00:00"];
        assert_eq!(get_total_fee("Car", &date_times), 18);

        // Different hours
        let date_times = vec!["2021-10-11 7:00:00", "2021-10-11 8:00:00"];
        assert_eq!(get_total_fee("Car", &date_times), 31);

        // Total fee over 60
        let date_times = vec![
            "2021-10-11 7:00:00",
            "2021-10-11 8:00:00",
            "2021-10-11 9:00:00",
            "2021-10-11 10:00:00",
            "2021-10-11 11:00:00",
            "2021-10-11 12:00:00",
            "2021-10-11 13:00:00",
            "2021-10-11 14:00:00",
            "2021-10-11 15:00:00",
            "2021-10-11 16:00:00",
        ];
        assert_eq!(get_total_fee("Car", &date_times), 60);
    }
}
