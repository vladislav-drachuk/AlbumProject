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
    public class RelationshipRepository : IRepository<Relationship>
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<Relationship> relationships;


        public RelationshipRepository(ApplicationContext context)
        {
            _context = context;
            relationships = _context.Relationships;
        }

        public IEnumerable<Relationship> GetAll()
        {
            return relationships;
        }


        public Relationship GetSingle(string id)
        {
            return relationships.FirstOrDefault(o => o.Id == id);
        }


        public IEnumerable<Relationship> FindBy(Expression<Func<Relationship, bool>> predicate)
        {
            return relationships.Where(predicate);
        }

        public void Create(Relationship relationship)
        {
            relationships.Add(relationship);
        }

        public void Update(Relationship relationship)
        {
            _context.Entry(relationship).State = EntityState.Modified;
        }

        public void Delete(Relationship relationship)
        {
            relationships.Remove(relationship);
        }
    }
}
