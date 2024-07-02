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
    public class MenuService: IMenuService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<RoleMenu> _roleMenuRepository;
        private readonly IGenericRepository<Menu> _menuRepository;
        private readonly IMapper _mapper;

        public MenuService(IGenericRepository<User> userRepository, IGenericRepository<RoleMenu> roleMenuRepository, IGenericRepository<Menu> menuRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _roleMenuRepository = roleMenuRepository;
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<List<MenuDTO>> List(int userId )
        {
            IQueryable <User> tbUser = await _userRepository.GetAll(u => u.Id == userId);
            IQueryable<RoleMenu> tbRoleMenu = await _roleMenuRepository.GetAll();
            IQueryable<Menu> tbMenu = await _menuRepository.GetAll();

            try
            {
                IQueryable<Menu> tbResult =
                    (from u in tbUser
                     join mr in tbRoleMenu on u.RoleId equals mr.RoleId
                     join m in tbMenu on mr.IdMenu equals m.Id
                     select m).AsQueryable();
                var menuList = tbResult.ToList();
                return _mapper.Map<List<MenuDTO>>(menuList);
            }
            catch
            {
                throw;
            }


        }
    }
}
