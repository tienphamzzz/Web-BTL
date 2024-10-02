using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web_BTL.Models.Medias;
using Web_BTL.Models.User;

namespace Web_BTL.Models.ListMedia.Favorite
{
    public class FavoriteListModel
    {
        public FavoriteListModel()
        {
            Medias = new HashSet<MediaModel>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FavoriteListId { get; set; } // khoá chính
        public virtual int? UserId { get; set; } // khoá ngoại
        public virtual UserModel? User { get; set; }
        public virtual ICollection<MediaModel> Medias { get; set; }
    }
}
