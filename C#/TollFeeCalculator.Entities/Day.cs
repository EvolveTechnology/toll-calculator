using System;
using Newtonsoft.Json;

namespace TollFeeCalculator.Entities
{
	public class Day
	{
		[JsonProperty(PropertyName = "datum")]
		public DateTime Datum { get; set; }
		[JsonProperty(PropertyName = "unixdatum")]
		public long Unixdatum { get; set; }
		[JsonProperty(PropertyName = "dag")]
		public string Dag { get; set; }
		[JsonProperty(PropertyName = "veckodag")]
		public string Veckodag { get; set; }
		[JsonProperty(PropertyName = "vecka")]
		public string Vecka { get; set; }
		[JsonProperty(PropertyName = "helgdag")]
		public string Helgdag { get; set; }
	}
}
