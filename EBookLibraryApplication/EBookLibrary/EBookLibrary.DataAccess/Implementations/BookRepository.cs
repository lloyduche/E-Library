using EBookLibrary.DataAccess.Abstractions;
using EBookLibrary.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace EBookLibrary.DataAccess.Implementations
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Book> _dbSet;

        public BookRepository(AppDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Book>();
        }

        public async Task<Book> GetDetailedBook(string Id)
        {
            return await _context.Books
                .Include(book => book.Ratings)
                .Include(book => book.Reviews)
                .Include(book => book.Category)
                .FirstOrDefaultAsync(book => book.Id == Id);
        }

        public async Task<Book> GetBookByAuthor(string authorid)
        {
            return await _context.Books.Where(b => b.Id == authorid).FirstOrDefaultAsync();
        }

        public async Task<Book> GetBookByCategory(string categoryid)
        {
            return await _context.Books.Where(b => b.CategoryId == categoryid).FirstOrDefaultAsync();
        }

        public IQueryable<Book> GetPaginatedBooks()
        {
            return _context.Books
                .Include(book => book.Category).AsQueryable();
        }

        public int GetTotalNumberOfBooks()
        {
            return (from b in _context.Books
                    select b)
                    .Count();
        }

        public int GetTotalNumberOfReviews()
        {
            return (from r in _context.Reviews
                    select r)
                    .Count();
        }
    }
}
