using Microsoft.EntityFrameworkCore;
using Web_BTL.Models;
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
            UserCreateDate, UserImagePath, UserState, UserDuration, _Role
             */
            {
                _context.Admins.AddRange(
                    new AdminModel 
                    {
                        UserName = "Mai Xuan Doanh", 
                        UserLogin = "maixuandoanh", 
                        LoginPassword = "12345678@Aa", 
                        UserEmail = "doanhxmai@gmail.com", 
                        UserCreateDate = new DateTime(2024, 8, 12), 
                        UserImagePath = "doanh", 
                        UserState = true, 
                        UserDuration = new TimeSpan(0, 0, 0),   
                        Role = Role.SuperAdmin
                    },
                    new AdminModel
                    {
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
                        UserName = "Dinh Cong Vinh",
                        UserLogin = "dinhcongvinh",
                        LoginPassword = "12345678@Aa",
                        UserEmail = "dinhcongvinh09092004@gmail.com",
                        UserCreateDate = new DateTime(2024, 8, 12),
                        UserImagePath = "vinh",
                        UserState = true,
                        UserDuration = new TimeSpan(0, 0, 0),
                        Role = Role.ServicePackage_Management
                    }
                    );
                _context.SaveChanges();
            }
            if (!_context.WatchLists.Any())
            {
                _context.WatchLists.AddRange(
                    new WatchListModel { CustomerId = 1 },
                    new WatchListModel { CustomerId = 2 },
                    new WatchListModel { CustomerId = 3 }
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
                        UserName = "Mai Xuan Doanh",
                        UserLogin = "maixuandoanhno1",
                        LoginPassword = "12345678@Aa",
                        UserEmail = "viaicamon28@gmail.com",
                        UserCreateDate = new DateTime(2024, 8, 12),
                        UserImagePath = "doanh1",
                        UserState = true,
                        UserDuration = new TimeSpan(0, 0, 0),
                        WatchListId = 1,
                        _ServicePackage = ServicePackage.Bacis
                    },
                    new CustomerModel
                    {
                        UserName = "Mai Xuan Doanh",
                        UserLogin = "maixuandoanhno2",
                        LoginPassword = "12345678@Aa",
                        UserEmail = "viaicamon2004@gmail.com",
                        UserCreateDate = new DateTime(2024, 8, 12),
                        UserImagePath = "doanh2",
                        UserState = true,
                        UserDuration = new TimeSpan(0, 0, 0),
                        WatchListId = 2,
                        _ServicePackage = ServicePackage.Bacis
                    },
                    new CustomerModel
                    {
                        UserName = "Mai Xuan Doanh",
                        UserLogin = "maixuandoanhno3",
                        LoginPassword = "12345678@Aa",
                        UserEmail = "oncamviai@gmail.com",
                        UserCreateDate = new DateTime(2024, 8, 12),
                        UserImagePath = "doanh3",
                        UserState = true,
                        UserDuration = new TimeSpan(0, 0, 0),
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
                        MediaName = "Supergirl",
                        MediaUrl = "supergirl",
                        MediaDescription = "The film is very good",
                        MediaQuality = "HD",
                        ReleaseDate = new DateTime(2024, 9, 12),
                        MediaAgeRating = 16,
                        MediaImagePath = "supergirl",
                        MediaDuration = new TimeSpan(2, 0, 0),
                        Favortive = false,
                        Watched = false,
                        Basic = true,
                        Premium = true,
                        Vip = true,
                        MediaState = true
                    },
                    new MediaModel
                    {
                        MediaName = "Transformer",
                        MediaUrl = "transformer",
                        MediaDescription = "The film is the best",
                        MediaQuality = "HD",
                        ReleaseDate = new DateTime(2024, 9, 12),
                        MediaAgeRating = 16,
                        MediaImagePath = "transformer",
                        MediaDuration = new TimeSpan(2, 0, 0),
                        Favortive = false,
                        Watched = false,
                        Basic = true,
                        Premium = true,
                        Vip = false,
                        MediaState = true
                    },
                    new MediaModel
                    {
                        MediaName = "Demon Slayer",
                        MediaUrl = "demonslayer",
                        MediaDescription = "The cartoon is very good",
                        MediaQuality = "HD",
                        ReleaseDate = new DateTime(2024, 9, 12),
                        MediaAgeRating = 16,
                        MediaImagePath = "demonslayer",
                        MediaDuration = new TimeSpan(2, 0, 0),
                        Favortive = false,
                        Watched = false,
                        Basic = true,
                        Premium = false,
                        Vip = false,
                        MediaState = true
                    }
                    );
                _context.SaveChanges();
            }
            if (!_context.Reviews.Any())
            {
                _context.Reviews.AddRange(
                    new ReviewModel { ReviewContent = "Very very good", ReviewRating = 5.0, ReviewCreateDate = new DateTime(2024, 9, 12), CustomerId = 1, MediaId = 1 },
                    new ReviewModel { ReviewContent = "Very good", ReviewRating = 5.0, ReviewCreateDate = new DateTime(2024, 9, 12), CustomerId = 1, MediaId = 2 },
                    new ReviewModel { ReviewContent = "Very very very good", ReviewRating = 5.0, ReviewCreateDate = new DateTime(2024, 9, 12), CustomerId = 1, MediaId = 3 }
                    );
                _context.SaveChanges();
            }
            if (!_context.Genres.Any())
            {
                _context.Genres.AddRange(
                    new GenreModel { Type = "Movie"},
                    new GenreModel { Type = "Cartoon"},
                    new GenreModel { Type = "Series"}
                    );
                _context.SaveChanges();
            }
            
            // media 
            var media1 = _context.Medias.FirstOrDefault(m => m.MediaId == 1);
            var media2 = _context.Medias.FirstOrDefault(m => m.MediaId == 2);
            var media3 = _context.Medias.FirstOrDefault(m => m.MediaId == 3);
            // watch list
            var wlist1 = _context.WatchLists.FirstOrDefault(w => w.WatchListId == 1);
            var wlist2 = _context.WatchLists.FirstOrDefault(w => w.WatchListId == 2);
            var wlist3 = _context.WatchLists.FirstOrDefault(w => w.WatchListId == 3);
            
            if (media1 != null && media2 != null && media3 != null)
            {
                if (wlist1 != null && wlist2 != null && wlist3 != null)
                {
                    media1.WatchLists.Add(wlist1);
                    media1.WatchLists.Add(wlist2);
                    media2.WatchLists.Add(wlist2);
                    media2.WatchLists.Add(wlist3);
                    media3.WatchLists.Add(wlist3);
                }
                _context.SaveChanges();
            }
        }
    }
}
  