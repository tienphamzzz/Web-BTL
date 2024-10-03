using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web_BTL.Models.Medias;
using Web_BTL.Models.User;
using Web_BTL.Models.User.Customer;

namespace Web_BTL.Models
{
    public class ReviewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; } // khoá chính
        public string? ReviewContent { get; set; } // nội dung
        public double? ReviewRating { get; set; } // sao đánh giá
        public DateTime? ReviewCreateDate { get; set; }
        public virtual int? CustomerId { get; set; } // khoá ngoại
        public virtual CustomerModel? UserModel { get; set; }
        public virtual int MediaId { get; set; }
        public virtual MediaModel? Medias { get; set; }
    }
}
