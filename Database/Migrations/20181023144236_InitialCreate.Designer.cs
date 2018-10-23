﻿// <auto-generated />
using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Database.Migrations
{
    [DbContext(typeof(SpotyPieIDbContext))]
    [Migration("20181023144236_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.BackEnd.Album", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AlbumType");

                    b.Property<string>("Genres");

                    b.Property<string>("Label");

                    b.Property<string>("Name");

                    b.Property<long>("Popularity");

                    b.Property<DateTimeOffset>("ReleaseDate");

                    b.Property<long>("TotalTracks");

                    b.Property<int?>("TracksId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TracksId");

                    b.HasIndex("UserId");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("Models.BackEnd.Artist", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AlbumId");

                    b.Property<string>("Genres");

                    b.Property<string>("ItemId");

                    b.Property<string>("Name");

                    b.Property<long>("Popularity");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("ItemId");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("Models.BackEnd.Copyright", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AlbumId");

                    b.Property<string>("Text");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.ToTable("Copyrights");
                });

            modelBuilder.Entity("Models.BackEnd.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AlbumId");

                    b.Property<string>("ArtistId");

                    b.Property<long>("Height");

                    b.Property<string>("Url");

                    b.Property<long>("Width");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("ArtistId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Models.BackEnd.Item", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("DiscNumber");

                    b.Property<long>("DurationMs");

                    b.Property<bool>("Explicit");

                    b.Property<bool>("IsLocal");

                    b.Property<bool>("IsPlayable");

                    b.Property<string>("Name");

                    b.Property<long>("TrackNumber");

                    b.Property<int?>("TracksId");

                    b.HasKey("Id");

                    b.HasIndex("TracksId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Models.BackEnd.Tracks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("Total");

                    b.HasKey("Id");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("Models.BackEnd.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("Birthdate");

                    b.Property<string>("Country");

                    b.Property<string>("DisplayName");

                    b.Property<string>("Email");

                    b.Property<int?>("ImagesId");

                    b.HasKey("Id");

                    b.HasIndex("ImagesId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Models.BackEnd.Album", b =>
                {
                    b.HasOne("Models.BackEnd.Tracks", "Tracks")
                        .WithMany()
                        .HasForeignKey("TracksId");

                    b.HasOne("Models.BackEnd.User")
                        .WithMany("Albums")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Models.BackEnd.Artist", b =>
                {
                    b.HasOne("Models.BackEnd.Album")
                        .WithMany("Artists")
                        .HasForeignKey("AlbumId");

                    b.HasOne("Models.BackEnd.Item")
                        .WithMany("Artists")
                        .HasForeignKey("ItemId");
                });

            modelBuilder.Entity("Models.BackEnd.Copyright", b =>
                {
                    b.HasOne("Models.BackEnd.Album")
                        .WithMany("Copyrights")
                        .HasForeignKey("AlbumId");
                });

            modelBuilder.Entity("Models.BackEnd.Image", b =>
                {
                    b.HasOne("Models.BackEnd.Album")
                        .WithMany("Images")
                        .HasForeignKey("AlbumId");

                    b.HasOne("Models.BackEnd.Artist")
                        .WithMany("Images")
                        .HasForeignKey("ArtistId");
                });

            modelBuilder.Entity("Models.BackEnd.Item", b =>
                {
                    b.HasOne("Models.BackEnd.Tracks")
                        .WithMany("Items")
                        .HasForeignKey("TracksId");
                });

            modelBuilder.Entity("Models.BackEnd.User", b =>
                {
                    b.HasOne("Models.BackEnd.Image", "Images")
                        .WithMany()
                        .HasForeignKey("ImagesId");
                });
#pragma warning restore 612, 618
        }
    }
}
