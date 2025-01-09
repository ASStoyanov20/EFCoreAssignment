// <copyright file="MappingProfile.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using AutoMapper;
using Data.Models;
using Domain.Models.Category;
using Domain.Models.Customer;
using Domain.Models.Product;
using Domain.Models.Sale;

namespace WebHost.Models;


public class MappingProfile : Profile
{
   
    public MappingProfile()
    {
        this.CreateMap<Category, CategoryViewDto>();
        this.CreateMap<CategoryInputDto, Category>();
        this.CreateMap<CategoryUpdateDto, Category>();

        this.CreateMap<Customer, CustomerViewDto>();
        this.CreateMap<CustomerInputDto, Customer>();
        this.CreateMap<CustomerUpdateDto, Customer>();

        this.CreateMap<Product, ProductViewDto>();
        this.CreateMap<ProductInputDto, Product>();
        this.CreateMap<ProductUpdateDto, Product>();

        this.CreateMap<Sale, SaleViewDto>();
        this.CreateMap<SaleInputDto, Sale>();
        this.CreateMap<SaleUpdateDto, Sale>();
    }
}