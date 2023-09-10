using AutoMapper;
using Petty.DTO;
using Petty.Entities;

namespace Petty.Helpers;

public class AutoMapperProfiles : Profile
{

    public AutoMapperProfiles()
    {
        CreateMap<RegisterDto, AppUser>();
        CreateMap<AppUser, MemberDto>();
        CreateMap<Photo, PhotoDto>();
    }
}