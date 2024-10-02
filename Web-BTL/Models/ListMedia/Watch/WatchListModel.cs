using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web_BTL.Models.Medias;
using Web_BTL.Models.User;

namespace Web_BTL.Models.ListMedia.Watch
{
    public class WatchListModel
    {
        public WatchListModel()
        {
            Medias = new HashSet<MediaModel>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WatchListId { get; set; } // khoá chính
        public virtual int? UserId { get; set; } // khoá ngoại
        public virtual UserModel? User { get; set; }
        public virtual ICollection<MediaModel> Medias { get; set; } // kết nối tới bảng phụ
    }
}
