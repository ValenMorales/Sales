using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.BILL.Services.Contract;
using AutoMapper;
using SistemaVenta.DAL.Repositories.Contract;
using SistemaVenta.Model.Models;
using SistemaVenta.DTO;
using Microsoft.EntityFrameworkCore;

namespace SistemaVenta.BILL.Services
{
    public class ProductService: IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDTO> create(ProductDTO product)
        {
            try
            {
                var createdProduct = _productRepository.Create(_mapper.Map<Product>(product));
                if (createdProduct.Id == 0)
                    throw new TaskCanceledException("The product can not be created");

                return _mapper.Map<ProductDTO>(createdProduct);

            }
            catch {
                throw;
                    }
        }

        public async Task<bool> delete(int id)
        {
            try
            {
                var foundProduct = await _productRepository.Get(p =>
                p.Id == id);

                if (foundProduct == null)
                    throw new TaskCanceledException("Product not found");

                if ( await _productRepository.Delete(foundProduct))
                    return true;
                throw new TaskCanceledException("The product can not be deleted");
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ProductDTO>> list()
        {
            try
            {
                var query = await _productRepository.GetAll();
                var productsList = query.Include(cat => cat.Category).ToList();
                return _mapper.Map<List<ProductDTO>>(productsList.ToList());
            }
            catch
            {
                throw;
            }
        }

        public async Task<ProductDTO> update(ProductDTO product)
        {
            try
            {
                var productModel = _mapper.Map<Product>(product);
                var foundProduct = await _productRepository.Get(u =>
                productModel.Id == u.Id
                    );

                if (foundProduct == null)
                    throw new TaskCanceledException("User not found");

                foundProduct.Name = productModel.Name;
                foundProduct.CategoryId = productModel.CategoryId;
                foundProduct.Stock= productModel.Stock;
                foundProduct.Price = productModel.Price;
                foundProduct.IsActive = productModel.IsActive;

                var productResponse = await _productRepository.Update(foundProduct);

                if (productResponse == null)
                    throw new TaskCanceledException("An error ocurred updating the product");
                return _mapper.Map<ProductDTO>(productResponse);

            }
            catch
            {
                throw;
            }
        }
    }
}
