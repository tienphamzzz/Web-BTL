using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_BTL.Migrations
{
    public partial class updateReviewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Customers_UserModelCustomerId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Medias_MediasMediaId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_MediasMediaId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_UserModelCustomerId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "MediasMediaId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UserModelCustomerId",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CustomerId",
                table: "Reviews",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MediaId",
                table: "Reviews",
                column: "MediaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Customers_CustomerId",
                table: "Reviews",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Medias_MediaId",
                table: "Reviews",
                column: "MediaId",
                principalTable: "Medias",
                principalColumn: "MediaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Customers_CustomerId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Medias_MediaId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CustomerId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_MediaId",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Reviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MediasMediaId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserModelCustomerId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MediasMediaId",
                table: "Reviews",
                column: "MediasMediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserModelCustomerId",
                table: "Reviews",
                column: "UserModelCustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Customers_UserModelCustomerId",
                table: "Reviews",
                column: "UserModelCustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Medias_MediasMediaId",
                table: "Reviews",
                column: "MediasMediaId",
                principalTable: "Medias",
                principalColumn: "MediaId");
        }
    }
}
