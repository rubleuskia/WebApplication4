using System;
using System.IO;
using AutoMapper;
using Core.Users.Model;
using DatabaseAccess.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.PhotoBase64, opt => opt.MapFrom(x => GetBase64(x.DatabasePhoto)))
                .ForMember(dest => dest.PhotoPath, opt => opt.Ignore())
                .ForMember(dest => dest.Photo, opt => opt.Ignore());

            CreateMap<UserViewModel, User>()
                .ForMember(dest => dest.DatabasePhoto, opt => {
                    opt.PreCondition(src => src.Photo != null);
                    opt.MapFrom(x => GetPhotoBinaryData(x.Photo));
                })
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(x => x.Email))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(x => x.Age ?? 0))
                .ForMember(dest => dest.PhotoId, opt => opt.Ignore())
                .ForMember(dest => dest.Accounts, opt => opt.Ignore())
                .ForMember(dest => dest.Photo, opt => opt.Ignore());
        }

        // move to utils
        private string GetBase64(byte[] bytes)
        {
            return bytes != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(bytes)}" : null;
        }

        // move to utils
        private static byte[] GetPhotoBinaryData(IFormFile formFile)
        {
            using var binaryReader = new BinaryReader(formFile.OpenReadStream());
            return binaryReader.ReadBytes((int) formFile.Length);
        }
    }
}
