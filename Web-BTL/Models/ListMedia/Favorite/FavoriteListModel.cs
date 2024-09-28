using Web_BTL.Models.User;

namespace Web_BTL.Models.ListMedia.Favorite
{
    public class FavoriteListModel
    {
        public FavoriteListModel()
        {
            FavoriteListMediaModels = new HashSet<FavoriteListMediaModel>();
        }

        public int FavoriteListId { get; set; } // khoá chính
        public virtual int? UserId { get; set; } // khoá ngoại
        public virtual UserModel? User { get; set; }
        public virtual ICollection<FavoriteListMediaModel> FavoriteListMediaModels { get; set; }
    }
}
