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

        public async Task<bool> delete(int id)
        {
            try
            {
                var foundUser = await _userRepository.Get(u => u.Id == id);
                if (foundUser.Id == null)
                    throw new TaskCanceledException("The user does not exist");
                bool response = await _userRepository.Delete(foundUser);
                if (response)
                    throw new TaskCanceledException("The deletion was not possible");
                return response;
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

        public async Task<UserDTO> update(UserDTO user)
        {
            try
            {
                var modelUser = _mapper.Map<User>(user);
                var foundUser = await _userRepository.Get(u => u.Id == modelUser.Id);
                if (foundUser == null)
                    throw new TaskCanceledException("User not found");

                foundUser.Name = modelUser.Name;
                foundUser.Email = modelUser.Email;
                foundUser.RoleId = modelUser.RoleId;
                foundUser.Password = modelUser.Password;
                foundUser.IsActive = modelUser.IsActive;

                var response = await _userRepository.Update(_mapper.Map<User>(foundUser));

                return response == null ? 
                throw new TaskCanceledException("The edition was not possible") : _mapper.Map<UserDTO>(response);
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
                    throw new TaskCanceledException("The user does not exists");

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
