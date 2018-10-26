using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.BackEnd
{
    public class Album
    {
        public int Id { get; set; }

        public string AlbumType { get; set; }

        // JSON
        public string Artists { get; set; }

        // Json
        public string Copyrights { get; set; }

        //[Json] List<string>
        public string Genres { get; set; }

        public List<Image> Images { get; set; }

        public string Label { get; set; }

        public string Name { get; set; }

        //click count
        public long Popularity { get; set; }

        public DateTimeOffset ReleaseDate { get; set; }

        public long TotalTracks { get; set; }

        public List<Item> Songs { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActiveTime { get; set; }

        public Album()
        {

        }

        public Album(Models.Spotify.Album al)
        {
            Id = 0;
            Created = DateTime.Now;
            AlbumType = al.AlbumType;
            Artists = al.Artists != null ? JsonConvert.SerializeObject(Helpers.GetArtist(al.Artists)) : null;
            Copyrights = al.Copyrights != null ? JsonConvert.SerializeObject(Helpers.GetCopyrights(al.Copyrights)) : null;
            Genres = al.Genres != null ? JsonConvert.SerializeObject(al.Genres.ToList()) : null;
            Images = al.Images != null ? Helpers.GetImages(al.Images) : null;
            Label = al.Label;
            Name = al.Name;
            Popularity = 0;
            ReleaseDate = al.ReleaseDate;
            TotalTracks = al.TotalTracks;
            Songs = al.Tracks != null ? Helpers.GetItems(al.Tracks.Items) : null;
            LastActiveTime = DateTime.Now;
        }
    }
}
