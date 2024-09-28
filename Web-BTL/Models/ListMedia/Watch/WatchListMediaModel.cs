using Web_BTL.Models.Medias;

namespace Web_BTL.Models.ListMedia.Watch
{
    public class WatchListMediaModel
    {
        public virtual int WatchListId { get; set; }
        public virtual WatchListModel? WatchList { get; set; }

        public virtual int MediaId { get; set; }
        public virtual MediaModel? Media { get; set; }
    }
}
