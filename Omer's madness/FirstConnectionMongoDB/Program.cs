using System;
using MongoDB.Driver;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            // the generic connection string is : mongodb+srv://OmerBentov:<password>@cluster0.rqspd.azure.mongodb.net/<dbname>?retryWrites=true&w=majority
            MongoClient dbClient = new MongoClient("mongodb+srv://omerbentov:1234123121omer@cluster0-9rwjn.gcp.mongodb.net/C?retryWrites=true&w=majority");

            var dbList = dbClient.ListDatabases().ToList();

            Console.WriteLine("The list of databases on this server is: ");
            foreach (var db in dbList)
            {
                Console.WriteLine(db);
            }
        }
    }
}