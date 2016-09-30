using AlbumProject.DataLayer.Repositories;
using AlbumProject.BusinessLogicLayer.Interfaces;

namespace AlbumProject.BusinessLogicLayer.Servises
{
    public class ServiceCreator : IServiceCreator
    {

        public IUserService CreateUserService(string connection)
        {
            return new UserService(new IdentityUnitOfWork(connection));
        }

        public IImageServise CreateImageServise(string connection)
          {
              return new ImageServise(new IdentityUnitOfWork(connection));
          }
         

        public IFollowingServise CreateFollowingServise(string connection)
        {
            return new FollowingServise(new IdentityUnitOfWork(connection));
        }

        public IUserProfileServise CreateUserProfileServise(string connection)
        {
            return new UserProfileServise(new IdentityUnitOfWork(connection));
        }
      
        public ILikeServise CreateLikeServise(string connection)
        {
            return new LikeServise(new IdentityUnitOfWork(connection));
        }
        
        public IRoleServise CreateRoleServise(string connection)
        {
            return new RoleServise(new IdentityUnitOfWork(connection));
        }
     
    
    }
}
