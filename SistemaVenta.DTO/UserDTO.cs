using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public int? RoleId { get; set; }

        public string? RoleDescription { get; set; }

        public string? Password { get; set; }

        public int? IsActive { get; set; }

    }
}
