using System.ComponentModel.DataAnnotations;

public class Review
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int MovieId { get; set; }

    [Range(1, 10)]
    public int Rating { get; set; }

    public string Comment { get; set; }

    public virtual User User { get; set; }

    public virtual Movie Movie { get; set; }
}
