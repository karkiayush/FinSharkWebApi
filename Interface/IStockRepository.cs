using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace api.Interface
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> FindStockAsync(int id);
        ValueTask<EntityEntry<Stock>> AddModelAsync(Stock model);
        Task<int> SaveChangesAsync();
        EntityEntry<Stock> DeleteStock(Stock stockModel);
    }
}