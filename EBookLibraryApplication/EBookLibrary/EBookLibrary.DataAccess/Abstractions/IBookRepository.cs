using EBookLibrary.Models;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace EBookLibrary.DataAccess.Abstractions
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<Book> GetDetailedBook(string Id);

        Task<Book> GetBookByCategory(string categoryid);

        IQueryable<Book> GetPaginatedBooks();

        IQueryable<Book> GetFilteredBooks(string query);

        int GetTotalNumberOfBooks();

        int GetTotalNumberOfReviews();
    }
}
