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
    public class CommentRepository : IRepository<Comment>
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<Comment> comments;


        public CommentRepository(ApplicationContext context)
        {
            _context = context;
            comments = _context.Comments;
        }

        public IEnumerable<Comment> GetAll()
        {
            return comments;
        }


        public Comment GetSingle(string id)
        {
            return comments.FirstOrDefault(o => o.Id == id);
        }


        public IEnumerable<Comment> FindBy(Expression<Func<Comment, bool>> predicate)
        {
            return comments.Where(predicate);
        }

        public void Create(Comment comment)
        {
            comments.Add(comment);
        }

        public void Update(Comment comment)
        {
            _context.Entry(comment).State = EntityState.Modified;
        }

        public void Delete(Comment comment)
        {
            comments.Remove(comment);
        }

    }
}
