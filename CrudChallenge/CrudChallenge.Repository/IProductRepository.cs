using CrudChallenge.Model;

namespace CrudChallenge.Repository
{
    public interface IProductRepository
    {
        public Task<Product?> GetProductById(string Id);

        Task<Product> InsertProduct(ProductDTO product);

        Task<Product> UpdateProduct(ProductDTO product);

        Task DeleteProduct(string productId);
    }
}