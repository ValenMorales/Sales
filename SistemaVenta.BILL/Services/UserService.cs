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
using Microsoft.EntityFrameworkCore;


namespace SistemaVenta.BILL.Services
{
    public class UserService : IUserService
    {

        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;
        public async Task<UserDTO> create(UserDTO user)
        {
            try
            {
                var createdUser = await _userRepository.Create(_mapper.Map<User>(user));

                if (createdUser.Id == 0)
                    throw new TaskCanceledException("The user can't be created");

                var query = await _userRepository.GetAll(u => u.Id == user.Id);

                createdUser = query.Include(role => role.Role).First();

                return _mapper.Map<UserDTO>(createdUser);
            }
            catch
            {
                throw;
            }
        }

        public Task<bool> delete(int id)
        {
            try
            {

            }
            catch
            {
                throw;
            }
        }

        public async Task<List<UserDTO>> list()
        {
            try
            {
                var query = await _userRepository.GetAll();
                var userList = query.Include(role => role.Role).ToList();
                return _mapper.Map<List<UserDTO>>(userList);
            }
            catch
            {
                throw;
            }
        }

        public Task<UserDTO> update(UserDTO user)
        {
            try
            {

            }
            catch
            {
                throw;
            }
        }

        public async Task<SessionDTO> validateCredentials(string email, string password)
        {
            try
            {
                var user = await _userRepository.GetAll(u =>
                u.Email == email && u.Password == password);
                if (user.FirstOrDefault() == null)
                    throw new TaskCanceledException("The user does not exists")

                User returnUser = user.Include(role => role.Role).First();
                return _mapper.Map<SessionDTO>(returnUser);

                
            }
            catch
            {
                throw;
            }
        }
    }
}
