using EBookLibrary.DTOs.BookDTOs;
using EBookLibrary.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookLibrary.DataAccess.Abstractions
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<Book> GetDetailedBook(string Id);

        Task<Book> GetBookByCategory(string categoryid);

        IQueryable<Book> GetPaginatedBooks();

       Task<IReadOnlyList<Book>> GetAllBooksWhere(SearchTermDto search);
    }
}
