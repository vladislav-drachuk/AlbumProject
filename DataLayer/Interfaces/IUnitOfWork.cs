using AlbumProject.DataLayer.Identity;
using System;
using System.Threading.Tasks;
using AlbumProject.DataLayer.Entities;

namespace AlbumProject.DataLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }

        
        IRepository<Image> ImageManager { get; }
        IRepository<Comment> CommentManager { get; }
        IRepository<Like> LikeManager { get; }
        IRepository<Relationship> RelationshipManager { get; }
        IRepository<Notification> NotificationManager { get; }

        Task SaveAsync();

    }
}
