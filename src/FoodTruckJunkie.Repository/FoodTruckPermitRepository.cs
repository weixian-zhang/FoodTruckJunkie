using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using FoodTruckJunkie.Model;

namespace FoodTruckJunkie.Repository
{
    public class FoodTruckPermitRepository : BaseRepository, IFoodTruckPermitRepository
    {
        private AppConfig _appconfig;
        private const string StoredProcSearchLatitudeLongtitude = "SP_SearchLatitudeLongtitude";

        public FoodTruckPermitRepository(AppConfig appconfig) : base(appconfig.MySQLConnectionString)
        {
            _appconfig = appconfig;
        }

        public IEnumerable<LatitudeLongitudeSearchResult> SearchLatitudeLongtitude
            (decimal latitude, decimal longtitude, int distantMiles, int noOfResult)
        {
            // var p = new DynamicParameters();
            // p.Add("@latitude", dbType: DbType.Decimal, direction: ParameterDirection.Input);
            // p.Add("@longtitude", dbType: DbType.Decimal, direction: ParameterDirection.Input);
            // p.Add("@distant", dbType: DbType.Int32, direction: ParameterDirection.Input);

            // _db.Execute(StoredProcSearchLatitudeLongtitude, p, commandType: CommandType.StoredProcedure);

            var result = _db.Query<LatitudeLongitudeSearchResult>(StoredProcSearchLatitudeLongtitude,
                new {latitude = latitude, longtitude = longtitude, distantMiles = distantMiles, noOfResult = noOfResult}, 
                commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}