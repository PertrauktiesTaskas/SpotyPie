using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class Spotypie2_PlaylistImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Playlist",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Playlist");
        }
    }
}
