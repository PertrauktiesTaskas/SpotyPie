using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Spotify
{
    public class Followers
    {
        [JsonProperty("href")]
        public object Href { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }
}
