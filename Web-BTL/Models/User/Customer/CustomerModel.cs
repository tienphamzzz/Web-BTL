using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web_BTL.Models.ListMedia.Watch;
using Web_BTL.Models.Medias;

namespace Web_BTL.Models.User.Customer
{
    public class CustomerModel : UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        public ServicePackage? _ServicePackage { get; set; }
        public CustomerModel()
        {
            _ServicePackage = ServicePackage.Basic;
            Reviews = new HashSet<ReviewModel>();
        }
        public virtual ICollection<ReviewModel> Reviews { get; set; } // liên kết đến bảng review
        public virtual int? WatchListId { get; set; } // khoá phụ bảng Watch List
        public virtual WatchListModel? WatchList { get; set; }
    }
}
