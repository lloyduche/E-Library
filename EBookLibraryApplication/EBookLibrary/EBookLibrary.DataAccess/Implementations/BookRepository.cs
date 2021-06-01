using EBookLibrary.DataAccess.Abstractions;
using EBookLibrary.DTOs.BookDTOs;
using EBookLibrary.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
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

        public async Task<IReadOnlyList<Book>> GetAllBooksWhere(SearchTermDto search)
        {
            return await _context.Books.Include(book => book.Category).Where(b =>
            b.Author.Contains(search.Author)
            || b.Category.Name.Contains(search.Category)
            || b.Title.Contains(search.Title)
            || b.Isbn.Contains(search.ISBN)).ToListAsync();
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
    }
}
