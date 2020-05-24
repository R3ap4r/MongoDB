using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace MongoDB
{
    //Just testing out the possibilities using XML tags
    //it looks like crap though, likely candidate for removal
    /// <summary>
    /// The main database class.
    /// Contains all methods for performing basic database functions.
    /// <list type="bullet">
    /// <item>
    /// <term>InsertRecord</term>
    /// <description>Add a record</description>
    /// </item>
    /// <item>
    /// <term>LoadRecords</term>
    /// <description>Loads records</description>
    /// </item>
    /// <item>
    /// <term>DeleteRecord</term>
    /// <description>Removes a record</description>
    /// </item>
    /// <item>
    /// <term>FindRecord</term>
    /// <description>Finds a record</description>
    /// </item>
    /// </list>
    /// </summary>
    public class MongoCRUD
    {
        private readonly IMongoDatabase db;


        /// <summary>
        /// Opens up an instance of the database specified by the user
        /// </summary>
        /// <param name="database">Name of the database</param>
        public MongoCRUD(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }


        /// <summary>
        /// Adds a record => InsertRecord("Users",new PersonModel {FirstName="Whack" , LastName="A mole" , IsAdmin=true});
        /// </summary>
        /// <typeparam name="T">Specifies which model is being added</typeparam>
        /// <param name="table">Name of the table to insert to</param>
        /// <param name="record">Collection of params that the specified model represents</param>
        public void InsertRecord<T>(string table, T record)
        {
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        /// <summary>
        /// Loads records => LoadRecords&lt;PersonModel&gt;("Users");
        /// </summary>
        /// <typeparam name="T">Specifies which model is being loaded</typeparam>
        /// <param name="table">Name of the table to load from</param>
        public List<T> LoadRecords<T>(string table)
        {
            var collection = db.GetCollection<T>(table);

            return collection.Find(new BsonDocument()).ToList();
        }

        /// <summary>
        /// Delete record =>  DeleteRecord&lt;PersonModel&gt;("Users", firstName, lastName);
        /// </summary>
        /// <typeparam name="T">Specifies which model is being </typeparam>
        /// <param name="table">Name of the table</param>
        /// <param name="firstName">Param to check</param>
        /// <param name="lastName">Param to check</param>
        public void DeleteRecord<T>(string table, string firstName, string lastName)
        {
            var collection = db.GetCollection<T>(table);

            //Build the main filter and delete the record
             
            var remove = Builders<T>.Filter.Eq("FirstName", firstName);
            var remove2 = Builders<T>.Filter.Eq("LastName", lastName);

            collection.DeleteOne(remove&remove2);

        }

        /// <summary>
        /// Find record =>  FindRecord&lt;PersonModel&gt;("Users", lastName);
        /// </summary>
        /// <typeparam name="T">Specifies which model to search for</typeparam>
        /// <param name="table">Specifies which table to look in</param>
        /// <param name="lastName">Param from specified model to use as search option</param>
        /// <returns></returns>
        
        public List<T> FindRecord<T>(string table, string lastName)

        {
            var find = db.GetCollection<T>(table);

            return find.Find(Builders<T>.Filter.Eq("LastName", lastName)).ToList();
        }
    }

    
}
