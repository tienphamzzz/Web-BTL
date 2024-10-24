using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web_BTL.Models.Medias;
using Web_BTL.Models.User;
using Web_BTL.Models.User.Customer;

namespace Web_BTL.Models.ListMedia.Watch
{
    public class WatchListModel
    {
        public WatchListModel()
        {
            Medias = new HashSet<ListMediaModel>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WatchListId { get; set; } // khoá chính
        public virtual int? CustomerId { get; set; } // khoá ngoại
        public virtual CustomerModel? User { get; set; }
        public virtual ICollection<ListMediaModel> Medias { get; set; } // kết nối tới bảng phụ
    }
}
