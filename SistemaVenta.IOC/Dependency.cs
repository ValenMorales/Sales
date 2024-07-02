using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaVenta.DAL.DBContext;
using SistemaVenta.DAL.Repositories;
using SistemaVenta.DAL.Repositories.Contract;
using SistemaVenta.Utility;

namespace SistemaVenta.IOC
{
    public static class Dependency
    {
        public static void InjectDependencies( this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<DbsalesContext>(
                options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("sqlString"));
                });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ISaleRepository, SaleRepository > ();

            services.AddAutoMapper(typeof(AutoMapperProfile));
        }
    }
}
