using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interface;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public Task<Stock?> GetByIdAsync(int id)
        {
            return _context.Stocks.FindAsync(id).AsTask();
        }

        public async Task<Stock> CreateAsync(Stock model)
        {
            await _context.Stocks.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateStockRqstDto)
        {
            var stockModel = await _context.Stocks.FindAsync(id);
            if (stockModel == null)
            {
                return null;
            }
            stockModel.CompanyName = updateStockRqstDto.CompanyName;
            stockModel.Purchase = updateStockRqstDto.Purchase;
            stockModel.Purchase = updateStockRqstDto.Purchase;
            stockModel.LastDiv = updateStockRqstDto.LastDiv;
            stockModel.Industry = updateStockRqstDto.Industry;
            stockModel.MarketCap = updateStockRqstDto.MarketCap;

            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteStock(int id)
        {
            var stockModel = await _context.Stocks.FindAsync(id);
            if (stockModel == null)
            {
                return null;
            }
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }
    }
}