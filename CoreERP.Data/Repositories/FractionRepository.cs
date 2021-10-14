﻿using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class FractionRepository : IFractionRepository
    {
        private SqlConfiguration _connectionString;

        public FractionRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteFraction(int id)
        {
            try
            {
                var db = dbConnection();

                var sql = @"DELETE from public.fracciones_descuento
                        WHERE id = @Id ";

                var result = await db.ExecuteAsync(sql, new { Id = id });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Fraction>> GetAllFractions()
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT id, fraccion, factor_descuento
                            FROM public.fracciones_descuento;
                             ";

                var result = await db.QueryAsync<Fraction>(sql, new { });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Fraction> GetFractionDetails(int id)
        {
            try
            {
                var db = dbConnection();
                var sql = "select * from cuentas  where id_cuenta = @Id";


                return await db.QueryFirstOrDefaultAsync<Fraction>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> InsertFraction(Fraction fraction)
        {
            try
            {
                var db = dbConnection();

                var sql = @"INSERT INTO public.fracciones_descuento
(fraccion, factor_descuento)
VALUES(@fraccion, @factor_descuento);
";

                var result = await db.ExecuteAsync(sql, new
                {
                
                    fraction.fraccion,
                    fraction.factor_descuento
                }
                );

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateFraction(Fraction fraction)
        {
            try
            {
                var db = dbConnection();

                var sql = @"UPDATE public.fracciones_descuento
SET fraccion=@fraccion, factor_descuento=@factor_descuento
WHERE id=@id;
";

                var result = await db.ExecuteAsync(sql, new
                {
                    fraction.id,
                    fraction.fraccion,
                    fraction.factor_descuento
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
