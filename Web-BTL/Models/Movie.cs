using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

public class Movie
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    public string Description { get; set; }

    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }

    public string Director { get; set; }

    public int GenreId { get; set; }

    public virtual Genre Genre { get; set; }
    public string PosterUrl { get; set; }

    public string VideoUrl { get; set; }

    public virtual ICollection<Review> Reviews { get; set; }
}
