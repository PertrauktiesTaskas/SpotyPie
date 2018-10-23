using System;
using System.Collections.Generic;
using System.Text;

namespace Models.BackEnd
{
    public class Tracks
    {
        public int Id { get; set; }
        public List<Item> Items { get; set; }

        public long Total { get; set; }
    }
}
