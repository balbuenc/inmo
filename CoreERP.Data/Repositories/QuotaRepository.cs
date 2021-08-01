using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class QuotaRepository : IQuotaRepository
    {
        private SqlConfiguration _connectionString;

        public QuotaRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteQuota(int id)
        {
            try
            {
                var db = dbConnection();

                var sql = @"DELETE from cuotas
                        WHERE id_cuota = @Id ";

                var result = await db.ExecuteAsync(sql, new { Id = id });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Quota>> GetAllQuotas()
        {
            try
            {
                var db = dbConnection();
                var sql = @"select c.id_cuota, c.id_credito, c.id_venta, c.cuota, c.monto_capital , c.monto_interes ,c.multa
                            ,c.monto_capital +c.monto_interes + c.multa as total
                            ,c.mora, c.estado , c.id_moneda , m.moneda, c.vencimiento, c.fecha_pago 
                            from public.cuotas c 
                            left outer join monedas m on m.id_moneda = c.id_moneda 
                            order by c.id_cuota asc";

                var result = await db.QueryAsync<Quota>(sql, new { });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Quota> GetQuotaDetails(int id)
        {
            try
            {
                var db = dbConnection();
                var sql = "select * from cuotas  where id_cuota = @Id";


                return await db.QueryFirstOrDefaultAsync<Quota>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> InsertQuota(Quota cuota)
        {
            try
            {
                var db = dbConnection();

                var sql = @"INSERT INTO public.cuotas
                            (id_credito, cuota, monto_capital, monto_interes, vencimiento, fecha_pago, multa, mora, id_venta)
                            VALUES(@id_credito, @cuota, @monto_capital, @monto_interes, @vencimiento, @fecha_pago, @multa, @mora, @id_venta);";

                var result = await db.ExecuteAsync(sql, new
                {
                    cuota.id_credito,
                    cuota.id_venta,
                    cuota.monto_capital,
                    cuota.monto_interes,
                    cuota.vencimiento,
                    cuota.fecha_pago,
                    cuota.multa,
                    cuota.mora
                }
                );

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateQuota(Quota cuota)
        {
            try
            {
                var db = dbConnection();

                var sql = @"UPDATE public.cuotas
                            SET id_credito=@id_credito, cuota=@cuota, monto_capital=@monto_capital, monto_interes=@monto_interes, vencimiento=@vencimiento, fecha_pago=@fecha_pago, multa=@multa, mora=@mora, id_venta=@id_venta
                            where id_cuota=@id_cuota;";

                var result = await db.ExecuteAsync(sql, new
                {
                    cuota.id_credito,
                    cuota.id_venta,
                    cuota.monto_capital,
                    cuota.monto_interes,
                    cuota.vencimiento,
                    cuota.fecha_pago,
                    cuota.multa,
                    cuota.mora,
                    cuota.id_cuota
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
