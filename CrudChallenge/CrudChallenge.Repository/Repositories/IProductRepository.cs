using CrudChallenge.Model;
using CrudChallenge.Repository;

namespace CrudChallenge.Data.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();

        Task<Product?> GetProductByIdAsync(string Id);

        Task<Product> InsertProductAsync(CreateProductRequest product);

        Task<Product> UpdateProductAsync(UpdateProductRequest product);

        Task DeleteProductAsync(string productId);
    }
}