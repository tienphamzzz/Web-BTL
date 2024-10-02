using Microsoft.EntityFrameworkCore;
using Web_BTL.Models.ListMedia.Favorite;
using Web_BTL.Models.ListMedia.History;
using Web_BTL.Models.ListMedia.Watch;
using Web_BTL.Models.Medias;
using Web_BTL.Models.User.Admin;
using Web_BTL.Models.User.Customer;

namespace Web_BTL.Repository
{
    public class SeedData
    {
        public static void SeedingData(DataContext _context)
        {
            /*
                Migrate dùng để xem các file nào đã áp dụng migrate 
                những migrate nào đã áp dụng thì sẽ bỏ qua
                và những migrate nào chưa được áp dụng thì sẽ tự động dịch và áp dụng
            ==> giúp đồng bộ hoá dữ liệu
             */
            _context.Database.Migrate();
            if (!_context.Admins.Any()) // seed data table Admin
            /*
            UserId, UserName, UserLogin, LoginPassword, UserEmail,
            UserCreateDate, UserImagePath, UserState, UserDuration, _Rule
             */
            {
                _context.Admins.AddRange(
                    new AdminModel 
                    {
                        UserId = 1,
                        UserName = "Mai Xuan Doanh", 
                        UserLogin = "maixuandoanh", 
                        LoginPassword = "12345678@Aa", 
                        UserEmail = "doanhxmai@gmail.com", 
                        UserCreateDate = new DateTime(2024, 8, 12), 
                        UserImagePath = "doanh", 
                        UserState = true, 
                        UserDuration = new TimeSpan(0, 0, 0), 
                        Role = Role.SuperAdmin},
                    new AdminModel
                    {
                        UserId = 2,
                        UserName = "Tien Pham", 
                        UserLogin = "tienpham",
                        LoginPassword = "12345678@Aa",
                        UserEmail = "tienpham2kk4@gmail.com",
                        UserCreateDate = new DateTime(2024, 8, 12),
                        UserImagePath = "tien",
                        UserState = true,
                        UserDuration = new TimeSpan(0, 0, 0),
                        Role = Role.Movie_Management
                    },
                    new AdminModel
                    {
                        UserId = 3,
                        UserName = "Kien Ngoc",
                        UserLogin = "kienngoc",
                        LoginPassword = "12345678@Aa",
                        UserEmail = "kienngoc@gmail.com",
                        UserCreateDate = new DateTime(2024, 8, 12),
                        UserImagePath = "kien",
                        UserState = true,
                        UserDuration = new TimeSpan(0, 0, 0),
                        Role = Role.CommentAndReview_Management
                    },
                    new AdminModel
                    {
                        UserId = 4,
                        UserName = "Dinh Cong Vinh",
                        UserLogin = "dinhcongvinh",
                        LoginPassword = "12345678@Aa",
                        UserEmail = "vinhdinhcong@gmail.com",
                        UserCreateDate = new DateTime(2024, 8, 12),
                        UserImagePath = "vinh",
                        UserState = true,
                        UserDuration = new TimeSpan(0, 0, 0),
                        Role = Role.ServicePackage_Management
                    }
                    );
                _context.SaveChanges();
            }
            if (!_context.Customers.Any())
            /*
            UserId, UserName, UserLogin, LoginPassword, UserEmail,
            UserCreateDate, UserImagePath, UserState, UserDuration, ServicePackage
            */
            {
                _context.Customers.AddRange(
                    new CustomerModel {
                        UserId = 1,
                        UserName = "Mai Xuan Doanh",
                        UserLogin = "maixuandoanhno1",
                        LoginPassword = "12345678@Aa",
                        UserEmail = "viaicamon28@gmail.com",
                        UserCreateDate = new DateTime(2024, 8, 12),
                        UserImagePath = "doanh1",
                        UserState = true,
                        UserDuration = new TimeSpan(0, 0, 0),
                        FavoriteListId = 1,
                        HistoryListId = 1,
                        WatchListId = 1,
                        _ServicePackage = ServicePackage.Bacis
                    },
                    new CustomerModel
                    {
                        UserId = 2,
                        UserName = "Mai Xuan Doanh",
                        UserLogin = "maixuandoanhno2",
                        LoginPassword = "12345678@Aa",
                        UserEmail = "viaicamon2004@gmail.com",
                        UserCreateDate = new DateTime(2024, 8, 12),
                        UserImagePath = "doanh2",
                        UserState = true,
                        UserDuration = new TimeSpan(0, 0, 0),
                        FavoriteListId = 2,
                        HistoryListId = 2,
                        WatchListId = 2,
                        _ServicePackage = ServicePackage.Bacis
                    },
                    new CustomerModel
                    {
                        UserId = 3,
                        UserName = "Mai Xuan Doanh",
                        UserLogin = "maixuandoanhno3",
                        LoginPassword = "12345678@Aa",
                        UserEmail = "oncamviai@gmail.com",
                        UserCreateDate = new DateTime(2024, 8, 12),
                        UserImagePath = "doanh3",
                        UserState = true,
                        UserDuration = new TimeSpan(0, 0, 0),
                        FavoriteListId = 3,
                        HistoryListId = 3,
                        WatchListId = 3,
                        _ServicePackage = ServicePackage.Bacis
                    }
                    );
                _context.SaveChanges();
            }
            if (!_context.Medias.Any())
            /*
            MediaName, MediaGenre, MediaUrl, MediaDescription, MediaQuality, 
            ReleaseDate, MediaAgeRating, MediaImagePath, MediaDuration, MediaState
            */
            {
                _context.Medias.AddRange(
                    new MediaModel 
                    { 
                        MediaId = 1,
                        MediaName = "Supergirl", 
                        MediaUrl = "supergirl",
                        MediaDescription = "The film is very good",
                        MediaQuality = "HD",
                        ReleaseDate = new DateTime(2024, 9, 12),
                        MediaAgeRating = 16,
                        MediaImagePath = "supergirl",
                        MediaDuration = new TimeSpan(2, 0, 0),
                        MediaState = true,
                    },
                    new MediaModel
                    {
                        MediaId = 2,
                        MediaName = "Transformer",
                        MediaUrl = "transformer",
                        MediaDescription = "The film is the best",
                        MediaQuality = "HD",
                        ReleaseDate = new DateTime(2024, 9, 12),
                        MediaAgeRating = 16,
                        MediaImagePath = "transformer",
                        MediaDuration = new TimeSpan(2, 0, 0),
                        MediaState = true,
                    },
                    new MediaModel
                    {
                        MediaId = 3,
                        MediaName = "Demon Slayer",
                        MediaUrl = "demonslayer",
                        MediaDescription = "The cartoon is very good",
                        MediaQuality = "HD",
                        ReleaseDate = new DateTime(2024, 9, 12),
                        MediaAgeRating = 16,
                        MediaImagePath = "demonslayer",
                        MediaDuration = new TimeSpan(2, 0, 0),
                        MediaState = true,
                    }
                    );
                _context.SaveChanges();
            }
            if (!_context.FavoriteLists.Any())
            {
                _context.FavoriteLists.AddRange(
                    
                    new FavoriteListModel { FavoriteListId = 1, UserId = 1},
                    new FavoriteListModel { FavoriteListId = 2, UserId = 2},
                    new FavoriteListModel { FavoriteListId = 3, UserId = 3}
                    );
                _context.SaveChanges();
            }
            if (!_context.HistoryLists.Any())
            {
                _context.HistoryLists.AddRange(
                    new HistoryListModel { HistoryListId = 1, UserId = 1},
                    new HistoryListModel { HistoryListId = 2, UserId = 2},
                    new HistoryListModel { HistoryListId = 3, UserId = 3}
                    );
                _context.SaveChanges();
            }
            if (!_context.WatchLists.Any())
            {
                _context.WatchLists.AddRange(
                    new WatchListModel { WatchListId = 1, UserId = 1 },
                    new WatchListModel { WatchListId = 2, UserId = 2 },
                    new WatchListModel { WatchListId = 3, UserId = 3 }
                    );
                _context.SaveChanges();
            }
            if (!_context.Reviews.Any())
            {

            }
            if (!_context.Actors.Any())
            {

            }
            if (!_context.Genres.Any())
            {
                _context.Genres.AddRange(
                    new GenreModel { GenreId = 1, Type = "Movie"},
                    new GenreModel { GenreId = 2, Type = "Cartoon"},
                    new GenreModel { GenreId = 3, Type = "Series"}
                    );
                _context.SaveChanges();
            }
        }
    }
}
  