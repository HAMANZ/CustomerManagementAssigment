using CustomerManagment.DomainLayer.Model;
using Microsoft.EntityFrameworkCore;
using  CustomerManagment.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.RespositoryPattern
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly WenApiDbContext _context;
        private readonly DbSet<T> _entities;

            public Repository(WenApiDbContext context)
        {
            this._context = context;
            _entities = context.Set<T>();
        }


        public IEnumerable<T> GetAll()
        {

            return _entities.AsEnumerable();
        }
        
        public List<T> GetAllList()
        {

            return _entities.ToList();
        }
           
        public List<T> GetAllList(int limit)
        {

            return _entities.Take(limit).ToList();
        }
        
       
        public T GetById(int id)
        {
            return _entities.Find(id);
        }
        public T GetByFilter(Expression<Func<T, bool>> filter)
        {

            return _entities.SingleOrDefault(filter);
        }

        public List<T> GetListByFilter(Expression<Func<T, bool>> filter)
        {
            return _entities.Where(filter).ToList();
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Add(entity);
            SaveChanges();
        }

        public T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
             _entities.Update(entity);
            SaveChanges();
            return entity;
            
        }

        private void SaveChanges()
        {
            _context.SaveChanges();
        }

        public int GetCount(Expression<Func<T, bool>> filter)
        {
            return _entities.Count(filter);
        }
        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Remove(entity);
            SaveChanges();
        }

        public int Activate(int Id)
        {
            throw new NotImplementedException();
        }

        public int Deactivate(int Id)
        {
            throw new NotImplementedException();
        }
    }
}