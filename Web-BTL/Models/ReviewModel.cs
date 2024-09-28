using Web_BTL.Models.Medias;
using Web_BTL.Models.User;

namespace Web_BTL.Models
{
    public class ReviewModel
    {
        public int ReviewId { get; set; } // khoá chính
        public string? ReviewContent { get; set; } // nội dung
        public double? ReviewRating { get; set; } // sao đánh giá
        public DateTime? ReviewCreateDate { get; set; }
        public virtual int? UserId { get; set; } // khoá ngoại
        public virtual UserModel? UserModel { get; set; }
        public virtual int? MediaId { get; set; } // khoá ngoại
        public virtual MediaModel? MediaModel { get; set; }
    }
}
