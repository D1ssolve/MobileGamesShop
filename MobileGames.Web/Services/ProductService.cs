using MobileGames.Web.Models;
using MobileGames.Web.Services.IServices;

namespace MobileGames.Web.Services;

public class ProductService : BaseService, IProductService
{
    public ProductService(IHttpClientFactory httpClient) 
        : base(httpClient)
    {
    }

    public async Task<T> GetAllProductsAsync<T>()
    {
        return await SendAsync<T>(new ApiRequest
        {
            ApiType = Config.ApiType.GET,
            Url = Config.ProductApiBase + "/api/products",
            AccessToken = ""
        });
    }

    public async Task<T> GetProductByIdAsync<T>(int id)
    {
        return await SendAsync<T>(new ApiRequest
        {
            ApiType = Config.ApiType.GET,
            Url = Config.ProductApiBase + $"/api/products/{id}",
            AccessToken = ""
        });
    }

    public async Task<T> CreateProductAsync<T>(ProductDto productDto)
    {
        return await SendAsync<T>(new ApiRequest
        {
            ApiType = Config.ApiType.POST,
            Data = productDto,
            Url = Config.ProductApiBase + "/api/products",
            AccessToken = ""
        });
    }

    public async Task<T> UpdateProductAsync<T>(ProductDto productDto)
    {
        return await SendAsync<T>(new ApiRequest
        {
            ApiType = Config.ApiType.PUT,
            Data = productDto,
            Url = Config.ProductApiBase + "/api/products",
            AccessToken = ""
        });
    }

    public async Task<T> DeleteProductAsync<T>(int id)
    {
        return await SendAsync<T>(new ApiRequest
        {
            ApiType = Config.ApiType.DELETE,
            Url = Config.ProductApiBase + $"/api/products/{id}",
            AccessToken = ""
        });
    }
}