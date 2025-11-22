using BlogAPI.Core.Models;
namespace BlogAPI.Core.Interfaces;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetAllAsync();
    Task<Post?> GetByIdAsync (int id);
    Task<Post> AddAsync(Post post);
    Task<bool> UpdateAsync(Post post);

    Task<bool> DeleteAsync(int id);
}