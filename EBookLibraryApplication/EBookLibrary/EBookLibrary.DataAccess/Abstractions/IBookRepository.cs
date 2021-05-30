using EBookLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibrary.DataAccess.Abstractions
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<Book> GetDetailedBook(string Id);
    }
}
