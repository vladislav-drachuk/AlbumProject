using System;
using System.Collections.Generic;
using AlbumProject.DataLayer.Entities;
using AlbumProject.DataLayer.Interfaces;
using AlbumProject.BusinessLogicLayer.DataTransferObjects;
using AlbumProject.BusinessLogicLayer.Interfaces;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using AlbumProject.BusinessLogicLayer.MappingConfiguration;
using AlbumProject.BusinessLogicLayer.Infrastructure;

namespace AlbumProject.BusinessLogicLayer.Servises
{
    public class UserProfileServise: IUserProfileServise

    {
        private IUnitOfWork Database;
        private MapperConfiguration conf = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile<OrganizationDTOProfile>();
            });
        private IMapper mapper;

       // private Configuration config;
        //private Mapper mapper;
       
        public UserProfileServise(IUnitOfWork iuow)
        {
            Database = iuow;
            //    config = new Configuration();
            //config.Configure();

            mapper = conf.CreateMapper();

        }

        public async Task<UserProfileDTO> FindProfileByName(string userName)
        {
            ApplicationUser user = await Database.UserManager.FindByNameAsync(userName);
            
            var userProfile = mapper.Map<ApplicationUser,UserProfileDTO>(user);
            

            return userProfile;
        }

        public async Task<UserProfileDTO> FindProfileById(string userId)
        {
            ApplicationUser user = await Database.UserManager.FindByIdAsync(userId);
            return mapper.Map<UserProfileDTO>(user);
        }

        public async Task DeleteAllInformation(string userName)
        {
            ApplicationUser user = await Database.UserManager.FindByNameAsync(userName);
            foreach(var img in user.Images)
            {
                Database.ImageManager.Delete(img);
                if ((System.IO.File.Exists(img.Url)))
                {
                    System.IO.File.Delete(img.Url);
                }
                foreach (var like in img.Likes)
                {
                    Database.LikeManager.Delete(like);
                }
               
            }
            await Database.SaveAsync();
        }
        
        private ICollection<UserProfileDTO> GetFollowers(ApplicationUser user)
        {
            var followers = Database.RelationshipManager.FindBy(r => r.FollowingUserId == user.Id).ToList()
                                                        .Select(r => r.FollowerUser).ToList();
            return mapper.Map<ICollection<ApplicationUser>, ICollection<UserProfileDTO>>(followers);
        }

        

        private ICollection<UserProfileDTO> GetFollowings(ApplicationUser user)
        {
            var followings = Database.RelationshipManager.FindBy(r => r.FollowerUserId == user.Id).ToList()
                                                         .Select(r => r.FollowingUser).ToList();
            return mapper.Map<ICollection<ApplicationUser>, ICollection<UserProfileDTO>>(followings);
        }



    }
}
