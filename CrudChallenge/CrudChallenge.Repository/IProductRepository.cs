using CrudChallenge.Model;

namespace CrudChallenge.Repository
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