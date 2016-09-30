using AlbumProject.DataLayer.EF;
using AlbumProject.DataLayer.Entities;
using AlbumProject.DataLayer.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;
using AlbumProject.DataLayer.Identity;

namespace AlbumProject.DataLayer.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;

        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        

        IRepository<Image> imageManager;
        IRepository<Comment> commentManager;
        IRepository<Like> likeManager;
        IRepository<Relationship> relationshipManager;
        IRepository<Notification> notificationManager;

        public IdentityUnitOfWork(string connectionString)
        {
            db = new ApplicationContext(connectionString);
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            notificationManager = new NotificationRepository(db);
            imageManager = new ImageRepository(db);
            commentManager = new CommentRepository(db);
            likeManager = new LikeRepository(db);
            relationshipManager = new RelationshipRepository(db);
        }

        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return roleManager; }
        }

     
        public IRepository<Notification> NotificationManager
        {
            get { return notificationManager; }
        }

        public IRepository<Image> ImageManager
        {
            get { return imageManager; }
        }

        public IRepository<Comment> CommentManager
        {
            get { return commentManager; }
        }
        public IRepository<Like> LikeManager
        {
            get { return likeManager; }
        }
        public IRepository<Relationship> RelationshipManager
        {
            get { return relationshipManager; }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                   
                }
                this.disposed = true;
            }
        }
    }
}
