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
            //chèn các giá trị cho bang phụ khi chèn xong thì xoá đi
            // media 
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
  