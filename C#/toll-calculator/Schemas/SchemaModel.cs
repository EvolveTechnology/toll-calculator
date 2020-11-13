using System.Collections.Generic;

namespace toll_calculator
{
    public class SchemaModel
    {
        public int Year { get; set; }
        public IEnumerable<ShortDate> FreeDays { get; set; }
    }
}
