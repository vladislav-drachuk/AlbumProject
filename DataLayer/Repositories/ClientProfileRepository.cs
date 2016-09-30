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
    /*class TagRepository: IRepository<Tag>
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<Tag> tags;
        

        public TagRepository(ApplicationContext context)
        {
            _context = context;
            tags = _context.Tags;
        }

        public IEnumerable<Tag> GetAll()
        {
            return tags;
        }


        public Tag GetSingle(string id)
        {
           return tags.FirstOrDefault(o => o.Id == id);
        }


        public IEnumerable<Tag> FindBy(Expression<Func<Tag, bool>> predicate)
        {
            return tags.Where(predicate);
        }

        public void Create(Tag tag)
        {
            tags.Add(tag);
        }

        public void Update(Tag tag)
        {
            _context.Entry(tag).State = EntityState.Modified;
        }

        public void Delete(Tag tag)
        {
            tags.Remove(tag);
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
       
    
    }
    */
}
