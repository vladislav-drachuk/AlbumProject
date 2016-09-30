using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlbumProject.BusinessLogicLayer.DataTransferObjects;
using WebLayer.Models;


namespace WebLayer
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration MapperConfiguration;

        public static void RegisterMappings()
        {
            MapperConfiguration = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserProfileDTO, UserModel>()
                   .ForMember(dest => dest.IsFollowing, opt => opt.Ignore())
                   .ForMember(dest => dest.IsBlocked, opt => opt.Ignore())
                   .ForMember(dest => dest.IsFollowing, opt => opt.Ignore())
                   .ForMember(dest => dest.Images, opt => opt.Ignore());
                cfg.CreateMap<ImageDTO, ImageModel>()
                   .ForMember(dest => dest.IsLiked, opt => opt.Ignore())
                   .ForMember(dest => dest.CountOfLikes, opt => opt.MapFrom(src => src.Likes.Count))
                   .ForMember(dest => dest.UserName,
                                opt => opt.MapFrom(src => src.User.UserName))
                ;
                  
            });
        }
    }
}