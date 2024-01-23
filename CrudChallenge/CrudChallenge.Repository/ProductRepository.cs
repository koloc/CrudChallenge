using System.Net;
using CrudChallenge.Data;
using CrudChallenge.Model;
using Microsoft.EntityFrameworkCore;

namespace CrudChallenge.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly CrudChallengeDbContext _dBContext;

        public ProductRepository(CrudChallengeDbContext dbContext)
        {
            _dBContext = dbContext;
        }

        public async Task<Product?> GetProductById(string Id)
        {
            return await _dBContext.Products.Select(
                s => new Product
                {
                    Id = s.Id,
                    Code = s.Code,
                    Name = s.Name,
                    Price = s.Price,
                    Stock = s.Stock
                }
            ).FirstOrDefaultAsync(s => s.Id == Id);
        }

        public async Task<Product> InsertProduct(ProductDTO product)
        {
            Product newProduct = new()
            {
                Id = Guid.NewGuid().ToString("N"),
                Name = product.Name,
                Code = product.Code,
                Price = product.Price,
                Stock = product.Stock
            };

            _dBContext.Products.Add(newProduct);
            await _dBContext.SaveChangesAsync();

            return newProduct;
        }

        public async Task<Product> UpdateProduct(ProductDTO product)
        {
            var entity = await _dBContext.Products.FirstOrDefaultAsync(s => s.Id == product.Id);

            if (entity == null) return null;

            entity.Name = !string.IsNullOrWhiteSpace(product.Name) ? product.Name : entity.Name;
            entity.Code = !string.IsNullOrWhiteSpace(product.Code) ? product.Code : entity.Code;
            entity.Price = product.Price > 0 ? product.Price : entity.Price;
            entity.Stock = product.Stock;

            await _dBContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteProduct(string productId)
        {
            var entity = new Product()
            {
                Id = productId
            };

            _dBContext.Products.Attach(entity);
            _dBContext.Products.Remove(entity);
            await _dBContext.SaveChangesAsync();
        }
    }
}