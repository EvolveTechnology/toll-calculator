mod holiday;
mod toll_calculator;
mod toll_fee_period;

use std::env;

fn main() {
    let args: Vec<String> = env::args().collect();
    let vehicle_type = &args[1];
    let date_times = &args[2].split(",").collect::<Vec<_>>();

    let fee = toll_calculator::get_total_fee(vehicle_type, date_times);
    println!("Total Fee is: {}", fee);
}
