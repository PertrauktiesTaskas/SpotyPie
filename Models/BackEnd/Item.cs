using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.BackEnd
{
    public class Item
    {
        public string Id { get; set; }

        public List<Artist> Artists { get; set; }

        public long DiscNumber { get; set; }

        public long DurationMs { get; set; }

        public bool Explicit { get; set; }

        public bool IsLocal { get; set; }

        public bool IsPlayable { get; set; }

        public string Name { get; set; }

        public long TrackNumber { get; set; }

    }
}
