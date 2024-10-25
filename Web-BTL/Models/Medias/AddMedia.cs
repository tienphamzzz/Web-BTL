namespace Web_BTL.Models.Medias
{
    public class AddMedia
    {
        public AddMedia() 
        {
            Genres = new List<GenreModel>();
            //SelectedGenreId = new List<int>();
            Media = new MediaModel();
        }
        public MediaModel? Media { get; set; }
        public List<GenreModel> Genres { get; set; }
        //public List<int> SelectedGenreId { get; set; }
        public string? SelectedGenreId { get; set; } = "";
        //public IFormFile Image { get; set; }
        //public IFormFile? Video { get; set; }
    }
}
