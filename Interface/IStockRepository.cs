using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace api.Interface
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?>  GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock model);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateStockRqstDto);
        Task<Stock?> DeleteStock(int id);
    }
}