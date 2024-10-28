using Web_BTL.Models.Medias;

namespace Web_BTL.Models.ListMedia.Watch
{
    public class ListMediaModel
    {
        // nối đến bảng watchList
        public int? WatchListId {  get; set; }
        public WatchListModel? watchList { get; set; }
        // nối đến bảng media
        public int? MediaId { get; set; }
        public MediaModel? media { get; set; }
        // các thông tin chi tiết
        public bool? IsWatched { get; set; } = false; // đã xem rồi hay chưa
        public bool? Favorite { get; set; } = false; // có được cho vào list yêu thích không
        public DateTime? AddDate { get; set; } // ngày thêm (nó sẽ bằng DateTime.Now())
        public TimeSpan? Watching { get; set; } // đã xem đến giờ thứ bao nhiêu
    }
}