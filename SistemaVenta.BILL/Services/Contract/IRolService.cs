using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DTO;


namespace SistemaVenta.BILL.Services.Contract
{
    public interface IRolService
    {
        // task es para manejar tareas asincronicas

        Task<List<RolDTO>> List();
    }
}
