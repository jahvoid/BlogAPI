using BlogAPI.Core.Interfaces;
using BlogAPI.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

namespace BlogAPI.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;

    public CommentsController(ICommentRepository commentRepository, IPostRepository postRepository)
    {
        _commentRepository = commentRepository;
        _postRepository = postRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllComments()
    {
        var comments = await _commentRepository.GetAllAsync();
        return Ok(comments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCommentById(int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        if(comment == null) return NotFound();

        return Ok(comment);
    }

    [HttpGet("/api/posts/{postId}/comments")]
    public async Task<IActionResult> GetCommentsByPost(int postId)
    {
        var post = await _postRepository.GetByIdAsync(postId);
        if(post == null) return NotFound("Post does not exist");
        
        var comments = await _commentRepository.GetCommentsByPostIdAsync(postId);
        return Ok(comments);
    }

    [HttpPost("/api/posts/{postId}/comments")]
    public async Task<IActionResult> AddComment(int postId, [FromBody] Comment comment)
    {
        var post = await _postRepository.GetByIdAsync(postId);
        if(post == null) return NotFound("Post does not exist");

        comment.PostId = postId;
        comment.CreatedDate = DateTime.UtcNow;

        var newComment = await _commentRepository.AddAsync(comment);

        return CreatedAtAction(nameof(GetCommentById), new{id = newComment.Id}, newComment);
    }

    [HttpPut("{id")]
    public async Task<IActionResult> UpdateComment(int id, [FromBody] Comment updated)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        if(comment == null) return NotFound();

        comment.Name = updated.Name;
        comment.Email = updated.Email;
        comment.Content = updated.Content;

        await _commentRepository.UpdateAsync(comment);
        return Ok(comment);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchComment(int id, [FromBody] JsonPatchDocument<Comment> patchDoc)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        if(comment == null) return NotFound();

        patchDoc.ApplyTo(comment);

        await _commentRepository.UpdateAsync(comment);
        return Ok(comment);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var success = await _commentRepository.DeleteAsync(id);
        if(!success) return NotFound();

        return NoContent();
    }
}

