using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class PurchaseDetailsRepository : IPurchaseDetailsRepository
    {
        private SqlConfiguration _connectionString;

        public PurchaseDetailsRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeletePurchaseDetail(int id)
        {
            var db = dbConnection();

            var sql = @"DELETE FROM public.presupuesto_detalles
                        WHERE id_presupuesto_detalle=@Id;";

            var result = await db.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        public async Task<IEnumerable<PurchaseDetails>> GetAllPurchaseDetails()
        {
            var db = dbConnection();
            var sql = @"SELECT id_compra, factura, fecha, estado, codigo, producto, cantidad, monto, imagen, proveedor, vendedor, moneda, total_compra, monto_total
                        FROM public.v_impresion_compras";

            return await db.QueryAsync<PurchaseDetails>(sql, new { });
        }

        public async Task<IEnumerable<PurchaseDetails>> GetPurchaseDetails(int id)
        {
            var db = dbConnection();
            var sql = @"SELECT id_compra, factura, fecha, estado, codigo, producto, cantidad, monto, imagen, proveedor, vendedor, moneda, total_compra, monto_total
                        FROM public.v_impresion_compras
                        where id_compra = @Id";


            return await db.QueryAsync<PurchaseDetails>(sql, new { Id = id });
        }

     



        public async Task<bool> InsertPurchaseDetail(PurchaseDetails purchaseDetail)
        {
            Discount discount;
            Product product;
            Currency currency;
            Purchase purchase;

            try
            {
                var db = dbConnection();

                var sql_presupuesto = "select id_moneda from compras c  where id_compra =@id_compra";
                purchase = await db.QueryFirstOrDefaultAsync<Purchase>(sql_presupuesto, new
                {
                    purchaseDetail.id_compra
                });


                var sql_producto = "select costo, precio, id_moneda from productos d where d.id_producto = @id_producto";
                product = await db.QueryFirstOrDefaultAsync<Product>(sql_producto, new
                {
                    purchaseDetail.id_producto
                }
                );

                purchaseDetail.monto = product.precio;
                purchaseDetail.monto = product.costo;


                var sql_descuento = "select porcentaje from descuentos d where d.id_descuento = @id_descuento";
                discount = await db.QueryFirstOrDefaultAsync<Discount>(sql_descuento, new
                {
                    purchaseDetail.id_compra

                }
                );

                var sql_moneda = "select cotizacion from  monedas m where m.id_moneda =@id_moneda";
                currency = await db.QueryFirstOrDefaultAsync<Currency>(sql_moneda, new
                {
                    product.id_moneda
                }
                );

                if (product.id_moneda != purchase.id_moneda)
                {
                    purchaseDetail.monto = purchaseDetail.monto * currency.cotizacion;
                }

                purchaseDetail.monto = (purchaseDetail.monto - (purchaseDetail.monto * (discount.porcentaje / 100))) * purchaseDetail.cantidad;

                var sql = @"INSERT INTO public.presupuesto_detalles
                        (id_presupuesto, id_producto, id_descuento, cantidad, costo, precio,total)
                        VALUES(@id_presupuesto, @id_producto, @id_descuento, @cantidad, @costo, @precio, @total);";

                var result = await db.ExecuteAsync(sql, new
                {
                    purchaseDetail.id_compra,
                    purchaseDetail.id_producto,
                    
                    purchaseDetail.cantidad,
                 
                    
                    purchaseDetail.monto
                }
                );

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdatePurchaseDetail(PurchaseDetails purchaseDetail)
        {
            Discount discount;
            Product product;

            try
            {
                var db = dbConnection();

                var sql_producto = "select costo, precio from productos d where d.id_producto = @id_producto";
                product = await db.QueryFirstOrDefaultAsync<Product>(sql_producto, new
                {
                    purchaseDetail.id_producto
                }
                );

                purchaseDetail.monto = product.precio;
                purchaseDetail.monto = product.costo;


                var sql_descuento = "select porcentaje from descuentos d where d.id_descuento = @id_descuento";
                discount = await db.QueryFirstOrDefaultAsync<Discount>(sql_descuento, new
                {
                    purchaseDetail.id_compra

                }
                );

                purchaseDetail.monto = (purchaseDetail.monto - (purchaseDetail.monto * (discount.porcentaje / 100))) * purchaseDetail.cantidad;

                var sql = @"UPDATE public.presupuesto_detalles
                        SET id_descuento=@id_descuento, cantidad=@cantidad, costo=@costo, precio=@precio, id_producto=@id_producto, total=@total 
                        WHERE id_presupuesto_detalle=@id_presupuesto_detalle;";

                var result = await db.ExecuteAsync(sql, new
                {
                    purchaseDetail.id_compra,
                    purchaseDetail.id_producto,
                  
                    purchaseDetail.cantidad,
                    purchaseDetail.monto,
                  
                 
                    purchaseDetail.id_compra_detalle
                }
                );

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
