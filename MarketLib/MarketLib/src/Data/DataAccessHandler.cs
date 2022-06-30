using MarketLib.src.StoreNS;
using MarketLib.src.UserP;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MarketLib.src.Data
{
    public class DataAccessHandler
    {
        private const string ConnectionString = "mongodb+srv://admin:admin@cluster0.qluhr.mongodb.net/?retryWrites=true&w=majority";
        private const string DatabaseName = "test";
        private const string StoreCollection = "stores";
        private const string UserCollection = "users";
        private const string ChoreHistoryCollection = "chore_history";

        private IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase(DatabaseName);
            return db.GetCollection<T>(collection);

        }


        public async Task<List<User>> GetAllUsers()
        {
            var usersCollection = ConnectToMongo<User>(UserCollection);
            var res = await usersCollection.FindAsync(_ => true);
            return res.ToList();
        }

        public async Task<List<Store>> GetAllStores()
        {
            var storesCollection = ConnectToMongo<Store>(StoreCollection);
            var res = await storesCollection.FindAsync(_ => true);
            return res.ToList();
        }

        /*public async Task<List<Store>> GetAllStoresForAUser(User user)
        {
            var storesCollection = ConnectToMongo<Store>(StoreCollection);
            var res = await storesCollection.FindAsync(s => s.AssignedTo.Id == user.Id);
            return res.ToList();
        }*/

        public async Task CreateUser(User user)
        {
            var usersCollection = ConnectToMongo<User>(UserCollection);
            await usersCollection.InsertOneAsync(user);
        }

        public async Task CreateStore(Store store)
        {
            var storesCollection = ConnectToMongo<Store>(StoreCollection);
            await storesCollection.InsertOneAsync(store);
        }

        /*public async Task UpdateStore(Store store)
        {
            var storesCollection = ConnectToMongo<Store>(ChoreHistoryCollection);
            var filter = Builders<Store>.Filter.Eq("Id", store.Id);
            await storesCollection.ReplaceOneAsync(filter, store, new ReplaceOptions { IsUpsert = true }); // if it doesn't exist, it inserts it.
        }*/

        /*public async Task DeleteStore(Store store)
        {
            var storesCollection = ConnectToMongo<Store>(StoreCollection);
            await storesCollection.DeleteOneAsync(s => s.Id == store.Id);
        }*/

        //complete chore example 54:09, https://www.youtube.com/watch?v=exXavNOqaVo
        // could work for purchase history
        /*public async Task CompleteChore(Store store)
        {
            // var storesCollection = ConnectToMongo<StoreDTO>(StoreCollection);
            //var filter = Builders<StoreDTO>.Filter.Eq("Id", store.Id);
            //await storesCollection.ReplaceOneAsync(filter, store);

            //var storeHistoryCollection = ConnectToMongo<StoreHistoryModel>(storeHistoryCollection);
            //await storeHistoryCollection.InsertOneAsync(new StoreHistoryModel(store));
            var client = new MongoClient(ConnectionString);
            using var session = await client.StartSessionAsync();

            session.StartTransaction();

            try
            {
                var db = client.GetDatabase(DatabaseName);
                var chorsCollection = db.GetCollection<ChoreModel>(ChoreCollection);
                var filter = Builders<ChoreModel>.Filter.Eq("Id", chore.Id);
                await choresCollection.ReplaceOneAsync(filter, chore);

                var choreHistoryCollection = db.GetCollection<ChoreHistoryModel>(ChoreHistoryCollection);
                await choreHistoryCollection.InsertOneAsync(new ChoreHistoryModel(chore));

                await session.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await session.AbortTransactionAsync();
                Console.WriteLine(ex.Message);

            }

        }*/
    }
}