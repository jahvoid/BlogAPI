using BlogAPI.Core.Models;
namespace BlogAPI.Core.Interfaces;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> GetAllAsync();
    Task<Comment?> GetByIdAsync(int id);

    Task<IEnumerable<Comment>> GeCommentsByPostIdAsync(int postId);

    Task<Comment> AddAsync(Comment comment);

    Task<bool> UpdateAsync(Comment comment);

    Task<bool> DeleteAsync(int id);

}