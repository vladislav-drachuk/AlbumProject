using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using AlbumProject.DataLayer.Interfaces;
using AlbumProject.DataLayer.Entities;
using AlbumProject.DataLayer.EF;
using System.Linq.Expressions;

namespace AlbumProject.DataLayer.Repositories
{
    class NotificationRepository : IRepository<Notification>
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<Notification> notifications;
        

        public NotificationRepository(ApplicationContext context)
        {
            _context = context;
            notifications = _context.Notifications;
        }

        public IEnumerable<Notification> GetAll()
        {
            return notifications;
        }


        public Notification GetSingle(string id)
        {
           return notifications.FirstOrDefault(o => o.Id == id);
        }


        public IEnumerable<Notification> FindBy(Expression<Func<Notification, bool>> predicate)
        {
            return notifications.Where(predicate);
        }

        public void Create(Notification notification)
        {
            notifications.Add(notification);
        }

        public void Update(Notification notification)
        {
            _context.Entry(notification).State = EntityState.Modified;
        }

        public void Delete(Notification clientProfile)
        {
            notifications.Remove(clientProfile);
        }

     /*   public Task<List<ClientProfile>> GetAllAsync()
        {
            return clientProfiles.ToListAsync();
        }

    
        public Task<ClientProfile> GetSingleAsync(int id)
        {
            return clientProfiles.FirstOrDefaultAsync(o => o.Id == id);
        }

       
        public Task<List<ClientProfile>> FindByAsync(Expression<Func<ClientProfile, bool>> predicate)
        {
            return clientProfiles.Where(predicate).ToListAsync();
        }
       
    */
    }
}
