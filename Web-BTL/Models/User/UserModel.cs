using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web_BTL.Models.User
{
    public abstract class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; } 
        public string? UserName { get; set; } 
        public string? UserLogin { get; set; } // tên tài khoản dùng để login
        public string? LoginPassword { get; set; } // mật khẩu của Login
        public string? UserEmail { get; set; } // địa chỉ email
        public DateTime? UserCreateDate { get; set; } // ngày tạo
        public string? UserImagePath { get; set; } // đường dẫn đến ảnh của user
        public bool? UserState { get; set; } // trạng thái tài khoản idle: chờ
        public TimeSpan? UserDuration { get; set; } // thời gian hoạt động của User
    }
}
