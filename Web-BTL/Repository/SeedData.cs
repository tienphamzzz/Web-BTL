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
            //foreach(var movie in _context.Medias) 
            //{
                
            //    if (!_context.Genres.Any(m => m.Type == movie.MediaDescription)) {
            //        var genre = new GenreModel { Type = movie.MediaDescription };
            //        _context.Genres.Add(genre);
            //        _context.SaveChanges();
            //    }
            //    else
            //    {
            //        var genre = _context.Genres.FirstOrDefault(m => m.Type == movie.MediaDescription);
            //        movie.Genres.Add(genre);
            //    }
            //}
            //chèn các giá trị cho bang phụ khi chèn xong thì xoá đi
            // media 
            //var media1 = _context.Medias.FirstOrDefault(m => m.MediaId == 1);
            //var media2 = _context.Medias.FirstOrDefault(m => m.MediaId == 2);
            //var media3 = _context.Medias.FirstOrDefault(m => m.MediaId == 3);
            //// genre
            //var genre1 = _context.Genres.FirstOrDefault(g => g.GenreId == 1);
            //var genre2 = _context.Genres.FirstOrDefault(g => g.GenreId == 2);
            //var genre3 = _context.Genres.FirstOrDefault(g => g.GenreId == 3);
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
            //    _context.SaveChanges();
            //} 
            
        }
    }
}
  