﻿public class WatchList
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int MovieId { get; set; }

    public virtual User User { get; set; }

    public virtual Movie Movie { get; set; }
}
