using Web_BTL.Models.Medias;
using Web_BTL.Models.User;

namespace Web_BTL.Models.ListMedia.History
{
    public class HistoryListModel
    {
        public HistoryListModel()
        {
            Medias = new HashSet<MediaModel>();
        }
        public int HistoryListId { get; set; }
        public virtual int? UserId { get; set; }
        public virtual UserModel? User { get; set; }
        public DateTime? WatchDate { get; set; }
        public virtual ICollection<MediaModel> Medias { get; set; }

    }
}
