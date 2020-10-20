using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private SqlConfiguration _connectionString;

        public CurrencyRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

       

        public async Task<IEnumerable<Currency>> GetAllCurrencies()
        {
            var db = dbConnection();
            var sql = "select * from monedas";


            return await db.QueryAsync<Currency>(sql, new { });
        }

        public async Task<Currency> GetCurrencytDetails(int id)
        {
            var db = dbConnection();
            var sql = "select * from monedas where idmoneda = @Id";


            return await db.QueryFirstOrDefaultAsync<Currency>(sql, new { Id = id });
        }

        public Task<bool> InsertCurrency(Currency currency)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCurrency(Currency currency)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCurrency(int id)
        {
            throw new NotImplementedException();
        }
    }
}
