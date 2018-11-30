using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class AddCurrentSongPop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Popularity",
                table: "Item",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastActiveTime",
                table: "Artists",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "CurrentSong",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArtistId = table.Column<int>(nullable: false),
                    PlaylistId = table.Column<int>(nullable: false),
                    AlbumId = table.Column<int>(nullable: false),
                    SongId = table.Column<int>(nullable: false),
                    DurationMs = table.Column<long>(nullable: false),
                    CurrentMs = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    LocalUrl = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentSong", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrentSong");

            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "LastActiveTime",
                table: "Artists");
        }
    }
}
