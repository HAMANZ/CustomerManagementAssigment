using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.RespositoryPattern
{
    public interface IRepository<T>
    {

        IEnumerable<T> GetAll();
        List<T> GetAllList();
        List<T> GetAllList(int limit);
        T GetById(int id);
        T GetByFilter(Expression<Func<T, bool>> filter);
        List<T> GetListByFilter(Expression<Func<T, bool>> filter);
        int GetCount(Expression<Func<T, bool>> filter);
        int Activate(int Id);
        int Deactivate(int Id);
        void Insert(T entity);
        void Remove(T entity);
        T Update(T entity);
    }
}
