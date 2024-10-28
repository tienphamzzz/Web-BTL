using Microsoft.EntityFrameworkCore;
using Web_BTL.Models;
using Web_BTL.Models.ListMedia.Watch;
using Web_BTL.Models.Medias;
using Web_BTL.Models.User.Admin;
using Web_BTL.Models.User.Customer;
using Web_BTL.Models.Actors;

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
                        UserImagePath = "default.jpg",
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
                        UserImagePath = "default.jpg",
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
                        UserImagePath = "default.jpg",
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
                        UserImagePath = "default.jpg",
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
                        UserImagePath = "default.jpg",
                        UserState = true,
                        UserDuration = new TimeSpan(0, 0, 0),
                        WatchListId = 1,
                        _ServicePackage = ServicePackage.Basic
                    },
                    new CustomerModel
                    {
                        UserName = "Mai Xuan Doanh",
                        UserLogin = "maixuandoanhno2",
                        LoginPassword = "12345678@Aa",
                        UserEmail = "viaicamon2004@gmail.com",
                        UserCreateDate = new DateTime(2024, 8, 12),
                        UserImagePath = "default.jpg",
                        UserState = true,
                        UserDuration = new TimeSpan(0, 0, 0),
                        WatchListId = 2,
                        _ServicePackage = ServicePackage.Basic
                    },
                    new CustomerModel
                    {
                        UserName = "Mai Xuan Doanh",
                        UserLogin = "maixuandoanhno3",
                        LoginPassword = "12345678@Aa",
                        UserEmail = "oncamviai@gmail.com",
                        UserCreateDate = new DateTime(2024, 8, 12),
                        UserImagePath = "default.jpg",
                        UserState = true,
                        UserDuration = new TimeSpan(0, 0, 0),
                        WatchListId = 3,
                        _ServicePackage = ServicePackage.Basic
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
                        package = ServicePackage.Premium
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
                        package = ServicePackage.Vip
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
            if (!_context.Actors.Any())
            {
                _context.Actors.AddRange(
                    new ActorModel { ActorName = "Megan Fox", AcctorDate = new DateTime(1986, 5, 16) }, //16 tháng 5, 1986 (38 tuổi)
                    new ActorModel { ActorName = "Shia LaBeouf", AcctorDate = new DateTime(1986, 6, 11) }, // 11 tháng 6, 1986 (38 tuổi)
                    new ActorModel { ActorName = "Rachael Taylor", AcctorDate = new DateTime(1984, 7, 11) } // 11 tháng 7, 1984 (40 tuổi)
                    );
                _context.SaveChanges();
            }
            //chèn các giá trị cho bang phụ khi chèn xong thì xoá đi
            // media
            var media1 = _context.Medias.FirstOrDefault(m => m.MediaId == 1);
            var media2 = _context.Medias.FirstOrDefault(m => m.MediaId == 2);
            var media3 = _context.Medias.FirstOrDefault(m => m.MediaId == 3);
            // genre
            //var genre1 = _context.Genres.FirstOrDefault(g => g.GenreId == 1);
            //var genre2 = _context.Genres.FirstOrDefault(g => g.GenreId == 2);
            //var genre3 = _context.Genres.FirstOrDefault(g => g.GenreId == 3);
            //// actor
            //var actor1 = _context.Actors.FirstOrDefault(a => a.ActorID == 1);
            //var actor2 = _context.Actors.FirstOrDefault(b => b.ActorID == 2);
            //var actor3 = _context.Actors.FirstOrDefault(c => c.ActorID == 3);
            //if (media1 != null && media2 != null && media3 != null)
            //{
            //    if (genre1 != null && genre2 != null && genre3 != null)
            //    {
            //        media1.Genres.Add(genre1);
            //        media1.Genres.Add(genre3);
            //        media2.Genres.Add(genre1);
            //        media2.Genres.Add(genre3);
            //        media3.Genres.Add(genre2);
            //        media3.Genres.Add(genre3);
            //    }
            //    if (actor1 != null && actor2 != null && actor3 != null)
            //    {
            //        _context.Actor_Medias.AddRange(
            //            new Actor_MediaModel { MediaId = media2.MediaId, Media = media2, Actor = actor1, ActorId = actor1.ActorID },
            //            new Actor_MediaModel { MediaId = media2.MediaId, Media = media2, Actor = actor2, ActorId = actor2.ActorID },
            //            new Actor_MediaModel { MediaId = media2.MediaId, Media = media2, Actor = actor3, ActorId = actor3.ActorID }
            //            );
            //    }
            //    _context.SaveChanges();
            //}
            if (!_context.ListMedia.Any())
            {
                var watchList1 = _context.WatchLists.FirstOrDefault(w => w.CustomerId == 1);
                var watchList2 = _context.WatchLists.FirstOrDefault(w => w.CustomerId == 2);
                var watchList3 = _context.WatchLists.FirstOrDefault(w => w.CustomerId == 3);
                _context.ListMedia.AddRange(
                    new ListMediaModel 
                    { 
                        WatchListId = watchList1.WatchListId,
                        MediaId = media1.MediaId,
                        IsWatched = false,
                        Favorite = false,
                        AddDate = DateTime.Now
                    },
                    new ListMediaModel
                    {
                        WatchListId = watchList2.WatchListId,
                        MediaId = media2.MediaId,
                        IsWatched = false,
                        Favorite = false,
                        AddDate = DateTime.Now
                    },
                    new ListMediaModel
                    {
                        WatchListId = watchList3.WatchListId,
                        MediaId = media3.MediaId,
                        IsWatched = false,
                        Favorite = false,
                        AddDate = DateTime.Now
                    },
                    new ListMediaModel
                    {
                        WatchListId = watchList2.WatchListId,
                        MediaId = media3.MediaId,
                        IsWatched = false,
                        Favorite = false,
                        AddDate = DateTime.Now
                    },
                    new ListMediaModel
                    {
                        WatchListId = watchList2.WatchListId,
                        MediaId = media1.MediaId,
                        IsWatched = false,
                        Favorite = false,
                        AddDate = DateTime.Now
                    }
                    );
                _context.SaveChanges();
            }
        }
    }
}
  