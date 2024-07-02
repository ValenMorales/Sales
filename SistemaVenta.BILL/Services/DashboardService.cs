using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SistemaVenta.DAL.Repositories.Contract;
using SistemaVenta.BILL.Services.Contract;
using SistemaVenta.Model.Models;
using SistemaVenta.DTO;
using System.Globalization;

namespace SistemaVenta.BILL.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _productRepository;

        public DashboardService(ISaleRepository saleRepository, IMapper mapper, IGenericRepository<Product> productRepository)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<DashBoardDTO> Summary()
        {
            DashBoardDTO vmDashBoard = new DashBoardDTO();
            try
            {
                vmDashBoard.TotalSales = await lastWeekTotalSales();
                vmDashBoard.TotalRevenue = await lastWeekTotalIncome();
                vmDashBoard.TotalProducts = await totalProducts();
                List<WeekSalesDTO> weekSalesList = new List<WeekSalesDTO>();

                foreach(KeyValuePair<string,int> item in await lastWeekSales())
                {
                    weekSalesList.Add(
                        new WeekSalesDTO()
                        {
                            Date = item.Key,
                            Total = item.Value
                        });
                }
                vmDashBoard.LastWeekSales = weekSalesList;
            }
            catch
            {
                throw;
            }
            return vmDashBoard;
        }

        private IQueryable<Sale> returnSales(IQueryable<Sale> saleTable, int substractCantity)
        {
            DateTime? lastDate = saleTable.OrderByDescending(s => s.RegisterDate)
                .Select(s => s.RegisterDate).First();
            lastDate = lastDate.Value.AddDays(substractCantity);

            return saleTable.Where(s =>
            s.RegisterDate.Value.Date >= lastDate.Value.Date);

        }

        private async Task<string> lastWeekTotalIncome()
        {
            double total = 0;
            IQueryable<Sale> _saleQuery = await _saleRepository.GetAll();

            if (_saleQuery.Count() > 0)
            {
                var saleTable = returnSales(_saleQuery, -7);
                total = (double) saleTable.Sum(sale => sale.Total.Value);
            }
            return Convert.ToString(total, new CultureInfo("es-ES"));
        }

        private async Task<int> lastWeekTotalSales()
        {
            int total = 0;
            IQueryable<Sale> _saleQuery = await _saleRepository.GetAll();

            if (_saleQuery.Count() > 0)
            {
                var saleTable = returnSales(_saleQuery, -7);
                total = saleTable.Count();
            }
            return total;
        }

        private async Task<int> totalProducts()
        {
            IQueryable<Product> _productQuery = await _productRepository.GetAll();
            int total = _productQuery.Count();
           
            return total;
        }

        private async Task<Dictionary<string, int>> lastWeekSales()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            IQueryable<Sale> _saleQuery = await _saleRepository.GetAll();

            if(_saleQuery.Count() > 0)
            {
                var saleTable = returnSales(_saleQuery, -7);

                result = saleTable
                    .GroupBy(v => v.RegisterDate.Value.Date)
                    .OrderBy(g => g.Key)
                    .Select(dv => new { date = dv.Key.ToString("dd/MM/yyyy"), total = dv.Count() })
                    .ToDictionary(keySelector: r => r.date, elementSelector: r => r.total);
            }
            return result;
        }
    }
}
