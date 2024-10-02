using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_BTL.Models.Actors;
using Web_BTL.Models.ListMedia.Favorite;
using Web_BTL.Models.ListMedia.Watch;
using Web_BTL.Models.User;

namespace Web_BTL.Models.Medias
{
    public class MediaModel
    {
        public MediaModel()
        {
            WatchLists = new HashSet<WatchListModel>();
            Reviews = new HashSet<ReviewModel>();
            Actors = new HashSet<ActorModel>();
            Genres = new HashSet<GenreModel>();
            FavoriteLists = new HashSet<FavoriteListModel>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MediaId { get; set; }
        public string? MediaUrl { get; set; } // đường dẫn đến phim
        public string? MediaName { get; set; } // tên phim
        public string? MediaDescription { get; set; } // mô tả phim
        public string? MediaQuality { get; set; } // chất lượng(HD, 2K, 4K, ...)
        public DateTime? ReleaseDate { get; set; } // ngày phát hành
        public int? MediaAgeRating { get; set; } // giới hạn số tuổi được xem
        public string? MediaImagePath { get; set; } // ảnh sơ bộ của phim
        public TimeSpan? MediaDuration { get; set; } // thời lượng
        public bool? MediaState { get; set; } // tình trạng đã xem hay chưa, true là đã xem, false là chưa xem
        public virtual ICollection<WatchListModel> WatchLists { get; set; } // kết nối đến bảng phụ
        public virtual ICollection<ReviewModel> Reviews { get; set; } // kết nối tới bảng Reviews
        public virtual ICollection<ActorModel> Actors { get; set; } // kết nối đến bảng phụ
        public virtual ICollection<FavoriteListModel> FavoriteLists { get; set; }
        public virtual ICollection<GenreModel> Genres { get; set; }
        
    }
}
