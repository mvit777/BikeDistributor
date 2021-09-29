using MongoDB.Bson;
using MongoDB.Driver;
using MV.Framework.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MV.Framework.providers
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly IMongoDBContext _mongoContext;
        protected IMongoCollection<TEntity> _dbCollection;

        protected BaseRepository(IMongoDBContext context)
        {
            _mongoContext = context;
            _dbCollection = _mongoContext.GetCollection<TEntity>(typeof(TEntity).Name);
        }
        public async Task<TEntity> Get(string id)
        {
            var objectId = new ObjectId(id);
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", objectId);
            return await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public async Task<TEntity> Get(string id, bool usePlainTextId)
        {
            FilterDefinition<TEntity> filter = null;
            if (usePlainTextId==false)
            {
                //ex. 5dc1039a1521eaa36835e541
                var objectId = new ObjectId(id);
                filter = Builders<TEntity>.Filter.Eq("_id", objectId);
            }
            else
            {
                filter = Builders<TEntity>.Filter.Eq("_id", id);
            }
            return await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<TEntity>> Get()
        {
            var all = await _dbCollection.FindAsync(Builders<TEntity>.Filter.Empty);
            return await all.ToListAsync();
        }
        public async Task Create(TEntity obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(typeof(TEntity).Name + " object is null");
            }
            await _dbCollection.InsertOneAsync(obj);
        }

        public virtual void Update(TEntity obj)
        {
            //see comment at https://www.thecodebuzz.com/mongodb-repository-implementation-unit-testing-net-core-example/#dbcontext
            var pkValue = obj.GetType().GetProperty("Id").GetValue(obj);
            _dbCollection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", pkValue), obj);
        }
        public void Delete(string id)
        {
            //ex. 5dc1039a1521eaa36835e541
            var objectId = new ObjectId(id);
            _dbCollection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", objectId));

        }
        public void Delete(string id, bool usePlainTextId)
        {
            if (usePlainTextId == true)
            {
                _dbCollection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id));
            }
            else
            {
                //ex. 5dc1039a1521eaa36835e541
                var objectId = new ObjectId(id);
                _dbCollection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", objectId));
            }
           

        }


    }
}
