using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        private SqlConfiguration _connectionString;

        public QuoteRepository(SqlConfiguration connectionStringg)
        {
            _connectionString  = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteQuote(int id)
        {
            try
            {
                var db = dbConnection();

                var sql = @"DELETE from cuentas
                        WHERE id_cuenta = @Id ";

                var result = await db.ExecuteAsync(sql, new { Id = id });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Quote>> GetAllQuotes()
        {
            try
            {
                var db = dbConnection();
                var sql = @"select vc.*, r.regla , m.mensaje,
                            (select valor from configuraciones c where parametro = 'TigoSMSService' ) || '?key=' || (select valor from configuraciones c where parametro = 'SMSKey' ) || '&message=' ||
                            (select valor from configuraciones c where parametro = 'SMSEncabezado' ) ||
	                        replace(
                            replace(
                            replace(
                            replace(
                            	replace(
		                            replace( 
		                                replace(
		                                    replace(
		                                    replace (m.mensaje, '@cliente', vc.nombre_para_documento)
		                                    ,'@nro_cuota',vc.numero_cuota::varchar(10) 
		                                    ) 
		                                ,'@nro_lote',vc.id_lote::varchar(10)
		                                )
		                            ,'@vencimiento',to_char(vc.fecha_vencimiento,'dd/MM/yyyy')
		                            )
		                        ,'@manzana',vc.id_manzana )
		                    ,'@contrato',vc.numero_contrato )
		                    ,'@documento',vc.documento )
		                    ,'@monto_cuota',vc.monto_cuota::varchar(20))
		                    ,'@fraccion',vc.id_fraccion::varchar(20))
                            || '&msisdn=' ||  replace (vc.telefono_particular,'+5959','09') 
                            as comando
                            from imports.vconsulta_cliente vc
                            inner join public.reglas r on  vc.id_fraccion between r.fraccion_desde and r.fraccion_hasta                                                
                            inner join mensajes m on m.id_mensaje = r.id_mensaje 
                            where 
                            ((vc.fecha_vencimiento + r.dias_vencidos  = current_date and r.tipo = 'D')
                            or (vc.mes_atraso = r.mes_atraso and r.tipo = 'M'))
                            order by vc.fecha_vencimiento asc;";

                var result = await db.QueryAsync<Quote>(sql, new { });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Quote> GetQuoteDetails(int id)
        {
            try
            {
                var db = dbConnection();
                var sql = "select * from cuentas  where id_cuenta = @Id";


                return await db.QueryFirstOrDefaultAsync<Quote>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> InsertQuote(Quote quote)
        {
            try
            {
                var db = dbConnection();

                var sql = @"INSERT INTO public.cuentas
                            (id_tipo_cuenta, denominacion, id_banco, nro_cuenta,id_moneda)
                            VALUES(@id_tipo_cuenta, @denominacion, @id_banco, @nro_cuenta, @id_moneda);";

                var result = await db.ExecuteAsync(sql, new
                {
                    quote.id_fraccion
                }
                );

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateQuote(Quote quote)
        {
            try
            {
                var db = dbConnection();

                var sql = @"UPDATE public.cuentas
                            SET id_tipo_cuenta=@id_tipo_cuenta, denominacion=@denominacion, id_banco=@id_banco, nro_cuenta=@nro_cuenta, id_moneda=@id_moneda
                            WHERE id_cuenta=@id_cuenta;";

                var result = await db.ExecuteAsync(sql, new
                {
                    quote.id_manzana
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
