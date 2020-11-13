using System;

namespace toll_calculator
{
    public interface IYearSchema
    {
        public int GetYear();

        public bool IsAFreeDay(DateTime date);
    }
}
