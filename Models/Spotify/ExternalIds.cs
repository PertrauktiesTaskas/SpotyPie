using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Spotify
{
    public class ExternalIds
    {
        [JsonProperty("upc")]
        public string Upc { get; set; }
    }
}
