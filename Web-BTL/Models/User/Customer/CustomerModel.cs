using Web_BTL.Models.ListMedia.Favorite;
using Web_BTL.Models.ListMedia.History;

namespace Web_BTL.Models.User.Customer
{
    public class CustomerModel : UserModel
    {
        public ServicePackage? _ServicePackage { get; set; }
        public CustomerModel()
        {
            _ServicePackage = ServicePackage.Bacis;
            Reviews = new HashSet<ReviewModel>();
            Favourites = new HashSet<FavoriteListModel>();
        }
        public virtual ICollection<ReviewModel> Reviews { get; set; } // liên kết đến bảng review
        public virtual ICollection<FavoriteListModel> Favourites { get; set; } // liên kết đến bảng FavouriteList
        public virtual int? HistoryListId { get; set; }
        public virtual HistoryListModel? History { get; set; } // liên kết tới vảng HistoryList
    }
}
