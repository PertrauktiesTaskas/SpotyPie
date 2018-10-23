using System;
using System.Collections.Generic;
using System.Text;

namespace Models.BackEnd
{
    public partial class Album
    {
        public string Id { get; set; }

        public string AlbumType { get; set; }

        public List<Artist> Artists { get; set; }

        public List<Copyright> Copyrights { get; set; }

        //[Json] List<string>
        public string Genres { get; set; }

        public List<Image> Images { get; set; }

        public string Label { get; set; }

        public string Name { get; set; }

        //click count
        public long Popularity { get; set; }

        public DateTimeOffset ReleaseDate { get; set; }

        public long TotalTracks { get; set; }

        public Tracks Tracks { get; set; }

    }
}
