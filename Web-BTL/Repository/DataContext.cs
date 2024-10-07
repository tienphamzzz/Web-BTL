using Microsoft.EntityFrameworkCore;
using Web_BTL.Models;
using Web_BTL.Models.Actors;
using Web_BTL.Models.ListMedia.Watch;
using Web_BTL.Models.Medias;
using Web_BTL.Models.User.Admin;
using Web_BTL.Models.User.Customer;

namespace Web_BTL.Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        // Bảng phim và thể loại
        public DbSet<MediaModel> Medias { get; set; }
        public DbSet<GenreModel> Genres { get; set; }

        // Bảng review
        public DbSet<ReviewModel> Reviews { get; set; }

        // Bảng Admin và Customer
        public DbSet<AdminModel> Admins { get; set; }
        public DbSet<CustomerModel> Customers { get; set; }

        // Bảng Actor
        public DbSet<ActorModel> Actors { get; set; }

        // Bảng Watch List và bảng phụ
        public DbSet<WatchListModel> WatchLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MediaModel>().
                HasMany(m => m.Actors).
                WithMany(a => a.Medias).
                UsingEntity(j => j.ToTable("Media_Actor"));
            modelBuilder.Entity<MediaModel>().
                HasMany(m => m.Genres).
                WithMany(g => g.Medias).
                UsingEntity(j => j.ToTable("Media_Genre"));
            modelBuilder.Entity<AdminModel>().
                Property(a => a.Role).
                HasConversion<string>();  // Ánh xạ enum thành chuỗi
            modelBuilder.Entity<CustomerModel>().
                Property(c => c._ServicePackage).
                HasConversion<string>();  // Ánh xạ enum thành chuỗi
        }
    }
}
