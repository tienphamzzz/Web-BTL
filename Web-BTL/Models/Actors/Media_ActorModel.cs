using Web_BTL.Models.Medias;

namespace Web_BTL.Models.Actors
{
    public class Media_ActorModel
    {
        public int ActorId { get; set; } // Id của actor
        public ActorModel? Actor { get; set; } // đối tượng của actor
        public int MediaId { get; set; } // Id của media
        public MediaModel? Media { get; set; } // đối tượng của media
        public bool? Character { get; set; } // vai diễn, true: diễn chính, false: diễn phụ
    }
}
