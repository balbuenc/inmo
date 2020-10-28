using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private SqlConfiguration _connectionString;

        public BrandRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteBrand(int id)
        {
            var db = dbConnection();

            var sql = @"DELETE from Marcas
                        WHERE id_marca = @Id ";

            var result = await db.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        public async Task<IEnumerable<Brand>> GetAllBrands()
        {
            var db = dbConnection();
            var sql = "select * from marcas order by marca asc";

            return await db.QueryAsync<Brand>(sql, new { });
        }

        public async Task<Brand> GetBrandDetails(int id)
        {
            var db = dbConnection();
            var sql = "select * from marcas  where id_marca = @Id";


            return await db.QueryFirstOrDefaultAsync<Brand>(sql, new { Id = id });
        }

        public async Task<bool> InsertBrand(Brand brand)
        {
            var db = dbConnection();

            var sql = @"INSERT INTO public.marcas (id_origen, marca) VALUES(@id_origen, @marca);";

            var result = await db.ExecuteAsync(sql, new
            {
                brand.id_origen,
                brand.marca
            }
            );

            return result > 0;
        }

        public async Task<bool> UpdateBrand(Brand brand)
        {
            var db = dbConnection();

            var sql = @"UPDATE public.marcas
                        set id_origen =@id_origen,
                            marca = @marca
                        where id_marca=@id_marca;";

            var result = await db.ExecuteAsync(sql, new
            {
                brand.id_origen,
                brand.marca
            }
            );

            return result > 0;
        }
    }
}
