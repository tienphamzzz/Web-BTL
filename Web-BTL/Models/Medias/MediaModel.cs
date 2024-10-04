using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_BTL.Models.Actors;
using Web_BTL.Models.ListMedia.Watch;
using Web_BTL.Models.User;

namespace Web_BTL.Models.Medias
{
    public class MediaModel
    {
        public MediaModel()
        {
            Actors = new HashSet<ActorModel>();
            Genres = new HashSet<GenreModel>();
            Reviews = new HashSet<ReviewModel>();
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
        public bool? Watched { get; set; }
        public bool? Favorite { get; set; }
        public bool? Basic {  get; set; }
        public bool? Premium {  get; set; }
        public bool? Vip { get; set; }
        public virtual int WatchListId { get; set; }
        public virtual WatchListModel WatchList { get; set; }
        public virtual ICollection<ActorModel> Actors { get; set; } // kết nối đến bảng phụ
        public virtual ICollection<GenreModel> Genres { get; set; }
        public virtual ICollection<ReviewModel> Reviews { get; set; }
    }
}
