using Web_BTL.Models.User;

namespace Web_BTL.Models.ListMedia.Watch
{
    public class WatchListModel
    {
        public WatchListModel()
        {
            watchListMedias = new HashSet<WatchListMediaModel>();
        }

        public int WatchListId { get; set; } // khoá chính
        public virtual int? UserId { get; set; } // khoá ngoại
        public virtual UserModel? User { get; set; }
        public virtual ICollection<WatchListMediaModel> watchListMedias { get; set; } // kết nối tới bảng phụ
    }
}
