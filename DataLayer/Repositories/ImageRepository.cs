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
    class ImageRepository : IRepository<Image>
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<Image> images;
       

        public ImageRepository(ApplicationContext context)
        {
            _context = context;
            images = _context.Images;
        }

        public IEnumerable<Image> GetAll()
        {
            return images;
        }


        public Image GetSingle(string id)
        {
            return images.FirstOrDefault(o => o.Id == id);
        }


        public IEnumerable<Image> FindBy(Expression<Func<Image, bool>> predicate)
        {
            return images.Where(predicate);
        }

        public void Create(Image image)
        {
            images.Add(image);
        }

        public void Update(Image image)
        {
            _context.Entry(image).State = EntityState.Modified;
        }

        public void Delete(Image image)
        {
            images.Remove(image);
        }

        /*  public Task<List<ClientProfile>> GetAllAsync()
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
