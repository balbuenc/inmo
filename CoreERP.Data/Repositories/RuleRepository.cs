using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class RuleRepository : IRuleRepository
    {
        private SqlConfiguration _connectionString;

        public RuleRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteRule(int id)
        {
            try
            {
                var db = dbConnection();

                var sql = @"DELETE from reglas
                        WHERE id_regla = @Id ";

                var result = await db.ExecuteAsync(sql, new { Id = id });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Rule>> GetAllRules()
        {
            try
            {
                var db = dbConnection();
                var sql = @"select r.*, m.titulo 
                            from reglas r 
                            inner join mensajes m on m.id_mensaje = r.id_mensaje 
                            order by id_regla desc; ";

                var result = await db.QueryAsync<Rule>(sql, new { });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Rule> GetRuleDetails(int id)
        {
            try
            {
                var db = dbConnection();
                var sql = "select * from reglas  where id_regla = @Id";


                return await db.QueryFirstOrDefaultAsync<Rule>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> InsertRule(Rule rule)
        {
            try
            {
                var db = dbConnection();

                var sql = @"INSERT INTO public.reglas
                            (regla, dias_vencidos, fraccion_desde, fraccion_hasta,  id_mensaje)
                            VALUES(@regla, @dias_vencidos, @fraccion_desde, @fraccion_hasta,  @id_mensaje);
                            ;";

                var result = await db.ExecuteAsync(sql, new
                {
                 
                    rule.regla,
                    rule.dias_vencidos,
                    rule.fraccion_desde,
                    rule.fraccion_hasta,
                    rule.id_mensaje
                }
                );

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateRule(Rule rule)
        {
            try
            {
                var db = dbConnection();

                var sql = @"UPDATE public.reglas
                            SET regla=@regla, dias_vencidos=@dias_vencidos, fraccion_desde=@fraccion_desde, fraccion_hasta=@fraccion_hasta, id_mensaje=@id_mensaje
                            WHERE id_regla=@id_regla;";

                var result = await db.ExecuteAsync(sql, new
                {
                    rule.id_regla,
                    rule.regla,
                    rule.dias_vencidos,
                    rule.fraccion_desde,
                    rule.fraccion_hasta,
                    rule.id_mensaje
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
