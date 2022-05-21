using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using FoodTruckJunkie.Model;

namespace FoodTruckJunkie.Repository
{
    public class PermitRepository : BaseRepository, IPermitRepository
    {
        private AppConfig _appconfig;
        private const string StoredProcSearchLatitudeLongtitude = "spSearchLatitudeLongtitude";

        public PermitRepository(AppConfig appconfig) : base(appconfig.MySQLConnectionString)
        {
            _appconfig = appconfig;
        }

        public IEnumerable<LatitudeLongTitudeSearchResult> SearchLatitudeLongtitude(decimal latitude, decimal longtitude, int distantMiles)
        {
            // var p = new DynamicParameters();
            // p.Add("@latitude", dbType: DbType.Decimal, direction: ParameterDirection.Input);
            // p.Add("@longtitude", dbType: DbType.Decimal, direction: ParameterDirection.Input);
            // p.Add("@distant", dbType: DbType.Int32, direction: ParameterDirection.Input);

            // _db.Execute(StoredProcSearchLatitudeLongtitude, p, commandType: CommandType.StoredProcedure);

            var result = _db.Query<LatitudeLongTitudeSearchResult>("SP_SearchLatiitudeLongtitude",
                new {latitude = latitude, longtitude = longtitude, distantMiles = distantMiles}, 
                commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}