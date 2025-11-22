using BlogAPI.Core.Interfaces;
using BlogAPI.Core.Models;
using BlogAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Infrastructure.Repository;

public class PostRepository : IPostRepository
{
    private readonly BlogDbContext _context;

    public PostRepository(BlogDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        return await _context.Posts
            .Include(p => p.Comments)
            .ToListAsync();

    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        return await _context.Posts 
            .Include(p => p.Comments)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Post> AddAsync(Post post)
    {
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task<bool> UpdateAsync(Post post)
    {
        _context.Posts.Update(post);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if(post == null) return false;

        _context.Posts.Remove(post);
        return await _context.SaveChangesAsync() > 0;   
    }
}