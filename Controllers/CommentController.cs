using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Interface;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]

    public class CommentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ICommentRepository _commentRepository;

        public CommentController(ApplicationDBContext context, ICommentRepository commentRepository)
        {
            _context = context;
            _commentRepository = commentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var commentList = await _commentRepository.GetAllCommentAsync();
            var commentDto = commentList.Select(s => s.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment != null) { return Ok(comment.ToCommentDto()); }
            return NotFound();
        }

       /*  [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequestDto commentDto)
        {
            var commentModel = commentDto.ToCommentFromCreateDTO();
            await _commentRepository.CreateCommentAsync(commentModel);
            return CreatedAtAction(
                nameof(GetCommentById),
                new { id = commentModel.Id },
                commentModel.ToCommentDto()
            );
        } */
    }
}