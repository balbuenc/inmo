using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        private SqlConfiguration _connectionString;

        public AreaRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteArea(int id)
        {
            var db = dbConnection();

            var sql = @"DELETE from areas
                        WHERE id_area = @Id ";

            var result = await db.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        public async Task<IEnumerable<Area>> GetAllAreas()
        {
            var db = dbConnection();
            var sql = "select * from Areas order by id_area asc";

            return await db.QueryAsync<Area>(sql, new { });
        }

        public async Task<Area> GetAreaDetails(int id)
        {
            var db = dbConnection();
            var sql = "select * from areas  where id_area = @Id";


            return await db.QueryFirstOrDefaultAsync<Area>(sql, new { Id = id });
        }

        public async Task<bool> InsertArea(Area area)
        {
            var db = dbConnection();

            var sql = @" INSERT INTO public.areas (area) VALUES(@area);";

            var result = await db.ExecuteAsync(sql, new
            {
                area.area
            }
            );

            return result > 0;
        }

        public async Task<bool> UpdateArea(Area area)
        {
            var db = dbConnection();

            var sql = @" UPDATE public.areas
                        set area =@area
                        where id_area=@id_area;";

            var result = await db.ExecuteAsync(sql, new
            {
                area.id_area,
                area.area
            }
            );

            return result > 0;
        }
    }
}
