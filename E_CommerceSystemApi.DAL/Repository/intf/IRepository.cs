using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceSystemApi.DAL.Repository.intf
{
    // IRepository interface defines the basic CRUD operations for a generic type T (Entity) 

    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
        IQueryable<T> GetAllQueryable();

    }
}
