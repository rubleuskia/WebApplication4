using AutoMapper;
using Core.Users.Models;
using DatabaseAccess.Entities;

namespace Core.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.PhotoPath, opt => opt.Ignore())
                .ForMember(dest => dest.Photo, opt => opt.Ignore());

            CreateMap<UserViewModel, User>()
                .ForMember(x => x.Photo, opt => opt.Ignore())
                .ForMember(x => x.PhotoId, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<CreateUserViewModel, User>()
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age.Value))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}