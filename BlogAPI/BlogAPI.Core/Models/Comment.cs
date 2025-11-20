namespace BlogAPI.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

public class Comment{
    public int Id {get; set;}

    [Required]
    public int PostId {get; set;}

    [Required]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name {get; set;} = string.Empty;

    [Required]
    [StringLength(150, ErrorMessage = " Email cannot exceed 150 characters")]
    public string? Email {get; set;} = string.Empty;

    [Required]
    [StringLength(1000, ErrorMessage = "Content cannot exceed 1000 characters")]
    public string Content {get; set;} = string.Empty;

    public DateTime CreatedDate {get; set;} = DateTime.UtcNow;

    public Type Property {get; set;}

    public Post? Post {get; set;}
    
}