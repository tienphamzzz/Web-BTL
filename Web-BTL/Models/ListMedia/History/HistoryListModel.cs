using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web_BTL.Models.User;
using Web_BTL.Models.Medias;

namespace Web_BTL.Models.ListMedia.History
{
    public class HistoryListModel
    {
        public HistoryListModel() 
        {
            Medias = new HashSet<MediaModel>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HistoryListId {  get; set; } 
        public int? UserId { get; set; }
        public UserModel? User { get; set; }
        public ICollection<MediaModel> Medias { get; set; }
    }
}
