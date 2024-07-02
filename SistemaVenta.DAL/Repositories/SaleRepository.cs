using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using SistemaVenta.DAL.DBContext;
using SistemaVenta.DAL.Repositories.Contract;
using SistemaVenta.Model.Models;

namespace SistemaVenta.DAL.Repositories
{
    public class SaleRepository: GenericRepository<Sale>, ISaleRepository
    {
        private readonly DbsalesContext dbContext;

        public SaleRepository(DbsalesContext dbContext ): base( dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Sale> Register(Sale sale)
        {
            Sale generatedSale = new Sale();

            using (var transaction = dbContext.Database.BeginTransaction()){
                try
                {
                    foreach(SaleDetail sd in sale.SaleDetails)
                    {
                        Product found_product = dbContext.Products.Where(p =>p.Id == sd.ProductId).First();
                        found_product.Stock = found_product.Stock - sd.Cantity;
                        dbContext.Products.Update( found_product );

                        await dbContext.SaveChangesAsync();

                        DocumentNumber correlative = dbContext.DocumentNumbers.First();

                        correlative.LastNumber = correlative.LastNumber + 1;
                        correlative.RegisterDate = DateTime.Now;

                        dbContext.DocumentNumbers.Update( correlative );
                        await dbContext.SaveChangesAsync();

                        int digits_cantity = 4;
                        string zeros = string.Concat(Enumerable.Repeat('0', digits_cantity));
                        string saleNumber = zeros + correlative.LastNumber.ToString();
                        saleNumber = saleNumber.Substring(saleNumber.Length - digits_cantity, digits_cantity);

                        sale.DocumentNumber = saleNumber;

                        await dbContext.AddAsync(sale);
                        await dbContext.SaveChangesAsync();

                        generatedSale = sale;

                        transaction.Commit();


                    }
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
                return generatedSale;
            }
        }
    }
}
