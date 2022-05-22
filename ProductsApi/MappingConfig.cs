using AutoMapper;
using ProductsApi.Models;
using ProductsApi.Models.Dto;

namespace ProductsApi;

public static class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<ProductDto, Product>();
            config.CreateMap<Product, ProductDto>();
        });

        return mappingConfig;
    }
}