using static toll_calculator_logic.Enums;

namespace toll_calculator_logic
{
    public class Emergency : IVehicle
    {
        public string GetVehicleType()
        {
            return "Emergency";
        }
    }
}