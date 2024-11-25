using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Repository;

namespace api.Interface
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllCommentAsync();
    }
}