using Kitchen.Service.DataAccess.Abstract;
using Kitchen.Service.DataAccess.Models;
using Kitchen.Service.Domain.Kitchen.Models;
using Kitchen.Service.Domain.Kitchen.UseCases.Queries;
using Kitchen.Service.Exceptions.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Kitchen.Service.DataAccess.Repositories
{
    public class MongoDbKitchenRepository : AbstractMongoDbRepository, IKitchenRepository
    {
        public MongoDbKitchenRepository(IOptions<DatabaseSettings> mongoDbSettings) : base(mongoDbSettings) 
        {
        
        }

        public async Task<List<KitchenResponse>> GetAllAsync(AllKitchenQuery request, CancellationToken cancellationToken = default)
        {
            var result = await GetCollection<TKitchen>(Collections.KitchenCollection)
            .AsQueryable()
            .Select(x => new KitchenResponse
            {
                Id = x.Id,
                Width = x.Width,
                Height = x.Height,
                WaterPipe = x.WaterPipe,
                Modules = x.Modules,
            })
            .ToListAsync(cancellationToken);

            return result;
        }

        public async Task<KitchenResponse> GetByIdOrDefaultAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = await GetCollection<TKitchen>(Collections.KitchenCollection)
                .AsQueryable()
                .Where(s => s.Id == id)
                .Select(x => new KitchenResponse
                {
                    Id = id,
                    Width = x.Width,
                    Height = x.Height,
                    WaterPipe = x.WaterPipe,
                    Modules = x.Modules,
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result ?? throw new NoDataFoundException($"Не найдена кухня с id {id}");
        }

        public async Task CreateAsync(TKitchen kitchenCollection, CancellationToken cancellationToken = default)
        {
            if (kitchenCollection is null)
            {
                throw new ArgumentNullException(nameof(kitchenCollection));
            }

            kitchenCollection.Id = ObjectId.GenerateNewId().ToString();
            await GetCollection<TKitchen>(Collections.KitchenCollection).InsertOneAsync(kitchenCollection, cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(string id, AddOrUpdateKitchenDto dto, CancellationToken cancellationToken = default)
        {
            if (dto is null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var filter = Builders<TKitchen>.Filter.Eq(s => s.Id, id);

            var update = Builders<TKitchen>.Update
                .Set(s => s.Width, dto.Width)
                .Set(s => s.Height, dto.Height)
                .Set(s => s.WaterPipe, dto.WaterPipe)
                .Set(s => s.Modules, dto.Modules);

            var result = await GetCollection<TKitchen>(Collections.KitchenCollection).UpdateOneAsync(filter, update, cancellationToken: cancellationToken);

            if (result.ModifiedCount == 0)
                throw new NoDataFoundException($"Не найдена кухня с id {id} для изменения");
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            await GetCollection<TKitchen>(Collections.KitchenCollection).DeleteOneAsync(doc => doc.Id == id, cancellationToken);
        }
    }
}
