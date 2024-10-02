using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_BTL.Models.Medias
{
    public class GenreModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GenreId { get; set; }
        public string? Type { get; set; }
        public int? MediaId { get; set; }
        public MediaModel? Media { get; set; }
    }
}
