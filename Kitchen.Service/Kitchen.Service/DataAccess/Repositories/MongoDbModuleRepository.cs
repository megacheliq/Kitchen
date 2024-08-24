using Kitchen.Service.DataAccess.Abstract;
using Kitchen.Service.DataAccess.Models;
using Kitchen.Service.Domain.Module.Models;
using Kitchen.Service.Domain.Module.UseCases.Queries;
using Kitchen.Service.Exceptions.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Kitchen.Service.DataAccess.Repositories
{
    public class MongoDbModuleRepository : AbstractMongoDbRepository, IModuleRepository
    {
        public MongoDbModuleRepository(IOptions<DatabaseSettings> mongoDbSettings) : base(mongoDbSettings)
        {

        }

        public async Task<List<ModuleResponse>> GetAllAsync(AllModuleQuery request, CancellationToken cancellationToken = default)
        {
            var result = await GetCollection<TModule>(Collections.ModuleCollection)
            .AsQueryable()
            .Select(x => new ModuleResponse
            {
                Id = x.Id,
                Name = x.Name,
                Width = x.Width,
                Height = x.Height,
                IsCorner = x.IsCorner,
                RequiresWater = x.RequiresWater,
            })
            .ToListAsync(cancellationToken);

            return result;
        }

        public async Task<ModuleResponse> GetByIdOrDefaultAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = await GetCollection<TModule>(Collections.ModuleCollection)
                .AsQueryable()
                .Where(s => s.Id == id)
                .Select(x => new ModuleResponse
                {
                    Id = id,
                    Name = x.Name,
                    Width = x.Width,
                    Height = x.Height,
                    IsCorner = x.IsCorner,
                    RequiresWater = x.RequiresWater,
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result ?? throw new NoDataFoundException($"Не найден модуль с id {id}");
        }

        public async Task CreateAsync(TModule moduleCollection, CancellationToken cancellationToken = default)
        {
            if (moduleCollection is null)
            {
                throw new ArgumentNullException(nameof(moduleCollection));
            }

            moduleCollection.Id = ObjectId.GenerateNewId().ToString();
            await GetCollection<TModule>(Collections.ModuleCollection).InsertOneAsync(moduleCollection, cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(string id, AddOrUpdateModuleDto dto, CancellationToken cancellationToken = default)
        {
            if (dto is null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var filter = Builders<TModule>.Filter.Eq(s => s.Id, id);

            var update = Builders<TModule>.Update
                .Set(s => s.Name, dto.Name)
                .Set(s => s.Width, dto.Width)
                .Set(s => s.Height, dto.Height)
                .Set(s => s.IsCorner, dto.IsCorner)
                .Set(s => s.RequiresWater, dto.RequiresWater);

            var result = await GetCollection<TModule>(Collections.ModuleCollection).UpdateOneAsync(filter, update, cancellationToken: cancellationToken);

            if (result.ModifiedCount == 0)
                throw new NoDataFoundException($"Не найден модуль с id {id} для изменения");
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            await GetCollection<TModule>(Collections.ModuleCollection).DeleteOneAsync(doc => doc.Id == id, cancellationToken);
        }
    }
}
