using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class ClientTypeRepository : IClientTypeRepository
    {

        private SqlConfiguration _connectionString;

        public ClientTypeRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public Task<bool> DeleteClientType(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ClientType>> GetAllClientTypes()
        {
            var db = dbConnection();
            var sql = "select * from tipos_clientes order by id_tipo_cliente asc";

            return await db.QueryAsync<ClientType>(sql, new { });
        }

        public Task<ClientType> GetClientTypeDetails(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertClientType(ClientType clientType)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateClientType(ClientType clientType)
        {
            throw new NotImplementedException();
        }
    }
}
