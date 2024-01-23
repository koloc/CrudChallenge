using CrudChallenge.Model;
using CrudChallenge.Repository;

namespace CrudChallenge.Data.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();

        Task<Product?> GetProductByIdAsync(string Id);

        Task<Product> InsertProductAsync(ProductDTO product);

        Task<Product> UpdateProductAsync(ProductDTO product);

        Task DeleteProductAsync(string productId);
    }
}