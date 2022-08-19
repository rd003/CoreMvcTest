using CoreMvcTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcTest.Repositories.Abstract
{
    public interface IBookCategoryRepository
    {
        Task<IEnumerable<BookCategory>> GetAll();
        Task<BookCategory> FindById(int id);
        Task<bool> Delete(int id);
        Task<bool> AddUpdate(BookCategory bookCategory);
    }
}
