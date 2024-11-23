using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            // why do we need to invoke ToList() method
            // This is something called "Deferred Execution". If we don't invoked ToList() method, it'll return us a list like obj but don't make sql query on the fly, so for making the sql query and executing it, we're using .toList() method
            var stocks = _context.Stocks.ToList().Select(s => s.ToStockDto());
            // The IActionResult just is the return type of Ok(variable_name); 
            return Ok(stocks);
        }


        // when you're returning an individual result like a single stock in this case, we need to use [FromRoute] and the id as a argument in the function

        //.NET will be using something called "Model Binding" to extract this string out and turn it into int type var and then pass it as the argument in the method. 
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stock = _context.Stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }
    }
}