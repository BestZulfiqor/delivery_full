using AutoMapper;
using Core.DTOs.CourierDto;
using Core.DTOs.MenuDto;
using Core.DTOs.OrderDetailDto;
using Core.DTOs.OrderDto;
using Core.DTOs.RestaurantDto;
using Core.DTOs.UserDto;
using Core.Entities;

namespace Infrastructure.AutoMapper;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<Courier, GetCourierDto>();
        CreateMap<CreateCourierDto, Courier>();
        CreateMap<UpdateCourierDto, Courier>();

        CreateMap<User, GetUserDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>();
        
        CreateMap<Restaurant, GetRestaurantDto>();
        CreateMap<CreateRestaurantDto, Restaurant>();
        CreateMap<UpdateRestaurantDto, Restaurant>();

        CreateMap<Order, GetOrderDto>();
        CreateMap<CreateOrderDto, Order>();
        CreateMap<UpdateOrderDto, Order>();

        CreateMap<OrderDetail, GetOrderDetailDto>();
        CreateMap<CreateOrderDetailDto, OrderDetail>();
        CreateMap<UpdateOrderDetailDto, OrderDetail>();

        CreateMap<Courier, GetCourierDto>();
        CreateMap<CreateCourierDto, Courier>();
        CreateMap<UpdateCourierDto, Courier>();

        CreateMap<Menu, GetMenuDto>();
        CreateMap<CreateMenuDto, Menu>();
        CreateMap<UpdateMenuDto, Menu>();
    }
}