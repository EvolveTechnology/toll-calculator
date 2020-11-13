using System;

namespace toll_calculator
{
    public interface IVehicle
    {
        int GetTotalFee(DateTime[] datetimes);
        VehicleType GetVehicleType();
    }
}
