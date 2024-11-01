using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_BTL.Models.Actors;
using Web_BTL.Models.ListMedia.Watch;
using Web_BTL.Models.User;
using Web_BTL.Models.User.Customer;

namespace Web_BTL.Models.Medias
{
    public class MediaModel
    {
        public MediaModel()
        {
            Actors = new HashSet<Actor_MediaModel>();
            Genres = new HashSet<GenreModel>();
            Reviews = new HashSet<ReviewModel>();
            WatchLists = new HashSet<ListMediaModel>();
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
        //public int? package { get; set; } = 0; // 0 là bagic, 1 là medium, 2 là vip
        public ServicePackage package { get; set; } = ServicePackage.Basic;
        public virtual ICollection<Actor_MediaModel> Actors { get; set; } // kết nối đến bảng phụ
        public virtual ICollection<GenreModel> Genres { get; set; }
        public virtual ICollection<ReviewModel> Reviews { get; set; }
        public virtual ICollection<ListMediaModel> WatchLists { get; set; }
    }
}