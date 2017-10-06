using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Toll_calculator.Models;
using Newtonsoft.Json;

namespace Toll_calculator.Adapters
{
    public class DateInfoAdapter
    {
        public static DateInfo GetDateInfo(DateTime drivingDate)
        {
            var param = $"{drivingDate.Year}{drivingDate:MM}{drivingDate:dd}";
            var request = (HttpWebRequest)WebRequest.Create($"http://api.dryg.net/dagar/v1/?datum={param}");
            request.Method = "GET";
            var response = (HttpWebResponse)request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            var line = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<DateInfo>(line);
        }
    }
}
