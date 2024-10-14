using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web_BTL.Models.Medias;

namespace Web_BTL.Models.Actors
{
    public class ActorModel
    {
        public ActorModel()
        {
            Medias = new HashSet<MediaModel>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActorID { get; set; } // khoá
        public string? ActorName { get; set; } // tên actor
        public DateTime? AcctorDate { get; set; } // ngày sinh
        public virtual ICollection<MediaModel> Medias { get; set; } // kết nối đến bảng phụ
    }
}
