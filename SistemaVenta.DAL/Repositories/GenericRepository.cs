using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DAL.Repositories.Contract;
using SistemaVenta.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SistemaVenta.DAL.Repositories
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {

        private readonly DbsalesContext dbContext;

        public GenericRepository(DbsalesContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TModel> Create(TModel model)
        {
            try
            {
                dbContext.Set<TModel>().Add(model);
                await dbContext.SaveChangesAsync();
                return model;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete(TModel model)
        {
            try
            {
                dbContext.Set<TModel>().Remove(model);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<TModel> Get(Expression<Func<TModel, bool>> filter)
        {
            try
            {
                TModel model = await dbContext.Set<TModel>().FirstOrDefaultAsync(filter);
                return model;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IQueryable<TModel>> GetAll(Expression<Func<TModel, bool>> filter = null)
        {
            try
            {
                IQueryable<TModel> queryModel = filter == null? dbContext.Set<TModel>() : dbContext.Set<TModel>().Where(filter);
                return queryModel;
            }
            catch
            {
                throw;
            }
        }

        public async Task<TModel> Update(TModel model)
        {
            try
            {

                dbContext.Set<TModel>().Update(model);
                await dbContext.SaveChangesAsync();
                return model;
            }
            catch
            {
                throw;
            }
        }
    }
}
