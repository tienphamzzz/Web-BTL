using Web_BTL.Models.Actors;
using Web_BTL.Models.ListMedia.History;
using Web_BTL.Models.ListMedia.Watch;

namespace Web_BTL.Models.Medias
{
    public class MediaModel
    {
        public MediaModel()
        {
            watchListMedias = new HashSet<WatchListMediaModel>();
            Reviews = new HashSet<ReviewModel>();
            Media_Actors = new HashSet<Media_ActorModel>();
        }
        public int MediaId { get; set; }
        public Genre? MediaGenre { get; set; } // kiểu phim
        public string? MediaUrl { get; set; } // đường dẫn đến phim
        public string? MediaName { get; set; } // tên phim
        public string? MediaDescription { get; set; } // mô tả phim
        public string? MediaQuality { get; set; } // chất lượng(HD, 2K, 4K, ...)
        public DateTime? ReleaseDate { get; set; } // ngày phát hành
        public string? MediaAgeRating { get; set; } // giới hạn số tuổi được xem
        public string? MediaImagePath { get; set; } // ảnh sơ bộ của phim
        public int? MediaDuration { get; set; } // thời lượng
        public bool? MediaState { get; set; } // tình trạng đã xem hay chưa, true là đã xem, false là chưa xem
        public virtual ICollection<WatchListMediaModel> watchListMedias { get; set; } // kết nối đến bảng phụ
        public virtual ICollection<ReviewModel> Reviews { get; set; } // kết nối tới bảng Reviews
        public virtual ICollection<Media_ActorModel> Media_Actors { get; set; } // kết nối đến bảng phụ
        public virtual int? HistoryListId { get; set; }
        public virtual HistoryListModel? History { get; set; }
    }
}
