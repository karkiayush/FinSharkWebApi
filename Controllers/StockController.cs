using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using api.Models;
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


        // when you're returning an individual result like a single stock in this case, we need to use [FromRoute] to specify that from where the id comes and the id as a argument in the function

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

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
        {
            // since when doing the post request, we gonna pass the data as the json in the body, but not as url.
            var stockModel = stockDto.ToStockFromCreateDTO();

            // Adding data to the database
            /* Adds the stockModel instance to the in-memory change tracker of the Entity Framework Core (EF Core) context. */
            _context.Stocks.Add(stockModel);
            /* commits all changes tracked by the EF Core context to the database, inserting a new record into the Stocks table. */
            _context.SaveChanges();

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
    }
}

