using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using TollFeeCalculator.Models;

namespace TollFeeCalculator.Services
{
    public interface ITollPassService
    {
        List<TollPass> Get();
        IEnumerable<TollPass> Get(string id);
        IEnumerable<TollPass> Get(string id, DateTime date);
        TollPass Create(TollPass tollPass);
        void Update(string id, TollPass tollPass);
    }

    public class TollPassService : ITollPassService
    {
        private readonly IMongoCollection<TollPass> _tollPasses;

        public TollPassService(IOptions<TollPassDatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);

            _tollPasses = database.GetCollection<TollPass>(settings.Value.TollPassCollectionName);
        }

        public List<TollPass> Get() =>
            _tollPasses.Find(tollPass => true).ToList();

        public IEnumerable<TollPass> Get(string id)
        {
            var result = _tollPasses.Find(tollPass => tollPass.VehicleId == id).ToList();
            return result;
        }

        public IEnumerable<TollPass> Get(string id, DateTime date)
        {
            return _tollPasses.Find<TollPass>(tollPass => tollPass.VehicleId == id && 
            (DateTime.Parse(tollPass.Date) == date)).ToList();
        }

        public TollPass Create(TollPass tollPass)
        {
            _tollPasses.InsertOne(tollPass);
            return tollPass;
        }

        public void Update(string id, TollPass tollPass) =>
            _tollPasses.ReplaceOne(tp => tp.VehicleId == id, tollPass);

        public void Remove(TollPass tollPass) =>
            _tollPasses.DeleteOne(tp => tp.VehicleId == tollPass.Id);
    }
}
