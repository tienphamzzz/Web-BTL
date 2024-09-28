using Web_BTL.Models.Medias;

namespace Web_BTL.Models.ListMedia.Favorite
{
    public class FavoriteListMediaModel
    {
        public virtual int FavouriteListId { get; set; }
        public virtual FavoriteListModel? FavoriteList { get; set; }
        public virtual int MediaId { get; set; }
        public virtual MediaModel? Media { get; set; }
    }
}
