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

        public async Task AddToRole(string userName, string roleName)
        {
            ApplicationUser user = await Database.UserManager.FindByNameAsync(userName);
            if (!(await Database.RoleManager.RoleExistsAsync(roleName)))
            {
                ApplicationRole role = new ApplicationRole { Name = roleName };
                
                await Database.RoleManager.CreateAsync(role);
            }
                await Database.UserManager.AddToRoleAsync(user.Id, roleName);
            await Database.SaveAsync();
        }

        public async Task  DeleteFromRole(string userName, string roleName)
        {
            ApplicationUser user = await Database.UserManager.FindByNameAsync(userName);
            var t = await Database.UserManager.RemoveFromRolesAsync(user.Id, roleName);
          
            await Database.SaveAsync();
        }

       
        public  ICollection<UserProfileDTO> GetUsersByRole(string roleName)
        {
            var role =  Database.RoleManager.FindByNameAsync(roleName).Result;
            var u1 = Database.UserManager.Users;
            var u2 = u1.Where(u => u.Roles.Any(r => r.RoleId == role.Id));
            var users = u2.ToList<ApplicationUser>();
            return mapper.Map<ICollection<ApplicationUser>, ICollection<UserProfileDTO>>(users);
        }

        public async Task<bool> IsInRole(string userName, string roleName)
        {
            ApplicationUser user = await Database.UserManager.FindByNameAsync(userName);
            return await Database.UserManager.IsInRoleAsync(user.Id, roleName);
        }
       

    }
}
