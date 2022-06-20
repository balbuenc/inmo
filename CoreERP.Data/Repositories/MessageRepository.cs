using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private SqlConfiguration _connectionString;

        public MessageRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteMessage(int id)
        {
            try
            {
                var db = dbConnection();

                var sql = @"DELETE from mensajes
                        WHERE id_mensaje = @Id ";

                var result = await db.ExecuteAsync(sql, new { Id = id });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Message>> GetAllMessages()
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT id_mensaje, titulo, mensaje
                            FROM public.mensajes;
                             ";

                var result = await db.QueryAsync<Message>(sql, new { });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Message> GetMessageDetails(int id)
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT id_mensaje, titulo, mensaje
                            FROM public.mensajes;
                            where id_mensaje = @Id";


                return await db.QueryFirstOrDefaultAsync<Message>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> InsertMessage(Message message)
        {
            try
            {
                var db = dbConnection();

                var sql = @"INSERT INTO public.mensajes
                            (titulo, mensaje)
                            VALUES(@titulo, @mensaje);";

                var result = await db.ExecuteAsync(sql, new
                {
                    message.titulo,
                    message.mensaje
                }
                );

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateMessage(Message message)
        {
            try
            {
                var db = dbConnection();

                var sql = @"UPDATE public.mensajes
                            SET mensaje=@mensaje, titulo=@titulo
                            WHERE id_mensaje=@id_mensaje;";

                var result = await db.ExecuteAsync(sql, new
                {
                    message.id_mensaje,
                    message.titulo,
                    message.mensaje
                }
                );

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
