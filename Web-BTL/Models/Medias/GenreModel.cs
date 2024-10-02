using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_BTL.Models.Medias
{
    public class GenreModel
    {
        public GenreModel() { Medias = new HashSet<MediaModel>(); }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GenreId { get; set; }
        public string? Type { get; set; }
        public ICollection<MediaModel> Medias { get; set; }
    }
}
