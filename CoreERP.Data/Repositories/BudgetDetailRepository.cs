using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class BudgetDetailRepository : IBudgetDetailRepository
    {
        private SqlConfiguration _connectionString;

        public BudgetDetailRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteBudgetDetail(int id)
        {
            var db = dbConnection();

            var sql = @"DELETE FROM public.presupuesto_detalles
                        WHERE id_presupuesto=0 AND id_producto=@id_producto;";

            var result = await db.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        public async Task<IEnumerable<BudgetDetails>> GetAllBudgetDetails()
        {
            var db = dbConnection();
            var sql = @"select pd.id_presupuesto, pd.id_producto, pd.id_descuento, pd.cantidad, pd.costo, pd.precio, p.codigo, p.producto , d.descuento, d.porcentaje 
                        from presupuesto_detalles pd
                        inner join productos p on p.id_producto = pd.id_producto
                        left outer join descuentos d on d.id_descuento = pd.id_descuento";

            return await db.QueryAsync<BudgetDetails>(sql, new { });
        }

        public async Task<BudgetDetails> GetBudgetDetailDetails(int id)
        {
            var db = dbConnection();
            var sql = @"select pd.id_presupuesto, pd.id_producto, pd.id_descuento, pd.cantidad, pd.costo, pd.precio, p.codigo, p.producto , d.descuento, d.porcentaje 
                        from presupuesto_detalles pd
                        inner join productos p on p.id_producto = pd.id_producto
                        left outer join descuentos d on d.id_descuento = pd.id_descuento 
                        where pd.id_presupuesto = @Id";


            return await db.QueryFirstOrDefaultAsync<BudgetDetails>(sql, new { Id = id });
        }

        public async Task<bool> InsertBudgetDetail(BudgetDetails budgetDetail)
        {
            var db = dbConnection();

            var sql = @"INSERT INTO public.presupuesto_detalles
                        (id_presupuesto, id_producto, id_descuento, cantidad, costo, precio)
                        VALUES(@id_presupuesto, @id_producto, @id_descuento, @cantidad, @costo, @precio);";

            var result = await db.ExecuteAsync(sql, new
            {
                budgetDetail.id_presupuesto,
                budgetDetail.id_producto,
                budgetDetail.id_descuento,
                budgetDetail.cantidad,
                budgetDetail.costo,
                budgetDetail.precio
            }
            );

            return result > 0;
        }

        public async Task<bool> UpdateBudgetDetail(BudgetDetails budgetDetail)
        {
            var db = dbConnection();

            var sql = @"UPDATE public.presupuesto_detalles
                        SET id_descuento=@id_descuento, cantidad=@cantidad, costo=@costo, precio=@precio
                        WHERE id_presupuesto=@id_presupuesto AND id_producto=@id_producto;";

            var result = await db.ExecuteAsync(sql, new
            {
                budgetDetail.id_presupuesto,
                budgetDetail.id_producto,
                budgetDetail.id_descuento,
                budgetDetail.cantidad,
                budgetDetail.costo,
                budgetDetail.precio
            }
            );

            return result > 0;
        }
    }
}
