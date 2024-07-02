using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace SistemaVenta.DAL.Repositories.Contract
{
    public interface IGenericRepository<TModel> where TModel : class
    {
//task es para trabajar de forma asincrona 

        Task<TModel> Get(Expression<Func<TModel, bool>> filter);
        Task<TModel> Create (TModel model);
        Task<TModel> Update (TModel model);
        Task<bool> Delete (TModel model);
//Iqueryable para que permita hacer una consulta 
        Task<IQueryable<TModel>> GetAll 
            (Expression<Func<TModel, bool>> filter = null);
    }
}
