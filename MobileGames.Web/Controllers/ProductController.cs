using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;
using MobileGames.Web.Models;
using MobileGames.Web.Services.IServices;
using Newtonsoft.Json;

namespace MobileGames.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _service;
    // GET
    public ProductController(IProductService service)
    {
        _service = service;
    }

    public async Task<IActionResult> ProductIndex()
    {
        List<ProductDto> list = new();
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var response = await _service.GetAllProductsAsync<ResponseDto>(accessToken);
        if (response is {IsSuccess: true})
        { 
            list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
        }
        return View(list);
    }

    public async Task<IActionResult> ProductCreate()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProductCreate(ProductDto model)
    {
        if (!ModelState.IsValid) return View(model);
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var response = await _service.CreateProductAsync<ResponseDto>(model, accessToken);
        if (response is {IsSuccess: true})
        {
            return RedirectToAction(nameof(ProductIndex));
        }

        return View(model);
    }

    public async Task<IActionResult> ProductEdit(int productId)
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var response = await _service.GetProductByIdAsync<ResponseDto>(productId, accessToken);
        if (response is {IsSuccess: true})
        {
            var model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            return View(model);
        } 
        
        return NotFound();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProductEdit(ProductDto model)
    {
        if (!ModelState.IsValid) return View(model);
        
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var response = await _service.UpdateProductAsync<ResponseDto>(model, accessToken);
        if (response is {IsSuccess: true})
        {
            return RedirectToAction(nameof(ProductIndex));
        }

        return View(model);
    }

    public async Task<IActionResult> ProductDelete(int productId)
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var response = await _service.GetProductByIdAsync<ResponseDto>(productId, accessToken);
        if (response is {IsSuccess: true})
        {
            var model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            return View(model);
        } 
        
        return NotFound();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProductDelete(ProductDto model)
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var response = await _service.DeleteProductAsync<ResponseDto>(model.ProductId, accessToken);
        if (response is {IsSuccess: true})
        {
            return RedirectToAction(nameof(ProductIndex));
        }

        return View(model);
    }
}