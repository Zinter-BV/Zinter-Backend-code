using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class MongoDbService
{
    private readonly IMongoDatabase _database;

    public MongoDbService(IOptions<MongoDbSettings> settings)
    {
        var mongoClient = new MongoClient(settings.Value.ConnectionString);
        _database = mongoClient.GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoCollection<YourEntity> YourCollection =>
        _database.GetCollection<YourEntity>("your_collection_name");
}
