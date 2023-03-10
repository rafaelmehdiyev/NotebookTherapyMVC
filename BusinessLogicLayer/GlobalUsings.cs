global using AutoMapper;
global using BusinessLogicLayer.Services.Abstract;
global using BusinessLogicLayer.Services.Concrete;
global using BusinessLogicLayer.Utilities.Extensions;
global using BusinessLogicLayer.Utilities.Results;
global using BusinessLogicLayer.Utilities.Security.Encrption;
global using DataAccessLayer;
global using DataAccessLayer.Repositories;
global using Entities.Abstract;
global using Entities.Concrete;
global using Entities.DTOs.Blog;
global using Entities.DTOs.Cart;
global using Entities.DTOs.CartItem;
global using Entities.DTOs.Category;
global using Entities.DTOs.FAQ;
global using Entities.DTOs.FAQCategory;
global using Entities.DTOs.Favourite;
global using Entities.DTOs.Product;
global using Entities.DTOs.ProductCollection;
global using Entities.DTOs.Review;
global using Entities.DTOs.Sale;
global using Entities.DTOs.SaleItem;
global using Entities.DTOs.Shipping;
global using FluentValidation;
global using FluentValidation.AspNetCore;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.IdentityModel.Tokens;
global using System.Reflection;
global using System.Text;