using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interface;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        /*
        Till now, we've only implemented the repository pattern.
        Now here, we need to have the ApplicationDbContext instance, so we will get the context using DI 
        */
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }


        public Task<List<Stock>> GetAllAsync()
        {
            // why do we need to invoke ToList() method

            // This is something called "Deferred Execution". If we don't invoked ToList() method, it'll return us a list like obj but don't make sql query on the fly, so for making the sql query and executing it, we're using .toList() method
            return _context.Stocks.ToListAsync();
        }

        public Task<Stock?> FindStockAsync(int id)
        {
            /* FindAsync returns a ValueTask<Stock?>, while your method signature expects a Task<Stock>. A ValueTask is different from a Task, and you can't directly assign or return one where the other is expected. */
            return _context.Stocks.FindAsync(id).AsTask();
        }

        public ValueTask<EntityEntry<Stock>> AddModelAsync(Stock model)
        {
            return _context.Stocks.AddAsync(model);
        }
        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public EntityEntry<Stock> DeleteStock(Stock stockModel)
        {
            return _context.Stocks.Remove(stockModel);
        }
    }
}