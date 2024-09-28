namespace Web_BTL.Models.User.Admin
{
    public enum Role
    {
        SuperAdmin, // admin với cấp cao nhất (với tất cả các quyền)
        User_management, // admin với cấp quản lý người dùng(ban hoặc cấp tài khoản cho người đăng ký)
        Movie_Management, // admin với cấp quản lý phim(push-pop phim lên web)
        CommentAndReview_Management, // quản lý đánh giá và review
        ServicePackage_Management // quản lý các gói dịch vụ cho user
    }
}
