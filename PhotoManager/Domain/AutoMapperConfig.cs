using AutoMapper;
using Domain.Models;
using PhotoManager.Service.Contract.DataContract;

namespace Domain
{
    public static class AutoMapperConfig
    {
        public static void Create()
        {
            Mapper.CreateMap<UserResponse, User>();
            Mapper.CreateMap<RoleResponse, Role>();

            Mapper.CreateMap<PhotoResponse, Photo>();
            Mapper.CreateMap<Photo, PhotoResponse>();

            Mapper.CreateMap<AlbumResponse, Album>();
            Mapper.CreateMap<Album, AlbumResponse>();

            Mapper.CreateMap<ExtendedSearch, PhotoResponse>()
                .ForMember(c => c.Id, option => option.Ignore())
                .ForMember(c => c.UserId, option => option.Ignore())
                .ForMember(c => c.Image, option => option.Ignore())
                .ForMember(c => c.ImageSize, option => option.Ignore())
                .ForMember(c => c.ImageType, option => option.Ignore())
                .ForMember(c => c.DateCreated, option => option.Ignore());
        }
    }
}
