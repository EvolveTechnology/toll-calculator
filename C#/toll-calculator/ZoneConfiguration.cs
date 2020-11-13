using System;
using System.Collections.Generic;
using System.Text;

namespace toll_calculator
{
    public class ThisTownZoneConfiguration : IConfiguration
    {
        public List<TollZone> TollZones() => tollzones;
        private List<TollZone> tollzones;

        public ThisTownZoneConfiguration()
        {
            tollzones = new List<TollZone>();
            tollzones.Add(new TollZone(new TimeSpan(6, 0, 0), new TimeSpan(6, 29, 0), 8));
            tollzones.Add(new TollZone(new TimeSpan(6, 30, 0), new TimeSpan(6, 59, 0), 13));
            tollzones.Add(new TollZone(new TimeSpan(7, 0, 0), new TimeSpan(7, 59, 0), 18));
            tollzones.Add(new TollZone(new TimeSpan(8, 0, 0), new TimeSpan(8, 29, 0), 13));
            tollzones.Add(new TollZone(new TimeSpan(8, 30, 0), new TimeSpan(14, 59, 0), 8));
            tollzones.Add(new TollZone(new TimeSpan(15, 0, 0), new TimeSpan(15, 29, 0), 13));
            tollzones.Add(new TollZone(new TimeSpan(15, 29, 0), new TimeSpan(16, 59, 0), 18));
            tollzones.Add(new TollZone(new TimeSpan(17, 0, 0), new TimeSpan(17, 59, 0), 13));
            tollzones.Add(new TollZone(new TimeSpan(18, 0, 0), new TimeSpan(18, 29, 0), 8));
            
        }
        
    }

}
