using EBookLibrary.DataAccess.Abstractions;
using EBookLibrary.DTOs;

using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EBookLibrary.DataAccess.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<bool> Delete(T entity)
        {
            _dbSet.Remove(entity);
           return await _context.SaveChangesAsync() > 0;
        }

        public async Task<T> Get(string Id)
        {
            T Item = await _dbSet.FindAsync(Id);
            return Item;
        }

        public IQueryable<T> GetAll() => _dbSet.AsQueryable<T>();

        public async Task<bool> Insert(T entity)
        {
            _dbSet.Add(entity);
           return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(T entity)
        {
            _dbSet.Update(entity);
          return  await _context.SaveChangesAsync() > 0;
        }

        public async Task<PagedResult<T>> GetByPage(int pageNumber, int pageSize)
        {
            //Create and Initialize new Paged result
            var pagedResult = new PagedResult<T>();
            pagedResult.CurrentPage = pageNumber;
            pagedResult.PageSize = pageSize;
            pagedResult.TotalRecords = GetAll().Count();

            var pageCount = (double)pagedResult.TotalRecords / pageSize;
            pagedResult.TotalPages = (int)Math.Ceiling(pageCount);

            //Caltulate number of items to skip
            var pagesToSkip = (pageNumber - 1) * pageSize;

            //Get and return the paged result from the records
            pagedResult.Result = await GetAll().Skip(pagesToSkip).Take(pageSize).ToListAsync();
            return pagedResult;
        }

        public async Task<T> Find(Expression<Func<T, bool>> expression) => await _dbSet.FirstOrDefaultAsync(expression);

        public int GetCount()
        {
            return _dbSet.Count();
        }
    }
}
