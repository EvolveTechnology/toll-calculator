namespace toll_calculator
{
    public interface ISchemas
    {
        public void RegisterSchemaForYear(IYearSchema year);
        public IYearSchema GetSchemaForYear(int year);
    }
}
