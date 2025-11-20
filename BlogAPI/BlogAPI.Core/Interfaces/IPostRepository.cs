using BlogAPI.Core.Models;
namespace BlogAPI.Core.Interfaces;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetAllAsync();
    Task<Post?> GetByIdAsync (int id);
    Task<Post> AddAsync(Post post);
    Task<Post> UpdateAsync(Post post);

    Task<bool> DeleteAsync(int id);
}