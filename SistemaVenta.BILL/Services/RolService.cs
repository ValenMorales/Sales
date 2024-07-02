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

namespace SistemaVenta.BILL.Services
{
    public class RolService: IRolService
    {
        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IMapper _mapper;

        public RolService(IGenericRepository<Role> roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<List<RolDTO>> List()
        {
            try
            {
                var rolesList = await _roleRepository.GetAll();
                return _mapper.Map<List<RolDTO>>(rolesList.ToList());
            }
            catch
            {
                throw;
            }
        }
    }
}
