using System.Collections.Generic;
using System.Data;
using FoodTruckJunkie.Model;
using MySql.Data.MySqlClient;

namespace FoodTruckJunkie.Repository
{
    public class BaseRepository 
    {
        public readonly IDbConnection _db;
        
        public BaseRepository(string connString)
        {
            _db = new MySqlConnection(connString);
        }

        public void Dispose()
        {
            _db.Close();
        }
    }
}