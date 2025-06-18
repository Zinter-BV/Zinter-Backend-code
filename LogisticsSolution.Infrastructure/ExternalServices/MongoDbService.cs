// File: LogisticsSolution.Infrastructure/MongoDbService.cs
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using LogisticsSolution.Application.Constant;
using System.Collections.Generic;
using System.Threading.Tasks;

public class MongoDbService<T>
{
    private readonly IMongoCollection<T> _collection;

    public MongoDbService(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _collection = database.GetCollection<T>(typeof(T).Name);
    }

    public async Task<List<T>> GetAllAsync() => await _collection.Find(_ => true).ToListAsync();
    public async Task InsertAsync(T entity) => await _collection.InsertOneAsync(entity);
}
