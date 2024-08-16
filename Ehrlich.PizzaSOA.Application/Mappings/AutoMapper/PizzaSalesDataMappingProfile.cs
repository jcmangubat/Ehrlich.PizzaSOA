using AutoMapper;
using Ehrlich.PizzaSOA.Application.Models;
using Ehrlich.PizzaSOA.Domain.Entities;
using static Ehrlich.PizzaSOA.Domain.Constants.Rules;

namespace Ehrlich.PizzaSOA.Application.Mappings.AutoMapper;

public class PizzaSalesDataMappingProfile : Profile
{
    public PizzaSalesDataMappingProfile()
    {
        CreateMap<PizzaTypeModel, PizzaType>()
            .ForMember(dest => dest.PizzaTypeCode, opt => opt.MapFrom(src => src.PizzaTypeCode))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => Enum.Parse<PizzaTypeCategoriesEnum>(src.Category)))
            .ForMember(dest => dest.Pizzas, opt => opt.Ignore());

        CreateMap<PizzaType, PizzaTypeModel>()
            .ForMember(dest => dest.PizzaTypeCode, opt => opt.MapFrom(src => src.PizzaTypeCode))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()));


        CreateMap<PizzaModel, Pizza>()
            .ForMember(dest => dest.Size, opt => opt.MapFrom(src => Enum.Parse<PizzaSizesEnum>(src.Size)));
        CreateMap<Pizza, PizzaModel>()
            .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size.ToString()));

        CreateMap<Order, OrderModel>().ReverseMap();

        CreateMap<OrderDetail, OrderDetailModel>()
            .ForMember(dest => dest.OrderNo, opt => opt.MapFrom(src => src.Order.OrderNo))
            .ForMember(dest => dest.PizzaCode, opt => opt.MapFrom(src => src.Pizza.PizzaCode));
    }
}
