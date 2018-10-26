using System;
using System.Collections.Generic;
using System.Text;

namespace Models.BackEnd
{
    public class Playlist
    {
        public int Id { get; set; }

        public List<Item> Items { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActiveTime { get; set; }

        public long Limit { get; set; }

        public long Total { get; set; }
    }
}
