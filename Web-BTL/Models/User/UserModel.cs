namespace Web_BTL.Models.User
{
    public abstract class UserModel
    {
        public int UserId { get; set; } // lấy số điện thoại làm ID
        public string? UserName { get; set; } // có thể lấy số điện thoại làm mặc định
        public string? UserLogin { get; set; } // tên tài khoản dùng để login
        public string? LoginPassword { get; set; } // mật khẩu của Login
        public string? UserEmail { get; set; } // địa chỉ email
        public DateTime? UserCreateDate { get; set; } // ngày tạo
        public string? UserImagePath { get; set; } // đường dẫn đến ảnh của user
        public bool? UserState { get; set; } // trạng thái tài khoản idle: chờ
        public TimeSpan? UserDuration { get; set; } // thời gian hoạt động của User
    }
}
