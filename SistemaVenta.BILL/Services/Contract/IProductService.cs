using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DTO;

namespace SistemaVenta.BILL.Services.Contract
{
    public interface IProductService
    {
        Task<List<ProductDTO>> list();

        Task<ProductDTO> create(ProductDTO product);
        Task<ProductDTO> update(ProductDTO user);
        Task<bool> delete(int id);
    }
}
