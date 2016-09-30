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
    public  class LikeRepository : IRepository<Like>
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<Like> likes;


        public LikeRepository(ApplicationContext context)
        {
            _context = context;
            likes = _context.Likes;
        }

        public IEnumerable<Like> GetAll()
        {
            return likes;
        }


        public Like GetSingle(string id)
        {
            return likes.FirstOrDefault(o => o.Id == id);
        }


        public IEnumerable<Like> FindBy(Expression<Func<Like, bool>> predicate)
        {
            return likes.Where(predicate);
        }

        public void Create(Like like)
        {
            likes.Add(like);
        }

        public void Update(Like like)
        {
            _context.Entry(like).State = EntityState.Modified;
        }

        public void Delete(Like like)
        {
            likes.Remove(like);
        }

    }
}
