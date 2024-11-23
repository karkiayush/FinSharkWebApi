using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        /* Trying to make one to many relation between stock and comment class.
        We're going to form this relationship using convention although there're many ways to form it.
        
        Convention basically means entity framework .net core. Here .net is going ahead and search through our code and form this relationship for us.
        
        Previously, we used to do manually using something called fluent API, */

        // This is the actual key for the comment which helps us to create the one-many relationship.
        public int StockId { get; set; }
        public Stock? Stock { get; set; }
    }
}