using BlogAPI.Core.Interfaces;
using BlogAPI.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;


namespace BlogAPI.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IPostRepository _postRepository;

    public PostsController(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPosts()
    {
        var posts = await _postRepository.GetAllAsync();
        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostById(int id)
    {
        var post = await _postRepository.GetByIdAsync(id);
        if (post == null) return NotFound(new {message = $"Post with ID {id} not found"});

        return Ok(post);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] Post post)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        post.Author = "admin";
        post.CreatedDate = DateTime.UtcNow;
        var newPost = await _postRepository.AddAsync(post);

        return CreatedAtAction(nameof(GetPostById), new {id = newPost.Id}, newPost);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(int id, [FromBody] Post post)
    {
        var existing = await _postRepository.GetByIdAsync(id);
        if (existing == null) return NotFound();

        existing.Title = post.Title;
        existing.Content = post.Content;
        existing.UpdatedDate = DateTime.UtcNow;

        var success = await _postRepository.UpdateAsync(existing);
        if (!success) return StatusCode(500);

        return Ok(existing);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchPost(int id, [FromBody] JsonPatchDocument<Post> patchDoc)
    {
        if (patchDoc == null) return BadRequest(new{message= "Patch document is null"});

        var post = await _postRepository.GetByIdAsync(id);
        if(post == null) return NotFound(new{message = $"Post with ID {id} not found"});

        patchDoc.ApplyTo(post);

        post.UpdatedDate = DateTime.UtcNow;

        await _postRepository.UpdateAsync(post);

        return Ok(post);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var success = await _postRepository.DeleteAsync(id);
        if(!success) return NotFound(new { message = $"Post with ID {id} not found"});

        return NoContent();
    }

}