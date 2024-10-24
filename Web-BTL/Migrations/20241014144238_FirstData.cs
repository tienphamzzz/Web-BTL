﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_BTL.Migrations
{
<<<<<<<< HEAD:Web-BTL/Migrations/20241024032941_them.cs
    public partial class them : Migration
========
    public partial class FirstData : Migration
>>>>>>>> MXD:Web-BTL/Migrations/20241014144238_FirstData.cs
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    ActorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcctorDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.ActorID);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserLogin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoginPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserState = table.Column<bool>(type: "bit", nullable: true),
                    UserDuration = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
<<<<<<<< HEAD:Web-BTL/Migrations/20241024032941_them.cs
                name: "WatchLists",
                columns: table => new
                {
                    WatchListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchLists", x => x.WatchListId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    _ServicePackage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HistoryListId = table.Column<int>(type: "int", nullable: true),
                    FavoriteListId = table.Column<int>(type: "int", nullable: true),
                    WatchListId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserLogin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoginPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserState = table.Column<bool>(type: "bit", nullable: true),
                    UserDuration = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customers_WatchLists_WatchListId",
                        column: x => x.WatchListId,
                        principalTable: "WatchLists",
                        principalColumn: "WatchListId");
                });

            migrationBuilder.CreateTable(
========
>>>>>>>> MXD:Web-BTL/Migrations/20241014144238_FirstData.cs
                name: "Medias",
                columns: table => new
                {
                    MediaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediaName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediaDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediaQuality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MediaAgeRating = table.Column<int>(type: "int", nullable: true),
                    MediaImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediaDuration = table.Column<TimeSpan>(type: "time", nullable: true),
                    package = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.MediaId);
                });

            migrationBuilder.CreateTable(
                name: "WatchLists",
                columns: table => new
                {
                    WatchListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchLists", x => x.WatchListId);
                });

            migrationBuilder.CreateTable(
                name: "Media_Actor",
                columns: table => new
                {
                    ActorsActorID = table.Column<int>(type: "int", nullable: false),
                    MediasMediaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media_Actor", x => new { x.ActorsActorID, x.MediasMediaId });
                    table.ForeignKey(
                        name: "FK_Media_Actor_Actors_ActorsActorID",
                        column: x => x.ActorsActorID,
                        principalTable: "Actors",
                        principalColumn: "ActorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Media_Actor_Medias_MediasMediaId",
                        column: x => x.MediasMediaId,
                        principalTable: "Medias",
                        principalColumn: "MediaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Media_Genre",
                columns: table => new
                {
                    GenresGenreId = table.Column<int>(type: "int", nullable: false),
                    MediasMediaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media_Genre", x => new { x.GenresGenreId, x.MediasMediaId });
                    table.ForeignKey(
                        name: "FK_Media_Genre_Genres_GenresGenreId",
                        column: x => x.GenresGenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Media_Genre_Medias_MediasMediaId",
                        column: x => x.MediasMediaId,
                        principalTable: "Medias",
                        principalColumn: "MediaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    _ServicePackage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HistoryListId = table.Column<int>(type: "int", nullable: true),
                    FavoriteListId = table.Column<int>(type: "int", nullable: true),
                    WatchListId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserLogin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoginPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserState = table.Column<bool>(type: "bit", nullable: true),
                    UserDuration = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customers_WatchLists_WatchListId",
                        column: x => x.WatchListId,
                        principalTable: "WatchLists",
                        principalColumn: "WatchListId");
                });

            migrationBuilder.CreateTable(
                name: "ListMedia",
                columns: table => new
                {
                    WatchListId = table.Column<int>(type: "int", nullable: false),
                    MediaId = table.Column<int>(type: "int", nullable: false),
                    IsWatched = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    Favorite = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    AddDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListMedia", x => new { x.WatchListId, x.MediaId });
                    table.ForeignKey(
                        name: "FK_ListMedia_Medias_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Medias",
                        principalColumn: "MediaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListMedia_WatchLists_WatchListId",
                        column: x => x.WatchListId,
                        principalTable: "WatchLists",
                        principalColumn: "WatchListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewRating = table.Column<double>(type: "float", nullable: true),
                    ReviewCreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    MediaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Medias_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Medias",
                        principalColumn: "MediaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_WatchListId",
                table: "Customers",
                column: "WatchListId",
                unique: true,
                filter: "[WatchListId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ListMedia_MediaId",
                table: "ListMedia",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Media_Actor_MediasMediaId",
                table: "Media_Actor",
                column: "MediasMediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Media_Genre_MediasMediaId",
                table: "Media_Genre",
                column: "MediasMediaId");

            migrationBuilder.CreateIndex(
<<<<<<<< HEAD:Web-BTL/Migrations/20241024032941_them.cs
                name: "IX_Medias_WatchListId",
                table: "Medias",
                column: "WatchListId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CustomerId",
========
                name: "IX_Reviews_MediasMediaId",
>>>>>>>> MXD:Web-BTL/Migrations/20241014144238_FirstData.cs
                table: "Reviews",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MediaId",
                table: "Reviews",
                column: "MediaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "ListMedia");

            migrationBuilder.DropTable(
                name: "Media_Actor");

            migrationBuilder.DropTable(
                name: "Media_Genre");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Medias");

            migrationBuilder.DropTable(
                name: "WatchLists");
        }
    }
}
