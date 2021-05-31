using EBookLibrary.DataAccess.Abstractions;
using EBookLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibrary.DataAccess.Implementations
{
    public class BookRepository : GenericRepository<Book> , IBookRepository
    {

        private readonly AppDbContext _context;
        private readonly DbSet<Book> _dbSet;

        public BookRepository(AppDbContext context): base(context)
        {
            _context = context;
            _dbSet = _context.Set<Book>();
        }

        public async Task<Book> GetDetailedBook(string Id)
        {
            return await _context.Books
                .Include(book => book.Ratings)
                .Include(book => book.Reviews)
                .Include(book=> book.Category)
                .FirstOrDefaultAsync(book => book.Id == Id);
        }
    }
}
