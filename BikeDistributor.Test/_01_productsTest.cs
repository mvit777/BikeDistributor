using BikeDistributor.Domain.Entities;
using BikeDistributor.Domain.Models;
using BikeDistributor.Infrastructure.core;
using BikeDistributor.Infrastructure.factories;
using BikeDistributor.Infrastructure.services;
using FluentAssertions;
using MongoDB.Bson.Serialization;
using MV.Framework;
using MV.Framework.providers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace BikeDistributor.Test
{
    public class _01_productsTest
    {
        private string _productTestsConfigFile = @".\Fixtures\ProductTests.json";

        private string _mongoDbNameTest = "BikeDbTest";
        private MongoSettings _mongoSettings = null;
        private string _servicesNamespace = "BikeDistributor.Infrastructure.services";

        private Config _configWS;
        private Config _blazorConfig;
        private BaseConfig _testProductConfig;
        private HttpClient _restClient;
        private bool _BsonTypesRegistered = false;
        //private string _baseUrl = "http://localhost:8021";
        public _01_productsTest()
        {
            //BsonSerializer.RegisterIdGenerator(typeof(string), new StringObjectIdGenerator());
            if (!BsonClassMap.IsClassMapRegistered(typeof(Bike)))
            {
                BsonClassMap.RegisterClassMap<Bike>();
                BsonClassMap.RegisterClassMap<BikeVariant>();
                BsonClassMap.RegisterClassMap<BikeOption>();
            }
            _BsonTypesRegistered = true;
            _testProductConfig = new BaseConfig(_productTestsConfigFile);
            _configWS = GetWSConfig();
            _mongoSettings = _configWS.GetClassObject<MongoSettings>("Mongo");
            _configWS.DefaultMongoSettings = _mongoSettings;
            _mongoSettings.DatabaseName = _mongoDbNameTest;
        }

        private Config GetWSConfig()
        {
            var file = @"C:\inetpub\wwwroot\sites\bikeapi\appsettings.json";
            return new Config(file);
        }

        private JObject GetJBike(int index)
        {
            return _testProductConfig.GetJObject("bikes", index);
        }

        [Fact]
        public async Task _01_00_TestLibraryConfigAsync()
        {
           
            var mongoContext = new MongoDBContext(_mongoSettings);
            //var selectedService = settings.Services.
            var bikeService = (MongoBikeService)MongoServiceFactory.GetMongoService(mongoContext, "MongoBikeService");
            var bikes = await bikeService.Get();
            bikes.Count.Should().BeOneOf(new int[] { 2 });
        }

        [Fact]
        public void _01_01_GetProductFromFile()
        {
            var bike = (Bike)BikeFactory.Create(GetJBike(0)).GetBike();
            bike.isStandard.Should().Be(true);
        }
        #region "dead code"

        /*
        /// <summary>
        /// commented as BikeRepo is now an internal class to enforce the use of BikeService
        /// </summary>
        /// <returns></returns>
        //[Fact]
        //public async Task _01_02_SaveProductMongoAsync()
        //{          
        //    var bike = BikeFactory.Create(GetJBike(1)).GetBike();
        //    bike.isStandard.Should().Be(false);
        //    var bv = (BikeVariant)bike;
        //    bv.GetOptions().Count.Should().BeGreaterThan(0);
        //    bv.GetOption("Material").Description.Should().Be("Carbon Fiber");
        //    var bikeRepo = new BikeRepository(_context);
        //    var defySe = new MongoEntityBike(bv);
        //    await bikeRepo.Create(defySe);
        //    var bikes = (List<MongoEntityBike>)await bikeRepo.Get();
        //    bikes.Count.Should().Be(1);
        //    var getInserted = await bikeRepo.Get(defySe.Bike.Model, true); //id is not in correct format
        //    var justInsertedBikeVariant = (BikeVariant)getInserted.Bike;
        //    justInsertedBikeVariant.GetOptions().Count.Should().BeGreaterThan(0);
        //    bikeRepo.Delete(getInserted.Id, true);
        //    bikes = (List<MongoEntityBike>)await bikeRepo.Get();
        //    bikes.Count.Should().Be(0);
        //}
        */
        #endregion

        /// <summary>
        /// Add a Bike using BikeService
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task _01_03_UsingBikeServiceAddAsync()
        {
            var bike = BikeFactory.Create(GetJBike(0)).GetBike();
            _mongoSettings.DatabaseName.Should().Be(_mongoDbNameTest);
            var mongoContext = new MongoDBContext(_mongoSettings);
            var bikeService = (MongoBikeService)MongoServiceFactory.GetMongoService(mongoContext, "MongoBikeService");
            bikeService.Delete(bike.Model);
            await bikeService.AddBikeAsync(bike);
            MongoEntityBike meb = await bikeService.Get(bike.Model);
            meb.Bike.Brand.Should().Be(bike.Brand);
        }

        /// <summary>
        /// BikeService Full CRUD
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task _01_04_UsingBikeServiceAddUpdateDeleteAsync()
        {
            int initialPrice = 2350;
            var bike = BikeFactory.Create(GetJBike(1)).GetBike();
            var mongoContext = new MongoDBContext(_mongoSettings);
            var bikeService = (MongoBikeService)MongoServiceFactory.GetMongoService(mongoContext, "MongoBikeService");
            bikeService.Delete(bike.Model);
            MongoEntityBike meb = await bikeService.AddBikeAsync(bike);
            bike.Price.Should().Equals(initialPrice);
            meb.Bike.Price.Should().Equals(initialPrice);
            meb.Bike.Brand.Should().Be("Giant");
            var bv = (BikeVariant)meb.Bike;
            var goldenChain = BikeOption.Create("Golden Chain").Create("an uncommon chain to show off", 400);
            bv.SetTotalPrice(goldenChain);
            int newPrice = initialPrice + goldenChain.Price;
            bv.Price.Should().Equals(newPrice);
            bv.Price.Should().Equals(2750);
            meb.Bike = bv;
            meb = bikeService.Update(meb);
            meb.Bike.Price.Should().Equals(newPrice);
            var bv2 = (BikeVariant)meb.Bike;
            bv2.GetBasePrice().Should().Equals(Bike.OneThousand);
            //bikeService.Delete(meb.Id);
            //throw new Exception(meb.Bike.Price.ToString() + "==" + newPrice.ToString());
        }

        [Fact]
        public async Task _01_05_UsingBikeOptionServiceAsync()
        {
            BikeOption bo = BikeOption.Create("Golden chain").Create("something to show off", 400);
            var mongoContext = new MongoDBContext(_mongoSettings);
            var bos = (MongoBikeOptionService)MongoServiceFactory.GetMongoService(mongoContext, "MongoBikeOptionService");
            bos.Delete(bo.Name);
            var mob = (MongoEntityBikeOption)await bos.AddBikeOptionAsync(bo);
            mob.BikeOption.Price.Should().Equals(400);
            mob.BikeOption.Price = 500;
            mob = bos.Update(mob);
            mob.BikeOption.Price.Should().Equals(500);
        }

        [Fact]
        public async Task _01_06_UseRegistryAsync()
        {
            var registrer = new MongoServiceInstanceRegister();
            var mongoContext = new MongoDBContext(_mongoSettings);

            var bikeService = (MongoBikeService)MongoServiceFactory.GetMongoService(mongoContext, "MongoBikeService");
            //MongoEntityBike meb = await bikeService.Get("Defy 1");
            var j = JsonConvert.SerializeObject(bikeService, new JsonSerializerSettings() {  NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented });
            //File.WriteAllText(@"c:\temp\seriazlr.txt", j);

            registrer.SetServiceInstance(bikeService, "MongoBikeService");
            var retrievedService = (MongoBikeService)registrer.GetServiceInstance("MongoBikeService", "BikeDistributor.Infrastructure.services.MongoBikeService, BikeDistributor");
            MongoEntityBike meb = await retrievedService.Get("Defy 1");
            meb.TotalPrice.Should().Equals(Bike.OneThousand);
        }

    }
}
