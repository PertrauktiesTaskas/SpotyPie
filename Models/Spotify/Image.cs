using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Spotify
{
    public class Image
    {
        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }

}
