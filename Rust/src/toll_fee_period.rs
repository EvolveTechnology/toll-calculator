use chrono::NaiveTime;

pub struct TollFeePeriod {
    start: NaiveTime,
    end: NaiveTime,
    fee: i32,
}

impl TollFeePeriod {
    pub fn new(start: NaiveTime, end: NaiveTime, fee: i32) -> Self {
        Self { start, end, fee }
    }

    pub fn within(&self, time: NaiveTime) -> bool {
        if time >= self.start && time < self.end {
            true
        } else {
            false
        }
    }

    pub fn get_fee(&self) -> i32 {
        self.fee
    }
}

#[cfg(test)]
mod tests {

    use super::*;

    #[test]
    fn test_within() {
        let p1 = TollFeePeriod {
            start: NaiveTime::from_hms(6, 0, 0),
            end: NaiveTime::from_hms(6, 30, 0),
            fee: 8,
        };
        let p2 = TollFeePeriod {
            start: NaiveTime::from_hms(6, 30, 0),
            end: NaiveTime::from_hms(7, 0, 0),
            fee: 13,
        };
        let p3 = TollFeePeriod {
            start: NaiveTime::from_hms(7, 0, 0),
            end: NaiveTime::from_hms(8, 0, 0),
            fee: 18,
        };

        assert_eq!(p1.within(NaiveTime::from_hms(6, 0, 0)), true);
        assert_eq!(p1.within(NaiveTime::from_hms(6, 29, 59)), true);
        assert_eq!(p1.within(NaiveTime::from_hms(6, 30, 0)), false);

        assert_eq!(p2.within(NaiveTime::from_hms(6, 30, 0)), true);
        assert_eq!(p2.within(NaiveTime::from_hms(6, 59, 59)), true);
        assert_eq!(p2.within(NaiveTime::from_hms(7, 0, 0)), false);

        assert_eq!(p3.within(NaiveTime::from_hms(7, 0, 0)), true);
        assert_eq!(p3.within(NaiveTime::from_hms(7, 59, 59)), true);
        assert_eq!(p3.within(NaiveTime::from_hms(8, 0, 0)), false);
    }
}
