namespace Web_BTL.Models.Actors
{
    public class ActorModel
    {
        public ActorModel()
        {
            Media_Actors = new HashSet<Media_ActorModel>();
        }
        public int ActorID { get; set; } // khoá
        public string? ActorName { get; set; } // tên actor
        public DateTime? AcctorDate { get; set; } // ngày sinh
        public ICollection<Media_ActorModel> Media_Actors { get; set; } // kết nối đến bảng phụ
    }
}
