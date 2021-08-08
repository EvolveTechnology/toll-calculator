using static toll_calculator_logic.Enums;

namespace toll_calculator_logic
{
    public class Foreign : IVehicle
    {
        public string GetVehicleType()
        {
            return "Foreign";
        }
    }
}