#ifndef TOLL_FEE_CALCULATOR_INCLUDE_TOLL_FEE_CALCULATOR_VEHICLE_H
#define TOLL_FEE_CALCULATOR_INCLUDE_TOLL_FEE_CALCULATOR_VEHICLE_H

#include <range/v3/algorithm/any_of.hpp>

#include <array>

namespace toll_fee_calculator
{
    enum class vehicle
    {
        Car,
        Diplomat,
        Emergency,
        Foreign,
        Military,
        Motorbike,
        Tractor,
    };

    inline constexpr std::array<vehicle, 6> toll_free_vehicles =
    {
        vehicle::Diplomat,
        vehicle::Emergency,
        vehicle::Foreign,
        vehicle::Military,
        vehicle::Motorbike,
        vehicle::Tractor,
    };

    inline bool is_toll_free_vehicle(const vehicle the_vehicle)
    {
        return ranges::any_of(toll_free_vehicles, [the_vehicle](const vehicle& other_vehicle) { return the_vehicle == other_vehicle; });
    }
}

#endif
