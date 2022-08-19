using CoreMvcTest.Models;
using CoreMvcTest.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcTest.Repositories.Abstract
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAll();
        Task<bool> AddUpdate(Book book);
        Task<bool> Delete(int id);
        Task<Book> GetById(int id);
        Task<Book> GetByName(string id);
    }
}
