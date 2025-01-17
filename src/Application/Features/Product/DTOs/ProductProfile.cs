using AutoMapper;

using Application.Featuers.Products.DTOs;
using Domain.Entities;
using Application.Features.DTOs;


public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<Product, CreateProductDto>();
        CreateMap<Product, UpdateProductDto>();
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
    }
}