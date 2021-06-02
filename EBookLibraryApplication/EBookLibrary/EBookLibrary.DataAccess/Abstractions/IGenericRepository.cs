using EBookLibrary.DTOs;
using EBookLibrary.DTOs.BookDTOs;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EBookLibrary.DataAccess.Abstractions
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        Task<T> Get(string id);

        Task<bool> Insert(T entity);

        Task<bool> Update(T entity);

        Task<bool> Delete(T entity);

        Task<T> Find(Expression<Func<T, bool>> expression);

        Task<PagedResult<T>> GetByPage(int pageNumber, int pageSize);

        int GetCount();
    }
}
