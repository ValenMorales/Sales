using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DTO;

namespace SistemaVenta.BILL.Services.Contract
{
    public interface IUserService
    {

        Task<List<UserDTO>> list();
        Task<SessionDTO> validateCredentials(string email, string password);

        Task<UserDTO> create(UserDTO user);
        Task<UserDTO> update(UserDTO user);
        Task<bool> delete(int id);
    }
}
