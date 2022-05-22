using ProductsApi.Models.Dto;

namespace ProductsApi.Repository;

public interface IProductRepository
{
    Task<IEnumerable<ProductDto>> GetProducts();
    Task<ProductDto> GetProductById(int productId);
    Task<ProductDto> CreateUpdateProduct(ProductDto product);
    Task<bool> DeleteProduct(int productId);
}