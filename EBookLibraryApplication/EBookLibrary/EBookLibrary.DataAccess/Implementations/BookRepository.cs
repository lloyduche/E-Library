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

        public IQueryable<Book> GetFilteredBooks(string query)
        {
            var res = _context.Books.Include(b=>b.Category).Where(x => EF.Functions.Like(x.Title, $"%{query}%")
                               || EF.Functions.Like(x.Isbn, $"%{query}%")
                               || EF.Functions.Like(x.Author, $"%{query}%")
                               || EF.Functions.Like(x.Description, $"%{query}%")
                               || EF.Functions.Like(x.Category.Name, $"%{query}%"))
                                .AsQueryable();
            return res;
        }

        public async Task<Book> GetBookByCategory(string categoryid)
        {
            return await _context.Books.Where(b => b.CategoryId == categoryid).FirstOrDefaultAsync();
        }

        public IQueryable<Book> GetPaginatedBooks()
        {
            return _context.Books
                .Include(book => book.Category)
                .Include(book => book.Ratings)
                .AsQueryable();
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
