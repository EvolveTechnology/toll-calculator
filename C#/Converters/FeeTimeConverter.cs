using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using TollFeeCalculator.Models;

namespace TollFeeCalculator.Converters
{
    public class FeeTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(List<TimeDateFeeModel>) || objectType == typeof(TimeDateFeeModel);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartArray)
            {
                JArray items = JArray.Load(reader);
                var results = new List<TimeDateFeeModel>();

                foreach (var item in items)
                { 
                    results.Add(GetModel(item));
                }
                return results;
            }
            else if (reader.TokenType == JsonToken.StartObject)
            {
                JObject item = JObject.Load(reader);
                return GetModel(item);
            }

            return null;
        }

        private TimeDateFeeModel GetModel(JToken item)
        {
            var fromTime = DateTime.ParseExact(item["From"].Value<string>(), "H:mm", CultureInfo.InvariantCulture);
            var toTime = DateTime.ParseExact(item["To"].Value<string>(), "H:mm", CultureInfo.InvariantCulture);
            var fee = item["Fee"].Value<int>();
            int minuteOffset = 0;
            if (item["MinuteOffset"] != null)
            {
                minuteOffset = item["MinuteOffset"].Value<int>();
            }

            var model = new TimeDateFeeModel()
            {
                StartHour = fromTime.Hour,
                StartMinute = fromTime.Minute,
                EndHour = toTime.Hour,
                EndMinute = toTime.Minute,
                Fee = fee,
                MinuteOffset = minuteOffset
            };
            return model;
        }
        

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
