using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TollFeeCalculator.Models
{
    public class TollPass
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string VehicleId { get; set; }

        public string VehicleType { get; set; }

        public string Date { get; set; }
    }
}
