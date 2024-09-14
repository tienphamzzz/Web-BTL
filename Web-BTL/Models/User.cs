using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    public string Role { get; set; }

    public virtual ICollection<Review> Reviews { get; set; }

    public virtual ICollection<WatchList> WatchLists { get; set; }
}
