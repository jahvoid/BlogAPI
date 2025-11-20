using System.Net;
using System.ComponentModel.DataAnnotations;



namespace BlogAPI.Core.Models;

public class Post
{
    public int Id {get; set;}

    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
    public string Title {get; set;} = string.Empty;


    [Required (ErrorMessage = "Main content for blog post is required")]
    public string Content {get; set;} = string.Empty;

    public string Author {get; set;} = "admin";

    public DateTime CreatedDate {get; set;} = DateTime.UtcNow;

    public DateTime? UpdatedDate {get; set;}

    public ICollection<Comment> Comments {get; set;} = new List<Comment>();

}