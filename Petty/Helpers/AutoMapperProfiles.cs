using AutoMapper;
using Petty.DTO;
using Petty.Entities;
using Petty.Extensions;

namespace Petty.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<RegisterDto, AppUser>();
        CreateMap<AppUser, MemberDto>()
            .ForMember(x => x.Age, opt => opt.MapFrom(x => x.DateOfBirth.CalculateAge()));
        CreateMap<Pet, PetDto>()
            .ForMember(x => x.PhotoUrl, opt => opt.MapFrom(x => x.Photos.FirstOrDefault(x => x.IsMain).Url));
        CreateMap<Photo, PhotoDto>();
        CreateMap<MemberUpdateDto, AppUser>();
        CreateMap<PetUpdateDto, Pet>();

        
    }
}