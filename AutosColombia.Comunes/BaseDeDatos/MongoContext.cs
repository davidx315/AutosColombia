using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutosColombia.Comunes.BaseDeDatos
{
    public class MongoContext
    {
        public static IMongoDatabase Database()
        {
            // Reading credentials from Web.config file   
            var MongoDatabaseName = ConfigurationManager.AppSettings["MongoDatabaseName"]; //CarDatabase  
            string conectionstring = ConfigurationManager.AppSettings["MongoConectionstring"]; //demouser  
            var _client = new MongoClient(conectionstring);
            var _database = _client.GetDatabase(MongoDatabaseName);

            return _database;
        }
    }
}
