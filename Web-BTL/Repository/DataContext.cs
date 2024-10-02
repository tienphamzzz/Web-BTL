using Microsoft.EntityFrameworkCore;
using Web_BTL.Models;
using Web_BTL.Models.Actors;
using Web_BTL.Models.ListMedia.Favorite;
using Web_BTL.Models.ListMedia.History;
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
        public DbSet<MediaModel> Medias;
        public DbSet<GenreModel> Genres;

        // Bảng review
        public DbSet<ReviewModel> Reviews;

        // Bảng Admin và Customer
        public DbSet<AdminModel> Admins;
        public DbSet<CustomerModel> Customers;

        // Bảng Actor
        public DbSet<ActorModel> Actors;
        
        // Bảng Favorite List
        public DbSet<FavoriteListModel> FavoriteLists;
        
        // Bảng History List
        public DbSet<HistoryListModel> HistoryLists;

        // Bảng Watch List và bảng phụ
        public DbSet<WatchListModel> WatchLists;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MediaModel>().
                HasMany(m => m.Actors).
                WithMany(a => a.Medias).
                UsingEntity(j => j.ToTable("Media_Actor"));
            modelBuilder.Entity<MediaModel>().
                HasMany(m => m.FavoriteLists).
                WithMany(f => f.Medias).
                UsingEntity(j => j.ToTable("Media_Favourite"));
            modelBuilder.Entity<MediaModel>().
                HasMany(m => m.WatchLists).
                WithMany(w => w.Medias).
                UsingEntity(j => j.ToTable("Media_Watch"));
        }
    }
}
