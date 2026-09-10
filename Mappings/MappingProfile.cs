using AutoMapper;
using EComerceAPI.DTOs;
using EComerceAPI.Models;

namespace EComerceAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();

            CreateMap<Product, ProductDto>()
                .ForMember(
                    dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<ProductDto, Product>();

            CreateMap<Cart, CartDto>();

            CreateMap<CartItem, CartItemResponseDto>()
                .ForMember(
                    dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product.Name));

            CreateMap<CartItemDto, CartItem>();

            // Order
            CreateMap<Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>();

            CreateMap<UpdateOrderDto, Order>();

            CreateMap<User, UserDto>();
        }
    }
}