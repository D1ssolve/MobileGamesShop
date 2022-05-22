using Microsoft.AspNetCore.Mvc;
using ProductsApi.Models.Dto;
using ProductsApi.Repository;

namespace ProductsApi.Controllers;

[Route("api/products/")]
public class ProductApiController : ControllerBase
{
    private readonly ResponseDto _response;
    private readonly IProductRepository _repository;
    
    public ProductApiController(IProductRepository repository)
    {
        _response = new ResponseDto();
        _repository = repository;
    }

    [HttpGet]
    public async Task<ResponseDto> Get()
    {
        try
        {
            var products = await _repository.GetProducts();
            _response.Result = products;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string> {e.ToString()};
        }

        return _response;
    }
    
    [HttpGet("{id:int}")]
    public async Task<ResponseDto> Get(int id)
    {
        try
        {
            var productDto = await _repository.GetProductById(id);
            _response.Result = productDto;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string> {e.ToString()};
        }

        return _response;
    }
    
    [HttpPost]
    public async Task<ResponseDto> Post([FromBody] ProductDto productDto)
    {
        try
        {
            var model = await _repository.CreateUpdateProduct(productDto);
            _response.Result = model;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string> {e.ToString()};
        }

        return _response;
    }
    
    [HttpPut]
    public async Task<ResponseDto> Put([FromBody] ProductDto productDto)
    {
        try
        {
            var model = await _repository.CreateUpdateProduct(productDto);
            _response.Result = model;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string> {e.ToString()};
        }

        return _response;
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ResponseDto> Delete(int id)
    {
        try
        {
            _response.Result = await _repository.DeleteProduct(id);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string> {e.ToString()};
        }

        return _response;
    }
}