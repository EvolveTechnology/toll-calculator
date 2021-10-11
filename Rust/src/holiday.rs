use bdays::easter;
use chrono::{Date, Datelike, Duration, Local, TimeZone, Weekday};

fn easter(year: i32) -> Date<Local> {
    let easter = easter::easter_naive_date(year).unwrap();
    Local.from_utc_date(&easter)
}

fn mid_summer(year: i32) -> Date<Local> {
    let date = Local.ymd(year, 6, 20);
    let weekday = date.weekday().num_days_from_sunday();

    date + Duration::days((6 - weekday).into())
}

fn all_saints(year: i32) -> Date<Local> {
    let date = Local.ymd(year, 11, 1);

    if date.weekday() == chrono::Weekday::Sun {
        date - Duration::days(1)
    } else {
        let weekday = date.weekday().num_days_from_sunday();
        date + Duration::days((6 - weekday).into())
    }
}

fn public_holidays(year: i32) -> Vec<Date<Local>> {
    let easter = easter(year);

    vec![
        // New Year's Day
        Local.ymd(year, 1, 1),
        // Epiphany
        Local.ymd(year, 1, 6),
        // Good Friday
        easter - Duration::days(2),
        // Easter
        easter,
        // Easter Monday
        easter + Duration::days(1),
        // Labour Day
        Local.ymd(year, 5, 1),
        // Ascension Day
        easter + Duration::days(39),
        // Pentecost
        easter + Duration::days(49),
        // National Day of Sweden
        Local.ymd(year, 6, 6),
        // Midsummer
        mid_summer(year),
        // All Saints' Day
        all_saints(year),
        // Christmas Eve
        Local.ymd(year, 12, 24),
        // Christmas Day
        Local.ymd(year, 12, 25),
        // Saint Stephen's Day
        Local.ymd(year, 12, 26),
        // New Year's Eve
        Local.ymd(year, 12, 31),
    ]
}

fn is_public_holiday(date: Date<Local>) -> bool {
    public_holidays(date.year()).contains(&date)
}

pub fn is_holiday(date: Date<Local>) -> bool {
    let weekday = date.weekday();
    weekday == Weekday::Sun || weekday == Weekday::Sat || is_public_holiday(date)
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_easter() {
        assert_eq!(easter(2016).format("%Y-%m-%d").to_string(), "2016-03-27");
        assert_eq!(easter(2017).format("%Y-%m-%d").to_string(), "2017-04-16");
        assert_eq!(easter(2018).format("%Y-%m-%d").to_string(), "2018-04-01");
        assert_eq!(easter(2019).format("%Y-%m-%d").to_string(), "2019-04-21");
        assert_eq!(easter(2020).format("%Y-%m-%d").to_string(), "2020-04-12");
        assert_eq!(easter(2021).format("%Y-%m-%d").to_string(), "2021-04-04");
        assert_eq!(easter(2022).format("%Y-%m-%d").to_string(), "2022-04-17");
        assert_eq!(easter(2023).format("%Y-%m-%d").to_string(), "2023-04-09");
        assert_eq!(easter(2024).format("%Y-%m-%d").to_string(), "2024-03-31");
        assert_eq!(easter(2025).format("%Y-%m-%d").to_string(), "2025-04-20");
        assert_eq!(easter(2026).format("%Y-%m-%d").to_string(), "2026-04-05");
    }

    #[test]
    fn test_mid_summer() {
        assert_eq!(
            mid_summer(2016).format("%Y-%m-%d").to_string(),
            "2016-06-25"
        );
        assert_eq!(
            mid_summer(2017).format("%Y-%m-%d").to_string(),
            "2017-06-24"
        );
        assert_eq!(
            mid_summer(2018).format("%Y-%m-%d").to_string(),
            "2018-06-23"
        );
        assert_eq!(
            mid_summer(2019).format("%Y-%m-%d").to_string(),
            "2019-06-22"
        );
        assert_eq!(
            mid_summer(2020).format("%Y-%m-%d").to_string(),
            "2020-06-20"
        );
        assert_eq!(
            mid_summer(2021).format("%Y-%m-%d").to_string(),
            "2021-06-26"
        );
        assert_eq!(
            mid_summer(2022).format("%Y-%m-%d").to_string(),
            "2022-06-25"
        );
        assert_eq!(
            mid_summer(2023).format("%Y-%m-%d").to_string(),
            "2023-06-24"
        );
        assert_eq!(
            mid_summer(2024).format("%Y-%m-%d").to_string(),
            "2024-06-22"
        );
        assert_eq!(
            mid_summer(2025).format("%Y-%m-%d").to_string(),
            "2025-06-21"
        );
        assert_eq!(
            mid_summer(2026).format("%Y-%m-%d").to_string(),
            "2026-06-20"
        );
    }

    #[test]
    fn test_all_saints() {
        assert_eq!(
            all_saints(2016).format("%Y-%m-%d").to_string(),
            "2016-11-05"
        );
        assert_eq!(
            all_saints(2017).format("%Y-%m-%d").to_string(),
            "2017-11-04"
        );
        assert_eq!(
            all_saints(2018).format("%Y-%m-%d").to_string(),
            "2018-11-03"
        );
        assert_eq!(
            all_saints(2019).format("%Y-%m-%d").to_string(),
            "2019-11-02"
        );
        assert_eq!(
            all_saints(2020).format("%Y-%m-%d").to_string(),
            "2020-10-31"
        );
        assert_eq!(
            all_saints(2021).format("%Y-%m-%d").to_string(),
            "2021-11-06"
        );
        assert_eq!(
            all_saints(2022).format("%Y-%m-%d").to_string(),
            "2022-11-05"
        );
        assert_eq!(
            all_saints(2023).format("%Y-%m-%d").to_string(),
            "2023-11-04"
        );
        assert_eq!(
            all_saints(2024).format("%Y-%m-%d").to_string(),
            "2024-11-02"
        );
        assert_eq!(
            all_saints(2025).format("%Y-%m-%d").to_string(),
            "2025-11-01"
        );
        assert_eq!(
            all_saints(2026).format("%Y-%m-%d").to_string(),
            "2026-10-31"
        );
    }

    #[test]
    fn test_is_public_holiday() {
        assert_eq!(is_public_holiday(Local.ymd(2026, 10, 31)), true);
        assert_eq!(is_public_holiday(Local.ymd(2026, 11, 30)), false);
        assert_eq!(is_public_holiday(Local.ymd(2022, 6, 25)), true);
        assert_eq!(is_public_holiday(Local.ymd(2022, 7, 25)), false);
        assert_eq!(is_public_holiday(Local.ymd(2017, 4, 16)), true);
        assert_eq!(is_public_holiday(Local.ymd(2017, 5, 15)), false);
    }

    #[test]
    fn test_is_holiday() {
        assert_eq!(is_holiday(Local.ymd(2021, 10, 10)), true);
        assert_eq!(is_holiday(Local.ymd(2021, 10, 9)), true);
        assert_eq!(is_holiday(Local.ymd(2021, 10, 11)), false);
    }
}
