using CoreMvcTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcTest.Repositories.Abstract
{
    public interface IBookPurchaseRepository
    {
        Task<IEnumerable<BookPurchase>> GetAll();
        Task<BookPurchase> FindById(int id);
        Task<bool> Delete(int id);
        Task<bool> AddUpdate(BookPurchase bookCategory);
    }
}
