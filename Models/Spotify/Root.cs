using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Spotify
{
    public class ArtistRoot
    {
        [JsonProperty("artists")]
        public List<Artist> Artists { get; set; }
    }
    public partial class AlbumRoot
    {
        [JsonProperty("albums")]
        public List<Album> Albums { get; set; }
    }
}
