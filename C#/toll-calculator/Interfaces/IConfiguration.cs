using System.Collections.Generic;

namespace toll_calculator
{
    public interface IConfiguration
    {

        public abstract List<TollZone> TollZones();
    }

}
