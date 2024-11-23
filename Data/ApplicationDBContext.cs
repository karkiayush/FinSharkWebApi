using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    /* ApplicationDBContext.cs is just a giant class that allows you to search you individual tables.
    
    It is a giant object that's gonna allow us to specify which table that we want. */
    public class ApplicationDBContext : DbContext // it need to inherit the DBContext class from the Microsoft.EntityFrameworkCore package
    {
        // :base() is same as :DbContext(), but since we can't do it in constructor, we have to use :base()
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            /* In C#, the syntax : DbContext() or : base() is known as "constructor chaining" or "constructor delegation". This is a mechanism used in object-oriented programming to call the constructor of a base class from a derived class constructor. */
        }

        /* Now, we need to add these tables, that we're talking about */
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}