using System.Net;
using System.ComponentModel.DataAnnotations;



namespace BlogAPI.Core.Models;

public class Posts
{
    public int Id {get; set;}

    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
    public string? Title {get; set;}


    [Required (ErrorMessage = "Main content for blog post is required")]
    public string? Content {get; set;}

    public string? Author {get; set;}

    public DateTime CreatedDate {get; set;}

    public DateTime? UpdatedDate {get; set;}

    //public ICollection<Comment> {get; set;}

}