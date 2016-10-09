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

      
       
        public UserProfileServise(IUnitOfWork iuow)
        {
            Database = iuow;
            //    config = new Configuration();
            //config.Configure();

            mapper = conf.CreateMapper();

        }

        /// <summary>
        /// Method finds user by name
        /// </summary>
        /// <param name="userName">User username</param>
        /// <returns>Return "OperationDetails" object, that gives informanion if method been executed successful </returns>
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


      



    }
}
