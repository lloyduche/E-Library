﻿using EBookLibrary.DTOs;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace EBookLibrary.DataAccess.Abstractions
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        Task<T> Get(string id);

        Task Insert(T entity);

        Task Update(T entity);

        Task Delete(T entity);

        Task<PagedResult<T>> GetByPage(int pageNumber, int pageSize);
    }
}