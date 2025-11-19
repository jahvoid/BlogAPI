namespace BlogAPI.Core.Models;
using System.ComponentModel.DataAnnotations;
public class Comment{
    public int Id {get; set;}

    [Required]
    public int PostId {get; set;}

    [Required]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string? Name {get; set;}

    [Required]
    [StringLength(150, ErrorMessage = " Email cannot exceed 150 characters")]
    public string? Email {get; set;}

    [Required]
    [StringLength(1000, ErrorMessage = "Content cannot exceed 1000 characters")]
    public string? Content {get; set;}

    public DateTime CreatedDate {get; set;}

    
}