using System;
using System.Collections.Generic;
using AlbumProject.DataLayer.Entities;
using AlbumProject.DataLayer.Interfaces;
using AlbumProject.BusinessLogicLayer.DataTransferObjects;
using AlbumProject.BusinessLogicLayer.Interfaces;
using AutoMapper;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AlbumProject.BusinessLogicLayer.MappingConfiguration
{
    public class OrganizationDTOProfile : Profile
    {
        public OrganizationDTOProfile()
        {
            CreateMap<ApplicationUser, UserProfileDTO>()
                         .ForMember(dest => dest.Followers, opt => opt.Ignore())
                         .ForMember(dest => dest.Followings, opt => opt.Ignore())
                         .MaxDepth(3); 
            CreateMap<Like, LikeDTO>()
                         .ForMember(dest => dest.LikedUser, opt => opt.MapFrom(scr=>scr.ApplicationUser))
                         .ForMember(dest => dest.LikedUserId, opt => opt.MapFrom(scr => scr.ApplicationUserId))
                         .MaxDepth(3);
            CreateMap<Image, ImageDTO>()
                         .ForMember(dest => dest.User, opt => opt.MapFrom(scr => scr.ApplicationUser))
                         .ForMember(dest => dest.UserId, opt => opt.MapFrom(scr => scr.ApplicationUser.Id))
                         .MaxDepth(3); 
            CreateMap<Comment, CommentDTO>();
            

        }
    }
 
}

