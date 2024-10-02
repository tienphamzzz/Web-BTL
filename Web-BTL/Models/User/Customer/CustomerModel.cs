using Web_BTL.Models.ListMedia.Favorite;
using Web_BTL.Models.ListMedia.History;
using Web_BTL.Models.ListMedia.Watch;
using Web_BTL.Models.Medias;

namespace Web_BTL.Models.User.Customer
{
    public class CustomerModel : UserModel
    {
        public ServicePackage? _ServicePackage { get; set; }
        public CustomerModel()
        {
            _ServicePackage = ServicePackage.Bacis;
            Reviews = new HashSet<ReviewModel>();
        }
        public virtual ICollection<ReviewModel> Reviews { get; set; } // liên kết đến bảng review
        public virtual int? HistoryListId { get; set; } // khoá phụ bảng History List
        public virtual HistoryListModel? HistoryList { get; set; }
        public virtual int? FavoriteListId { get; set; } // khoá phụ bảng Favorite List
        public virtual FavoriteListModel? FavoriteList { get; set; }
        public int? WatchListId { get; set; } // khoá phụ bảng Watch List
        public virtual WatchListModel? WatchList { get; set; }
    }
}
