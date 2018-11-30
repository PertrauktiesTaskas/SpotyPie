using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class Spotypie2_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Playlist",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Popularity",
                table: "Playlist",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Item",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Playlist");

            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "Playlist");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Item");
        }
    }
}
