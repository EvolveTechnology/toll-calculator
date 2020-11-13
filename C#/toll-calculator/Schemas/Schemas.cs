using System.Collections.Generic;
using System.Linq;

namespace toll_calculator
{
    public class Schemas : ISchemas
    {
        private List<IYearSchema> _years;
        public Schemas()
        {
            _years = new List<IYearSchema>();
        }

        public IYearSchema GetSchemaForYear(int year)
        {
            return _years.FirstOrDefault(x => x.GetYear() == year);
        }

        public void RegisterSchemaForYear(IYearSchema year)
        {
            _years.Add(year);
        }
    }
}
