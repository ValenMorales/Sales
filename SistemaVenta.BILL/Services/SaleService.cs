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
using System.Linq.Expressions;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace SistemaVenta.BILL.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IGenericRepository<SaleDetail> _saleDetailRepository;
        private readonly IMapper _mapper;

        public SaleService(ISaleRepository saleRepository, IGenericRepository<SaleDetail> saleDetailRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _saleDetailRepository = saleDetailRepository;
            _mapper = mapper;
        }

        public async Task<List<SaleDTO>> historial(string searchBy, string saleNumber, string initialDate, string finalDate, string clientName)
        {
            IQueryable<Sale> query = await _saleRepository.GetAll();
            var result = new List<Sale>();

            try
            {
                switch (searchBy)
                {
                    case "date":
                        DateTime in_date = DateTime.ParseExact(initialDate, "dd/MM/yyyy", new CultureInfo("es-ES"));
                        DateTime fin_date = DateTime.ParseExact(finalDate, "dd/MM/yyyy", new CultureInfo("es-ES"));
                        result = await query
                             .Where(data => data.RegisterDate.Value.Date >= in_date.Date && data.RegisterDate.Value.Date <= fin_date.Date)
                             .Include(dv => dv.SaleDetails)
                                 .ThenInclude(sd => sd.Client)
                             .Include(dv => dv.SaleDetails)
                                 .ThenInclude(sd => sd.Product)
                             .ToListAsync();
                        break;
                    case "client":
                        result = await query.Where(data => data.SaleDetails.Any(sd => sd.Client.Name == clientName))
                           .Include(dv => dv.SaleDetails)
                                 .ThenInclude(sd => sd.Client)
                             .Include(dv => dv.SaleDetails)
                                 .ThenInclude(sd => sd.Product)
                             .ToListAsync();
                        break;
                    default:
                        result = await query
                           .Where(data => data.DocumentNumber == saleNumber)
                           .Include(dv => dv.SaleDetails)
                               .ThenInclude(sd => sd.Client)
                           .Include(dv => dv.SaleDetails)
                               .ThenInclude(sd => sd.Product)
                           .ToListAsync();
                        break;
                }

            }
            catch
            {
                throw;
            }
            return _mapper.Map<List<SaleDTO>>(result);
        }

        public async Task<SaleDTO> register(SaleDTO sale)
        {
            try
            {
                var registeredSale = await _saleRepository.Register(_mapper.Map<Sale>(sale));

                if (registeredSale.Id == 0) 
                    throw new TaskCanceledException("The registration was not possible");

                return _mapper.Map<SaleDTO>(registeredSale);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ReportDTO>> report(string initial_date, string final_date)
        {
            IQueryable<SaleDetail> query = await _saleDetailRepository.GetAll();
            var result = new List<SaleDetail>();

            try
            {
                DateTime in_date = DateTime.ParseExact(initial_date, "dd/MM/yyyy", new CultureInfo("es-ES"));
                DateTime fin_date = DateTime.ParseExact(final_date, "dd/MM/yyyy", new CultureInfo("es-ES"));
                result = await query
                    .Include(p => p.Product)
                    .Include(c => c.Client)
                    .Include(s => s.Sale)
                    .Where(
                    sd =>
                    sd.Sale.RegisterDate.Value.Date >= in_date.Date &&
                    sd.Sale.RegisterDate.Value.Date <= fin_date.Date)
                    .ToListAsync();
            }
            catch
            {
                throw;
            }

            return _mapper.Map<List<ReportDTO>>(result);
        }


    }
}
