using VehicleService;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace UserDataService.UserService;

public class userService
{
    private readonly IMongoCollection<Vehicle> userCollection;

    public userService(
        IOptions<DatabaseSettings> DatabaseSettings)
    {
        var mongoClient = new MongoClient(
            DatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            DatabaseSettings.Value.DatabaseName);

        userCollection = mongoDatabase.GetCollection<Vehicle>(
            DatabaseSettings.Value.UserCollectionName);
    }
    public  List<Vehicle> GetAsync() {
        return userCollection.Find(_ => true).ToList();
    }

    public void PostVehicle(Vehicle newVehicle){
        userCollection.InsertOne(newVehicle);
    }
       
/*
    public  bool Login(LoginModel login) {
            var login_try = userCollection.Find<LoginModel>(login1 => login.Password == login1.Password ).Any();
            if (login_try == false){
                return false;
            }
            return true;
    }
    
    public async Task<List<Vehicle>> GetAsync() =>
        await userCollection.Find(_ => true).ToListAsync();

    public async Task<LoginModel?> GetAsync(string id) =>
        await userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(LoginModel newUser) =>
        await userCollection.InsertOneAsync(newUser);

    public async Task UpdateAsync(string id, LoginModel updatedUser) =>
        await userCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

    public async Task RemoveAsync(string id) =>
        await userCollection.DeleteOneAsync(x => x.Id == id);*/
}