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
    public class RoleServise: IRoleServise
    {
        private IUnitOfWork Database;
        private MapperConfiguration conf = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<OrganizationDTOProfile>();
        });
        private IMapper mapper;

        public RoleServise(IUnitOfWork iuow)
        {
            Database = iuow;
            mapper = conf.CreateMapper();
        }

        /// <summary>
        /// Method add user to role
        /// </summary>
        /// <param name="userName">User username</param>
        ///<param name="roleName">Role</param>
        /// <returns>Return "OperationDetails" object, that gives informanion if method been executed successful </returns>
        public async Task<OperationDetails> AddToRole(string userName, string roleName)
        {
            if (userName == null)
            {
                return new OperationDetails(false, "Invalid name", userName);
            }
            else if (roleName == null)
            {
                return new OperationDetails(false, "Invalid role", roleName);
            }
            else
            {


                try
                {
                    ApplicationUser user = await Database.UserManager.FindByNameAsync(userName);
                    if (!(await Database.RoleManager.RoleExistsAsync(roleName)))
                    {
                        ApplicationRole role = new ApplicationRole { Name = roleName };

                        await Database.RoleManager.CreateAsync(role);
                    }
                    await Database.UserManager.AddToRoleAsync(user.Id, roleName);
                    await Database.SaveAsync();
                    return new OperationDetails(true, "", "");
                }

                catch (NullReferenceException)
                {
                    return new OperationDetails(false, "Invalid name", userName);
                }
            }

        }

        /// <summary>
        /// Method add user to role
        /// </summary>
        /// <param name="userName">User username</param>
        ///<param name="roleName">Role</param>
        /// <returns>Return "OperationDetails" object, that gives informanion if method been executed successful </returns>
        public async Task<OperationDetails> DeleteFromRole(string userName, string roleName)
        {
            if (userName == null)
            {
                return new OperationDetails(false, "Invalid name", userName);
            }
            else if (roleName == null)
            {
                return new OperationDetails(false, "Invalid role", roleName);
            }
            else
            {
             try
                {
            ApplicationUser user = await Database.UserManager.FindByNameAsync(userName);
            var t = await Database.UserManager.RemoveFromRolesAsync(user.Id, roleName);
          
            await Database.SaveAsync();
                return new OperationDetails(true, "", "");
            }
           catch (NullReferenceException)
            {
                return new OperationDetails(false, "Invalid name", userName);
            }
        }

    }

        /// <summary>
        /// Method get users to in role
        /// </summary>
        /// <param name="userName">User username</param>
        ///<param name="roleName">Role</param>
        /// <returns>Return collection of users</returns>
        public ICollection<UserProfileDTO> GetUsersByRole(string roleName)
        {  
            var role =  Database.RoleManager.FindByNameAsync(roleName).Result;
            var u1 = Database.UserManager.Users;
            var u2 = u1.Where(u => u.Roles.Any(r => r.RoleId == role.Id));
            var users = u2.ToList<ApplicationUser>();
            return mapper.Map<ICollection<ApplicationUser>, ICollection<UserProfileDTO>>(users);
        }
       
        /// <summary>
        /// Method check if user is in role
        /// </summary>
        /// <param name="userName">User username</param>
        ///<param name="roleName">Role</param>
         public async Task<bool> IsInRole(string userName, string roleName)
        {
            ApplicationUser user = await Database.UserManager.FindByNameAsync(userName);
            return await Database.UserManager.IsInRoleAsync(user.Id, roleName);
        }
       

    }
}
