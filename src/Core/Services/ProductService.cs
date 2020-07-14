using Core.Dtos;
using Core.Models;
using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository repository;

        public ProductService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task AddProduct(ProductAddRequest productAddRequest)
        {
            var product = new Product() { Name = productAddRequest.Name, Price = productAddRequest.Price};
            await this.repository.Insert<Product>(product);
            await this.repository.Save();
        }

        public async Task<List<Product>> GetProducts()
        {
            return this.repository.GetAll<Product>().ToList();
        }
    }
}
