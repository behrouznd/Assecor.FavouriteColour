using AutoMapper;
using Entities.Models;
using Shared.DataTransferObject.People;
using Shared.Enums;

namespace FavouriteColour;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Person, PersonDto>()
            .ForMember(des => des.name, opt => opt.MapFrom(src => src.Name))
            .ForMember(des => des.lastname, opt => opt.MapFrom(src => src.LastName))
            .ForMember(des => des.zipcode, opt => opt.MapFrom(src => GetZipCode(src.Address)))
            .ForMember(des => des.city, opt => opt.MapFrom(src => GetCityName(src.Address)))
            .ForMember(des => des.color, opt => opt.MapFrom(src => Enum.GetName(typeof(Colour), src.Color)))
            .ReverseMap()
            .ForPath(s => s.Address, opt => opt.MapFrom(src => string.Concat(src.zipcode, " ", src.city)))
            .ForPath(s => s.Color, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.color)? 0 : Enum.Parse<Colour>(src.color)))
            ;
    }

    private string GetZipCode(string? address)
    {
        if (string.IsNullOrEmpty(address))
        {
            return string.Empty;
        }
        return address.Split(' ')[0];
    }

    private string GetCityName(string? address)
    {
        if (string.IsNullOrEmpty(address))
        {
            return string.Empty;
        }
        return string.Join(' ', address.Split(" ")[1..]);
    }
}
