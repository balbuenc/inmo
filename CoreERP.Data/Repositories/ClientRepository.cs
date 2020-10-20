using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private SqlConfiguration _connectionString;

        public ClientRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteClient(int id)
        {
            var db = dbConnection();

            var sql = @"DELETE from clientes
                        WHERE idcliente = @Id ";

            var result = await db.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            var db = dbConnection();
            var sql = "select * from Clientes";


            return await db.QueryAsync<Client>(sql, new { });
        }

        public async Task<Client> GetClientDetails(int id)
        {
            var db = dbConnection();
            var sql = "select * from Clientes where id_cliente = @Id";


            return await db.QueryFirstOrDefaultAsync<Client>(sql, new { Id = id });
        }

        public async Task<bool> InsertClient(Client client)
        {
            var db = dbConnection();

            var sql = @" INSERT INTO public.clientes
                        (nombres, apellidos, sexo, fecha_nacimiento, ci, ruc, direccion, telefono, email, observaciones, fecha_alta, razon_social, codigo, es_cliente_fiel,  id_ciudad)
                        VALUES(@nombres,@apellidos,@sexo,@fecha_nacimiento,@ci,@ruc,@direccion,@telefono,@email,@observaciones,@fecha_alta,@razon_social,@codigo,@es_cliente_fiel,@id_ciudad);";

            var result = await db.ExecuteAsync(sql, new {   client.nombres,
                                                            client.apellidos,
                                                            client.sexo,
                                                            client.fecha_nacimiento,
                                                            client.ci,
                                                            client.ruc,
                                                            client.direccion,
                                                            client.telefono,
                                                            client.email,
                                                            client.observaciones,
                                                            client.fecha_alta,
                                                            client.razon_social,
                                                            client.codigo,
                                                            client.es_cliente_fiel,
                                                            
                                                            client.id_barrio

                                                        });

            return result > 0;
        }

        public async Task<bool> UpdateClient(Client client)
        {

            var db = dbConnection();

            var sql = @" UPDATE public.clientes
                        set nombres =@nombres, apellidos = @apellidos, sexo =@sexo, fecha_nacimiento =@fecha_nacimiento, ci=@ci, ruc =@ruc, direccion= @direccion, telefono =@telefono, email =@email, observaciones =@observaciones, fecha_alta =@fecha_alta, razon_social =@razon_social, codigo =@codigo, es_cliente_fiel =@es_cliente_fiel,  id_ciudad =@id_ciudad
                        where id_cliente=@id_cliente;";

            var result = await db.ExecuteAsync(sql, new
            {
                client.nombres,
                client.apellidos,
                client.sexo,
                client.fecha_nacimiento,
                client.ci,
                client.ruc,
                client.direccion,
                client.telefono,
                client.email,
                client.observaciones,
                client.fecha_alta,
                client.razon_social,
                client.codigo,
                client.es_cliente_fiel,
                
                client.id_barrio,
                
                client.id_cliente

            });

            return result > 0;
        }
    }
}
