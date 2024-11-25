using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interface;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDBContext context, IStockRepository stockRepo)
        {
            _context = context;
            _stockRepo = stockRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // why do we need to invoke ToList() method
            // This is something called "Deferred Execution". If we don't invoked ToList() method, it'll return us a list like obj but don't make sql query on the fly, so for making the sql query and executing it, we're using .toList() method
            var stocks = await _stockRepo.GetAllAsync();
            var stocksDto = stocks.Select(s => s.ToStockDto());
            // The IActionResult just is the return type of Ok(variable_name); 
            return Ok(stocksDto);
        }


        // when you're returning an individual result like a single stock in this case, we need to use [FromRoute] to specify that from where the id comes and the id as a argument in the function

        //.NET will be using something called "Model Binding" to extract this string out and turn it into int type var and then pass it as the argument in the method. 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);
            if (stock != null)
            {
                return Ok(stock.ToStockDto());
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDTO();
            await _stockRepo.CreateAsync(stockModel);

            /* CreatedAtAction is an ASP.NET Core helper method that simplifies returning a 201 Created response. */
            return CreatedAtAction(
                /* actionName: The name of the controller method that handles GET requests for the created resource (in this case, GetById). */
                nameof(GetById),
                /*  The parameters required by the GetById method (e.g., the ID of the resource). */
                new { id = stockModel.Id },
                /* value: The resource data you want to include in the response body (e.g., the created stockModel). */
                stockModel.ToStockDto()
            );
        }

        [HttpPut("{id}")]
        // [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockDto)
        {
            var stockModel = await _stockRepo.UpdateAsync(id, updateStockDto);
            if (stockModel != null)
            {
                return Ok(stockModel.ToStockDto());
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel = await _stockRepo.DeleteStock(id);
            if (stockModel != null)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}

