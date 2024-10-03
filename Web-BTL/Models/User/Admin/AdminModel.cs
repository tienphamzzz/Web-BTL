using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Web_BTL.Models.User.Admin
{
    public class AdminModel : UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminId { get; set; }
        public Role Role { get; set; }
    }
}
