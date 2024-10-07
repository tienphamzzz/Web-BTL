﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Web_BTL.Repository;

#nullable disable

namespace Web_BTL.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.33")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ActorModelMediaModel", b =>
                {
                    b.Property<int>("ActorsActorID")
                        .HasColumnType("int");

                    b.Property<int>("MediasMediaId")
                        .HasColumnType("int");

                    b.HasKey("ActorsActorID", "MediasMediaId");

                    b.HasIndex("MediasMediaId");

                    b.ToTable("Media_Actor", (string)null);
                });

            modelBuilder.Entity("GenreModelMediaModel", b =>
                {
                    b.Property<int>("GenresGenreId")
                        .HasColumnType("int");

                    b.Property<int>("MediasMediaId")
                        .HasColumnType("int");

                    b.HasKey("GenresGenreId", "MediasMediaId");

                    b.HasIndex("MediasMediaId");

                    b.ToTable("Media_Genre", (string)null);
                });

            modelBuilder.Entity("Web_BTL.Models.Actors.ActorModel", b =>
                {
                    b.Property<int>("ActorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ActorID"), 1L, 1);

                    b.Property<DateTime?>("AcctorDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ActorName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ActorID");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("Web_BTL.Models.ListMedia.Watch.WatchListModel", b =>
                {
                    b.Property<int>("WatchListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WatchListId"), 1L, 1);

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.HasKey("WatchListId");

                    b.ToTable("WatchLists");
                });

            modelBuilder.Entity("Web_BTL.Models.Medias.GenreModel", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreId"), 1L, 1);

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GenreId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Web_BTL.Models.Medias.MediaModel", b =>
                {
                    b.Property<int>("MediaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MediaId"), 1L, 1);

                    b.Property<bool?>("Basic")
                        .HasColumnType("bit");

                    b.Property<bool?>("Favorite")
                        .HasColumnType("bit");

                    b.Property<int?>("MediaAgeRating")
                        .HasColumnType("int");

                    b.Property<string>("MediaDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan?>("MediaDuration")
                        .HasColumnType("time");

                    b.Property<string>("MediaImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MediaName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MediaQuality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("MediaState")
                        .HasColumnType("bit");

                    b.Property<string>("MediaUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Premium")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("Vip")
                        .HasColumnType("bit");

                    b.Property<int>("WatchListId")
                        .HasColumnType("int");

                    b.Property<bool?>("Watched")
                        .HasColumnType("bit");

                    b.HasKey("MediaId");

                    b.HasIndex("WatchListId");

                    b.ToTable("Medias");
                });

            modelBuilder.Entity("Web_BTL.Models.ReviewModel", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewId"), 1L, 1);

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("MediaId")
                        .HasColumnType("int");

                    b.Property<int?>("MediasMediaId")
                        .HasColumnType("int");

                    b.Property<string>("ReviewContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ReviewCreateDate")
                        .HasColumnType("datetime2");

                    b.Property<double?>("ReviewRating")
                        .HasColumnType("float");

                    b.Property<int?>("UserModelCustomerId")
                        .HasColumnType("int");

                    b.HasKey("ReviewId");

                    b.HasIndex("MediasMediaId");

                    b.HasIndex("UserModelCustomerId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Web_BTL.Models.User.Admin.AdminModel", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminId"), 1L, 1);

                    b.Property<string>("LoginPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UserCreateDate")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan?>("UserDuration")
                        .HasColumnType("time");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserLogin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("UserState")
                        .HasColumnType("bit");

                    b.HasKey("AdminId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Web_BTL.Models.User.Customer.CustomerModel", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"), 1L, 1);

                    b.Property<int?>("FavoriteListId")
                        .HasColumnType("int");

                    b.Property<int?>("HistoryListId")
                        .HasColumnType("int");

                    b.Property<string>("LoginPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UserCreateDate")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan?>("UserDuration")
                        .HasColumnType("time");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserLogin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("UserState")
                        .HasColumnType("bit");

                    b.Property<int?>("WatchListId")
                        .HasColumnType("int");

                    b.Property<string>("_ServicePackage")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.HasIndex("WatchListId")
                        .IsUnique()
                        .HasFilter("[WatchListId] IS NOT NULL");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ActorModelMediaModel", b =>
                {
                    b.HasOne("Web_BTL.Models.Actors.ActorModel", null)
                        .WithMany()
                        .HasForeignKey("ActorsActorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web_BTL.Models.Medias.MediaModel", null)
                        .WithMany()
                        .HasForeignKey("MediasMediaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GenreModelMediaModel", b =>
                {
                    b.HasOne("Web_BTL.Models.Medias.GenreModel", null)
                        .WithMany()
                        .HasForeignKey("GenresGenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web_BTL.Models.Medias.MediaModel", null)
                        .WithMany()
                        .HasForeignKey("MediasMediaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Web_BTL.Models.Medias.MediaModel", b =>
                {
                    b.HasOne("Web_BTL.Models.ListMedia.Watch.WatchListModel", "WatchList")
                        .WithMany("Medias")
                        .HasForeignKey("WatchListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WatchList");
                });

            modelBuilder.Entity("Web_BTL.Models.ReviewModel", b =>
                {
                    b.HasOne("Web_BTL.Models.Medias.MediaModel", "Medias")
                        .WithMany("Reviews")
                        .HasForeignKey("MediasMediaId");

                    b.HasOne("Web_BTL.Models.User.Customer.CustomerModel", "UserModel")
                        .WithMany("Reviews")
                        .HasForeignKey("UserModelCustomerId");

                    b.Navigation("Medias");

                    b.Navigation("UserModel");
                });

            modelBuilder.Entity("Web_BTL.Models.User.Customer.CustomerModel", b =>
                {
                    b.HasOne("Web_BTL.Models.ListMedia.Watch.WatchListModel", "WatchList")
                        .WithOne("User")
                        .HasForeignKey("Web_BTL.Models.User.Customer.CustomerModel", "WatchListId");

                    b.Navigation("WatchList");
                });

            modelBuilder.Entity("Web_BTL.Models.ListMedia.Watch.WatchListModel", b =>
                {
                    b.Navigation("Medias");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Web_BTL.Models.Medias.MediaModel", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Web_BTL.Models.User.Customer.CustomerModel", b =>
                {
                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
