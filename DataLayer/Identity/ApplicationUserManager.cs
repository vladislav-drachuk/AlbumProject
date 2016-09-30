using AlbumProject.DataLayer.Entities;
using Microsoft.AspNet.Identity;

namespace AlbumProject.DataLayer.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
        {

           
        }
    }
}
