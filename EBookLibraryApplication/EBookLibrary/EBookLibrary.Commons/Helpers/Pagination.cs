using EBookLibrary.DTOs;

using System;
using System.Linq;

namespace EBookLibrary.Commons.Helpers
{
    public static class Pagination
    {
        public static PagedResult<T> Paginate<T>(this IQueryable<T> query, int pageNumber, int pageSize) where T : class
        {
            //Create and Initialize new Paged result
            var pagedResult = new PagedResult<T>();
            pagedResult.CurrentPage = pageNumber;
            pagedResult.PageSize = pageSize;
            pagedResult.TotalRecords = query.Count();

            var pageCount = (double)pagedResult.TotalRecords / pageSize;
            pagedResult.TotalPages = (int)Math.Ceiling(pageCount);

            //Caltulate number of items to skip
            var pagesToSkip = (pageNumber - 1) * pageSize;

            //Get and return the paged result from the records
            pagedResult.Result = query.Skip(pagesToSkip).Take(pageSize).ToList();
            return pagedResult;
        }
    }
}
