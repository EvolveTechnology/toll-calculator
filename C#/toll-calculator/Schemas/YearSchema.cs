using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace toll_calculator
{
    public class YearSchema : IYearSchema
    {
        private SchemaModel schema;

        public YearSchema(string schemaPath)
        {
           
            schema = JsonConvert.DeserializeObject<SchemaModel>(File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(),"Years", schemaPath)));

        }

        public int GetYear()
        {
            return schema.Year;
        }

        public bool IsAFreeDay(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                return true;
            if (schema.FreeDays.Any(x => x.Month == date.Month && x.Day == 0))
                return true;
            return schema.FreeDays.Any(x => x.Month == date.Month &&  x.Day == date.Day);
        }
    }
}
