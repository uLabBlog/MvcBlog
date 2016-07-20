using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _Proje_Blog_.EF.CRUD
{
    public class BaseRepository<TEntity> where TEntity : class
    {
        private BlogContext _context = new BlogContext();

        public void Create(TEntity obj)
        {
            _context.Set<TEntity>().Add(obj);
            _context.SaveChanges();
        }
        public void Update(TEntity obj)
        {
            _context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        public void Remove(TEntity obj)
        {
            _context.Set<TEntity>().Remove(obj);
            _context.SaveChanges();
        }
        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }
        public List<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }
    }
}