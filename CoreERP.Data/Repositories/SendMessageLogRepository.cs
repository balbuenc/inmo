using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class SendMessageLogRepository  : ISendMessageLogRepository
    {
        private SqlConfiguration _connectionString;

        public SendMessageLogRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteSendMessageLog(int id)
        {
            try
            {
                var db = dbConnection();

                var sql = @"DELETE from envio_mensajes
                        WHERE id_envio_mensaje = @Id ";

                var result = await db.ExecuteAsync(sql, new { Id = id });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<SendMessageLog>> GetAllSendMessageLogs()
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT id_envio_mensaje, fecha, id_mensaje, texto, id_fraccion, id_manzana, id_lote, numero_contrato, nombre_para_documento, id_cliente, documento, numero_cuota, monto_cuota, mora_cuota, fecha_vencimiento, telefono_particular, comando, respuesta
                            FROM public.envio_mensajes
                            order by id_envio_mensaje desc";

                var result = await db.QueryAsync<SendMessageLog>(sql, new { });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SendMessageLog> GetSendMessageLogDetails(int id)
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT id_envio_mensaje, fecha, id_mensaje, texto, id_fraccion, id_manzana, id_lote, numero_contrato, nombre_para_documento, id_cliente, documento, numero_cuota, monto_cuota, mora_cuota, fecha_vencimiento, telefono_particular, comando, respuesta
                            FROM public.envio_mensajeswhere id_envio_mensaje = @Id";


                return await db.QueryFirstOrDefaultAsync<SendMessageLog>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> InsertSendMessageLog(SendMessageLog message_log)
        {
            try
            {
                var db = dbConnection();

                var sql = @"INSERT INTO public.envio_mensajes
                            (fecha,  texto, id_fraccion, id_manzana, id_lote, numero_contrato, nombre_para_documento, id_cliente, documento, numero_cuota, monto_cuota, mora_cuota, fecha_vencimiento, telefono_particular, comando, respuesta,id_mensaje)
                            VALUES(CURRENT_DATE,  @texto, @id_fraccion, @id_manzana, @id_lote, @numero_contrato, @nombre_para_documento, @id_cliente, @documento, @numero_cuota, @monto_cuota, @mora_cuota, @fecha_vencimiento, @telefono_particular, @comando, @respuesta,@id_mensaje);
                            ";

                var result = await db.ExecuteAsync(sql, new
                {
                    message_log.fecha,
                    message_log.texto,
                    message_log.id_fraccion,
                    message_log.id_manzana,
                    message_log.id_lote,
                    message_log.numero_contrato,
                    message_log.nombre_para_documento,
                    message_log.id_cliente,
                    message_log.documento,
                    message_log.numero_cuota,
                    message_log.monto_cuota,
                    message_log.mora_cuota,
                    message_log.fecha_vencimiento,
                    message_log.telefono_particular,
                    message_log.comando,
                    message_log.respuesta,
                    message_log.id_mensaje

                }
                );

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateSendMessageLog(SendMessageLog message_log)
        {
            try
            {
                var db = dbConnection();

                var sql = @"UPDATE public.cuentas
                            SET id_tipo_cuenta=@id_tipo_cuenta, denominacion=@denominacion, id_banco=@id_banco, nro_cuenta=@nro_cuenta, id_moneda=@id_moneda
                            WHERE id_cuenta=@id_cuenta;";

                var result = await db.ExecuteAsync(sql, new
                {
                    message_log.texto
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
